using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Xeon.XTween
{
    public class Sequence : Tweener, ITween
    {
        protected List<Tweener> tweens = new();
        protected float lastTweenInsertTime = 0f;
        protected float lastTweenFullDuration = 0f;

        public List<ITween> GetTweens() => tweens.Select(tween => (ITween)tween).ToList();

        public override void Reset()
        {
            base.Reset();
            lastTweenInsertTime = 0f;
            lastTweenFullDuration = 0f;
            tweens.Clear();
        }

        public override void Update()
        {
            if (!IsPlaying) return;
            elapsed += UnityEngine.Time.deltaTime;
            foreach (var tween in tweens)
            {
                if (tween.IsPlaying || tween.IsCompleted) continue;
                if (elapsed >= tween.StartTime) tween.Play();
            }
            onUpdate?.Invoke();
            if (tweens.Any(t => !t.IsCompleted)) return;
            onComplete?.Invoke();
            IsPlaying = false;
            IsCompleted = true;
        }

        public override void Pause()
        {
            base.Pause();
            foreach (var tween in tweens)
                tween.Pause();
        }

        public override void Rewind()
        {
            foreach (var tween in tweens)
                tween.Rewind();
            base.Rewind();
        }

        public override void Kill()
        {
            base.Kill();
            foreach (var tween in tweens)
                tween.Kill();
        }

        public Sequence Append(Tweener tween)
        {
            tween.SetStartTime(lastTweenInsertTime);
            tween.SetIsSequenced();
            lastTweenInsertTime += tween.FullDuration;
            lastTweenFullDuration = tween.FullDuration;
            tweens.Add(tween);
            return this;
        }

        public Sequence Join(Tweener tween)
        {
            var lastTween = tweens.LastOrDefault();
            tween.SetStartTime(lastTween == null ? 0f : lastTween.StartTime);
            tween.SetIsSequenced();
            // 一緒に再生するTweenの内、最も長いTweenになるか確認する
            if (lastTweenFullDuration < tween.FullDuration)
            {
                // 差分の分だけ最後の時間を進める
                lastTweenInsertTime += tween.FullDuration - lastTweenFullDuration;
                lastTweenFullDuration = tween.FullDuration;
            }
            tweens.Add(tween);
            return this;
        }

        public Sequence Insert(float atPosition, Tweener tween)
        {
            tween.SetStartTime(atPosition);
            tween.SetIsSequenced();
            lastTweenInsertTime = Mathf.Max(lastTweenInsertTime, atPosition + tween.FullDuration);
            tweens.Add(tween);
            return this;
        }
    }
}
