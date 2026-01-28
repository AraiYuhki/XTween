using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;


public class ScriptImportPostProcessor : AssetPostprocessor
{
    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        var targets = importedAssets
            .Concat(movedAssets)
            .Where(path => System.IO.Path.GetExtension(path) == ".cs")
            .Distinct();
        foreach (var path in targets)
        {
            var fullPath = GetFullPath(path);
            var encoding = EncodeHelper.GetJpEncoding(fullPath);
            if (encoding == null)
            {
                Debug.LogError($"Failed to get encoding: {path}");
                continue;
            }
            if (encoding.EncodingName == Encoding.UTF8.EncodingName)
            {
                continue;
            }
            var data = string.Empty;
            using (var sr = new StreamReader(fullPath, encoding))
            {
                data = sr.ReadToEnd();
            }
            using (var sw = new StreamWriter(fullPath, false, Encoding.UTF8))
            {
                sw.Write(data);
            }
            Debug.Log($"{path} is encoded {encoding.EncodingName} to UTF8");
        }
    }

    private static string GetFullPath(string assetPath)
    {
        const string assetsPrefix = "Assets/";
        if (assetPath.StartsWith(assetsPrefix))
        {
            return Path.Combine(Application.dataPath, assetPath.Substring(assetsPrefix.Length));
        }

        return Path.GetFullPath(assetPath);
    }
}
