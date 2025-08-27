using System;
using UnityEngine;

namespace GA.Sessions.Class_03.Scripts
{
    [Serializable]
    public struct FloatDamper
    {
        [SerializeField] private float _smoothTime;
        private float currentVelocity;
        public float TargetValue { get; set; }
        public float CurrentValue { get; private set; }

        public void Update()
        {
            CurrentValue = Mathf.SmoothDamp(CurrentValue, TargetValue, ref currentVelocity, _smoothTime);
        }
    }
}