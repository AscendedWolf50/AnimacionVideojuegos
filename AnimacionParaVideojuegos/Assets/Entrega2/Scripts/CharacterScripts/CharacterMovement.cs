using UnityEngine;
using UnityEngine.InputSystem;

namespace GA.Sessions.Class_03.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class CharacterMovement : MonoBehaviour, ICharacterComponent
    {
        [SerializeField] private FloatDamper speedX;
        [SerializeField] private FloatDamper speedY;
        [SerializeField] private float angularSpeed;
        [SerializeField] private Camera camera;
        private Quaternion targetRotation;


        private int _speedXHash;
        private int _speedYHash;
        private Animator _animator;
        public Character ParentCharacter { get; set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _speedXHash = Animator.StringToHash("SpeedX");
            _speedYHash = Animator.StringToHash("SpeedY");
        }
        private void SolveCharacterRotation()
        {
            Vector3 floorNormal = transform.up;
            Vector3 cameraRealForward = camera.transform.forward;
            float angleInterpolator = Mathf.Abs(Vector3.Dot(cameraRealForward, floorNormal));
            Vector3 cameraForward = Vector3.Lerp(cameraRealForward, camera.transform.up, angleInterpolator).normalized;
            Vector3 characterForward = Vector3.ProjectOnPlane(cameraForward, floorNormal).normalized;
            Debug.DrawLine(transform.position, transform.position + characterForward * 2, Color.magenta, 5);
            targetRotation = Quaternion.LookRotation(characterForward, floorNormal);
        }

        private void ApplyCharacterRotation()
        {
            float motionMagnitude = Mathf.Sqrt(speedX.TargetValue * speedX.TargetValue + speedY.TargetValue * speedY.TargetValue);
            float rotationSpeed = Mathf.SmoothStep(0, .1f, motionMagnitude);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotationSpeed);
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            Vector2 inputValue = ctx.ReadValue<Vector2>();
            speedX.TargetValue = inputValue.x;
            speedY.TargetValue = inputValue.y;
        }
        private void Update()
        {
            speedX.Update();
            speedY.Update();
            _animator.SetFloat(_speedXHash, speedX.CurrentValue);
            _animator.SetFloat(_speedYHash, speedY.CurrentValue);
            SolveCharacterRotation();
            if (!ParentCharacter.IsAiming)
                ApplyCharacterRotation();
        }
    }
}