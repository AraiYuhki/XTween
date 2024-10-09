using Codice.CM.Common.Tree.Partial;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Xeon.XTween
{
    public enum EaseType
    {
        Linear,

        InSine,
        OutSine,
        InOutSine,

        InQuad,
        OutQuad,
        InOutQuad,

        InCubic,
        OutCubic,
        InOutCubic,

        InQuart,
        OutQuart,
        InOutQuart,

        InQuint,
        OutQuint,
        InOutQuint,

        InExpo,
        OutExpo,
        InOutExpo,

        InCirc,
        OutCirc,
        InOutCirc,

        InBack,
        OutBack,
        InOutBack,

        InElastic,
        OutElastic,
        InOutElastic,

        InBounce,
        OutBounce,
        InOutBounce
    }

    public static class TweenFunctions
    {
        private const float value1 = 1.70158f;
        private const float value2 = value1 * 1.525f;
        private const float value3 = value1 + 1f;
        private const float value4 = 2 * Mathf.PI / 3;
        private const float value5 = 2 * Mathf.PI / 4.5f;

        private const float n = 7.5625f;
        private const float d = 2.75f;

        private static readonly Dictionary<EaseType, Func<float, float>> functions = new()
        {
            { EaseType.Linear, Linear },
            { EaseType.InSine, InSine },
            { EaseType.OutSine, OutSine },
            { EaseType.InOutSine, InOutSine },
            { EaseType.InQuad, InQuad },
            { EaseType.OutQuad, OutQuad },
            { EaseType.InOutQuad, InOutQuad },
            { EaseType.InCubic, InCubic },
            { EaseType.OutCubic, OutCubic },
            { EaseType.InOutCubic, InOutCubic },
            { EaseType.InQuart, InQuart },
            { EaseType.OutQuart, OutQuart },
            { EaseType.InOutQuart, InOutQuart },
            { EaseType.InQuint, InQuint },
            { EaseType.OutQuint, OutQuint },
            { EaseType.InOutQuint, InOutQuint },
            { EaseType.InExpo, InExpo },
            { EaseType.OutExpo, OutExpo },
            { EaseType.InOutExpo, InOutExpo },
            { EaseType.InCirc, InCirc },
            { EaseType.OutCirc, OutCirc },
            { EaseType.InOutCirc, InOutCirc },
            { EaseType.InBack, InBack },
            { EaseType.OutBack, OutBack },
            { EaseType.InOutBack, InOutBack },
            { EaseType.InElastic, InElastic },
            { EaseType.OutElastic, OutElastic },
            { EaseType.InOutElastic, InOutElastic },
            { EaseType.InBounce, InBounce },
            { EaseType.OutBounce, OutBounce },
            { EaseType.InOutBounce, InOutBounce}
        };

        public static float Evaluate(EaseType type, float time)
            => functions[type](time);
        public static float Linear(float time) => time;
        public static double Linear(double time) => time;

        public static float InSine(float time) => 1 - Mathf.Cos(time * Mathf.PI * 0.5f);
        public static double InSine(double time) => 1d - Math.Cos(time * Math.PI * 0.5);

        public static float OutSine(float time) => Mathf.Sin(time * Mathf.PI * 0.5f);
        public static double OutSine(double time) => Math.Sin(time * Math.PI * 0.5);

        public static float InOutSine(float time) => -(Mathf.Cos(Mathf.PI * time) - 1) * 0.5f;
        public static double InOutSine(double time) => -(Math.Cos(Math.PI * time) - 1d) * 0.5;

        public static float InQuad(float time) => time * time;
        public static double InQuad(double time) => time * time;

        public static float OutQuad(float time) => 1 - (1 - time) * (1 - time);
        public static double OutQuad(double time) => 1 - (1 - time) * (1 - time);

        public static float InOutQuad(float time) => time < 0.5f ? 2 * time * time : 1 - Mathf.Pow(-2 * time + 2, 2) * 0.5f;
        public static double InOutQuad(double time) => time < 0.5 ? 2 * time * time : 1 - Math.Pow(-2 * time + 2, 2) * 0.5;

        public static float InCubic(float time) => Mathf.Pow(time, 3);
        public static double InCubic(double time) => Math.Pow(time, 3);

        public static float OutCubic(float time) => 1 - Mathf.Pow(1 - time, 3);
        public static double OutCubic(double time) => 1 - Math.Pow(1 - time, 3);

        public static float InOutCubic(float time) => time < 0.5 ? 4 * Mathf.Pow(time, 3) : 1 - Mathf.Pow(-2 * time + 2, 3) * 0.5f;
        public static double InOutCubic(double time) => time < 0.5 ? 4 * Math.Pow(time, 3) : 1 - Math.Pow(-2 * time + 2, 3) * 0.5;

        public static float InQuart(float time) => Mathf.Pow(time, 4);
        public static double InQuart(double time) => Math.Pow(time, 4);

        public static float OutQuart(float time) => 1 - Mathf.Pow(1 - time, 4);
        public static double OutQuart(double time) => 1 - Math.Pow(1 - time, 4);

        public static float InOutQuart(float time) => time < 0.5f ? 8 * Mathf.Pow(time, 4) : 1 - Mathf.Pow(-2 * time + 2, 4) * 0.5f;
        public static double InOutQuart(double time) => time < 0.5 ? 8 * Math.Pow(time, 4) : 1 - Math.Pow(-2 * time + 2, 4) * 0.5;

        public static float InQuint(float time) => Mathf.Pow(time, 5);
        public static double InQuint(double time) => Math.Pow(time, 5);

        public static float OutQuint(float time) => 1 - Mathf.Pow(1 - time, 5);
        public static double OutQuint(double time) => 1 - Math.Pow(1 - time, 5);

        public static float InOutQuint(float time) => time < 0.5f ? 16 * Mathf.Pow(time, 5) : 1 - Mathf.Pow(-2 * time + 2, 5) * 0.5f;
        public static double InOutQuint(double time) => time < 0.5 ? 16 * Math.Pow(time, 5) : 1 - Math.Pow(-2 * time + 2, 5) * 0.5;

        public static float InExpo(float time) => time == 0 ? 0 : Mathf.Pow(2, 10 * time - 10);
        public static double InExpo(double time) => time == 0 ? 0d : Math.Pow(2, 10 * time - 10);

        public static float OutExpo(float time) => time == 1f ? 1f : 1 - Mathf.Pow(2, -10 * time);
        public static double OutExpo(double time) => time == 1f ? 1f : 1 - Math.Pow(2, -10 * time);

        public static float InOutExpo(float time)
        {
            if (time == 0) return 0;
            if (time == 1f) return 1f;
            if (time < 0.5f)
                return Mathf.Pow(2, 20 * time - 10) * 0.5f;
            return (2 - Mathf.Pow(2, -20 * time + 10)) * 0.5f;
        }

        public static double InOutExpo(double time)
        {
            if (time == 0) return 0;
            if (time == 1) return 1;
            if (time < 0.5)
                return Math.Pow(2, 20 * time - 10) * 0.5;
            return (2 - Math.Pow(2, -20 * time + 10)) * 0.5;
        }

        public static float InCirc(float time) => 1 - Mathf.Sqrt(1 - Mathf.Pow(time, 2));
        public static double InCirc(double time) => 1d - Math.Sqrt(1d - Math.Pow(time, 2d));

        public static float OutCirc(float time) => Mathf.Sqrt(1 - Mathf.Pow(time - 1, 2));
        public static double OutCirc(double time) => Math.Sqrt(1d - Math.Pow(time - 1d, 2d));

        public static float InOutCirc(float time) => time < 0.5f ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * time, 2))) * 0.5f : (Mathf.Sqrt(1 - Mathf.Pow(-2 * time + 2, 2)) + 1) * 0.5f;
        public static double InOutCirc(double time) => time < 0.5f ? (1d - Math.Sqrt(1d - Math.Pow(2d * time, 2d))) * 0.5 : (Math.Sqrt(1d - Math.Pow(-2d * time + 2d, 2d)) + 1d) * 0.5;

        public static float InBack(float time) => value3 * Mathf.Pow(time, 3) - value1 * time * time;
        public static double InBack(double time) => value3 * Math.Pow(time, 3d) - value1 * time * time;

        public static float OutBack(float time) => 1 + value3 * Mathf.Pow(time - 1, 3) + value1 * Mathf.Pow(time - 1, 2);
        public static double OutBack(double time) => 1d + value3 * Math.Pow(time - 1d, 3d) + value1 * Math.Pow(time - 1d, 2d);

        public static float InOutBack(float time)
        {
            if (time < 0.5f)
                return Mathf.Pow(2 * time, 2) * ((value2 + 1) * 2 * time - value2) * 0.5f;
            return (Mathf.Pow(2 * time - 2, 2) * ((value2 + 1) * (time * 2 - 2) + value2) + 2) * 0.5f;
        }

        public static double InOutBack(double time)
        {
            if (time < 0.5)
                return Math.Pow(2d * time, 2d) * ((value2 + 1d) * 2d * time - value2) + 0.5;
            return (Math.Pow(2d * time - 2d, 2d) * ((value2 + 1d) * (time * 2d - 2d) + value2) + 2d) * 0.5;
        }

        public static float InElastic(float time)
        {
            if (time == 0f) return 0f;
            if (time == 1f) return 1f;
            return -Mathf.Pow(2, 10 * time - 10) * Mathf.Sin((time * 10 - 10.75f) * value4);
        }

        public static double InElastic(double time)
        {
            if (time == 0d) return 0d;
            if (time == 1d) return 1d;
            return -Math.Pow(2d, 10d * time - 10d) * Math.Sin((time * 10d - 10.75) * value4);
        }

        public static float OutElastic(float time)
        {
            if (time == 0f) return 0f;
            if (time == 1f) return 1f;
            return Mathf.Pow(2, -10 * time) * Mathf.Sin((time * 10 - 0.75f) * value4) + 1;
        }

        public static double OutElastic(double time)
        {
            if (time == 0d) return 0d;
            if (time == 1d) return 1d;
            return Math.Pow(2d, -10d * time) * Math.Sin((time * 10d - 0.75d) * value4) + 1d;
        }

        public static float InOutElastic(float time)
        {
            if (time == 0f) return 0f;
            if (time == 1f) return 1f;
            if (time < 0.5f)
                return -(Mathf.Pow(2, 20 * time - 10) * Mathf.Sin((20 * time - 11.125f) * value5)) * 0.5f;
            return Mathf.Pow(2, -20 * time + 10) * Mathf.Sin((20 * time - 11.125f) * value5) * 0.5f + 1;
        }

        public static double InOutElastic(double time)
        {
            if (time == 0d) return 0d;
            if (time == 1d) return 1d;
            if (time < 0.5)
                return -(Math.Pow(2d, 20d * time - 10d) * Math.Sin((20 * time - 11.125) * value5)) * 0.5;
            return Math.Pow(2d, -20d * time + 10d) * Math.Sin((20 * time - 11.125) * value5) * 0.5 + 1;
        }

        public static float InBounce(float time) => 1 - OutBounce(1 - time);
        public static double InBounce(double time) => 1d - OutBounce(1 - time);

        public static float OutBounce(float time)
        {
            if (time < 1 / d)
                return n * time * time;
            if (time < 2 / d)
                return n * (time -= 1.5f / d) * time + 0.75f;
            if (time < 2.5f / d)
                return n * (time -= 2.25f / d) * time + 0.9375f;
            return n * (time -= 2.625f / d) * time + 0.984375f;
        }

        public static double OutBounce(double time)
        {
            if (time < 1 / d)
                return n * time * time;
            if (time < 2 / d)
                return n * (time -= 1.5 / d) * time + 0.75;
            if (time < 2.5 / d)
                return n * (time -= 2.25 / d) * time + 0.9375;
            return n * (time -= 2.625 / d) * time + 0.984375;
        }

        public static float InOutBounce(float time) => time < 0.5f ? (1 - OutBounce(1 - 2 * time)) * 0.5f : (1 + OutBounce(2 * time - 1)) * 0.5f;
        public static double InOutBounce(double time) => time < 0.5 ? (1 - OutBounce(1 - 2 * time)) * 0.5 : (1 + OutBounce(2 * time - 1)) * 0.5;
    }
}
