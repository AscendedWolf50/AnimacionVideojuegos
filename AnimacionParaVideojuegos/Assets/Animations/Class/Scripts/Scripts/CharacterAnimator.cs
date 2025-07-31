using UnityEngine;

namespace GA.Sessions.Class_02.Scripts
{
    public class CharacterAnimator
    {
        private readonly Animator _animator;
        private readonly int _speedHash = Animator.StringToHash("Blend");

        public CharacterAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void UpdateSpeed(float speed)
        {
            _animator.SetFloat(_speedHash, speed);
        }
    }
}