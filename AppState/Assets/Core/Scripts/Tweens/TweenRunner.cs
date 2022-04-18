using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OneDay.Core.Tweens
{
    public static class TweenRunner
    { 
        public static void Run(TweenConfig config, Action onFinished, Object customTarget)
        {
            switch (config.Type)
            {
             case TweenType.Move:
                 var target = customTarget != null ? (Transform) customTarget : config.TransformTarget;
                 var tween =
                     target.DOMove(config.ToVectorValue, config.Duration)
                         .SetLoops(config.Loops, config.LoopType)
                         .SetEase(config.EaseType)
                         .SetDelay(config.Delay);
                 if (config.UseFromValue)
                 {
                     tween.From(config.FromValue);
                 }

                 if (onFinished != null)
                 {
                     tween.onComplete += () => onFinished();
                 }
                 break;
             
             case TweenType.Scale:
                 target = customTarget != null ? (Transform) customTarget : config.TransformTarget;
                 tween = target.DOScale(config.ToVectorValue, config.Duration)
                         .SetLoops(config.Loops, config.LoopType)
                         .SetEase(config.EaseType)
                         .SetDelay(config.Delay);

                 if (config.UseFromValue)
                 {
                     tween.From(config.FromValue);
                 }
                 if (onFinished != null)
                 {
                     tween.onComplete += () => onFinished();
                 }
                 break;
             
             case TweenType.LocalMove:
                 target = customTarget != null ? (Transform) customTarget : config.TransformTarget;
                 tween = target.DOLocalMove(config.ToVectorValue, config.Duration)
                     .SetLoops(config.Loops, config.LoopType)
                     .SetEase(config.EaseType)
                     .SetDelay(config.Delay);

                 if (config.UseFromValue)
                 {
                     tween.From(config.FromValue);
                 }
                 if (onFinished != null)
                 {
                     tween.onComplete += () => onFinished();
                 }
                 break;
             default:
                 throw new ArgumentOutOfRangeException();
            }
        }
    }
}