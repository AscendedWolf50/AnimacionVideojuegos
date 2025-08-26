using GA.Sessions.Class_02.Scripts.Input;
using UnityEngine;

namespace GA.Sessions.Class_02.Scripts
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterInputFactory.InputType inputType = CharacterInputFactory.InputType.Player;

        private ICharacterInput _input;
        private CharacterAnimator _characterAnimator;

        private void Awake()
        {
            _input = CharacterInputFactory.CreateInput(inputType);
            _characterAnimator = new CharacterAnimator(animator);
        }

        private void Update()
        {
            float speed = _input.GetSpeedInput();
            _characterAnimator.UpdateSpeed(speed);
        }
    }
}