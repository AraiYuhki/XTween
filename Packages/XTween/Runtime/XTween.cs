using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Xeon.XTween
{
    public interface ITween
    {
        void Play();
        void Reset();
        void Update();
        void Kill();
        void SetPlayOnAwake(bool flag);
        bool PlayOnAwake { get; }
        bool IsCompleted { get; }
        bool IsSequenced { get; }
        bool IsKilled { get; }
    }

    public class XTween
    {
        private static XTween instance;
        public static XTween Instance
        {
            get
            {
                if (instance == null)
                    instance = new XTween();
                return instance;
            }
        }

        public bool PlayOnAwake { get; set; } = true;
        private int defaultPool = 30;

        private Dictionary<Type, LinkedPool<ITween>> pools = new();
        private List<ITween> activeTweens = new();

        private XTween()
        {
            pools[typeof(IntTween)] = new LinkedPool<ITween>(() => OnCreate<IntTween>(), OnGet, OnRelease, OnDestroy, true, defaultPool);
            pools[typeof(FloatTween)] = new LinkedPool<ITween>(() => OnCreate<FloatTween>(), OnGet, OnRelease, OnDestroy, true, defaultPool);
            pools[typeof(LongTween)] = new LinkedPool<ITween>(() => OnCreate<LongTween>(), OnGet, OnRelease, OnDestroy, true, defaultPool);
            pools[typeof(Vector2Tween)] = new LinkedPool<ITween>(() => OnCreate<Vector2Tween>(), OnGet, OnRelease, OnDestroy, true, defaultPool);
            pools[typeof(Vector3Tween)] = new LinkedPool<ITween>(() => OnCreate<Vector3Tween>(), OnGet, OnRelease, OnDestroy, true, defaultPool);
            pools[typeof(Vector4Tween)] = new LinkedPool<ITween>(() => OnCreate<Vector4Tween>(), OnGet, OnRelease, OnDestroy, true, defaultPool);
            pools[typeof(ColorTween)] = new LinkedPool<ITween>(() => OnCreate<ColorTween>(), OnGet, OnRelease, OnDestroy, true, defaultPool);
            pools[typeof(QuaternionTween)] = new LinkedPool<ITween>(() => OnCreate<QuaternionTween>(), OnGet, OnRelease, OnDestroy, true, defaultPool);
            pools[typeof(Sequence)] = new LinkedPool<ITween>(() => OnCreate<Sequence>(), OnGet, OnRelease, OnDestroy, true, defaultPool);

            Application.onBeforeRender -= Update;
            Application.onBeforeRender += Update;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= PlayModeStateChanged;
            UnityEditor.EditorApplication.playModeStateChanged += PlayModeStateChanged;
#else
            Application.wantsToQuit -= WantsToQuit;
            Application.wantsToQuit += WantsToQuit;
#endif
            Application.quitting -= KillAll;
            Application.quitting += KillAll;
        }

        private bool WantsToQuit()
        {
            KillAll();
            return false;
        }

#if UNITY_EDITOR
        private void PlayModeStateChanged(UnityEditor.PlayModeStateChange state)
        {
            if (state != UnityEditor.PlayModeStateChange.ExitingPlayMode) return;
            KillAll();
        }
#endif

        private T OnCreate<T>() where T : Tweener, new() => new T();
        private void OnGet<T>(T target) where T : ITween
        {
            activeTweens.Add(target);
            target.Reset();
            target.SetPlayOnAwake(PlayOnAwake);
        }

        private void OnRelease<T>(T target) where T : ITween => activeTweens.Remove(target);
        private void OnDestroy<T>(T target) { }

        private void Update()
        {
            foreach (var tween in activeTweens.ToList())
            {
                if (tween.PlayOnAwake)
                    tween.Play();

                if (tween.IsSequenced)
                {
                    tween.Update();
                    continue;
                }

                if (tween.IsKilled)
                {
                    Kill(tween);
                    continue;
                }

                tween.Update();

                if (!tween.IsCompleted) continue;
                Kill(tween);
            }
        }

        public void Kill(ITween tween)
        {
            pools[tween.GetType()].Release(tween);
            activeTweens.Remove(tween);
            if (tween is not Sequence sequence) return;
            Kill(sequence.GetTweens());
        }

        public void Kill(List<ITween> tweens)
        {
            foreach (var tween in tweens)
                Kill(tween);
        }

        public void KillAll()
        {
            foreach (var tween in activeTweens)
                tween.Kill();
            foreach (var (type, pool) in pools)
                pool.Clear();
            activeTweens.Clear();
        }

        public static T Get<T>() where T : Tweener
            => Instance.pools[typeof(T)].Get() as T;

        public static Sequence Sequence()
            => Instance.pools[typeof(Sequence)].Get() as Sequence;
    }
}
