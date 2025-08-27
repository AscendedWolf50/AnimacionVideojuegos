using System;
using UnityEngine;

namespace GA.Sessions.Class_04.Scripts
{
    public class HitReceiver: MonoBehaviour,IHittable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string hitTrigger = "Hit";
        public void ApplyHit(HitInfo hitInfo)
        {
            if(animator) animator.SetTrigger(hitTrigger);
        }

        private void Reset()
        {
            animator = GetComponentInChildren<Animator>();
        }
    }
}