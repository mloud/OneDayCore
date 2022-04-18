using System;
using DG.Tweening;
using UnityEngine;

namespace OneDay.Core.Tweens
{
    [Serializable]
    public class TweenConfig
    {
        public TweenType Type;
        [Header("Timing")]
        public float Duration;
        public float Delay;
        [Header("Easing")]
        public Ease EaseType;
        [Header("Looping")]
        public LoopType LoopType;
        public int Loops;
        [Header("Values")]
        public bool UseFromValue;
        public Vector3 ToVectorValue;
        public Vector3 FromValue;
        [Header("Target")]
        public Transform TransformTarget;
        public Transform ImageTarget;
        public Transform SpriteTarget;
    }
}