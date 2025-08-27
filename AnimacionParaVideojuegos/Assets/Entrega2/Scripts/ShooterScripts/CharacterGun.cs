using System;
using GA.Sessions.Class_03.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace GA.Sessions.Class_04.Scripts
{
    public class CharacterGun : MonoBehaviour, ICharacterComponent
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] Animator animator;
        [SerializeField] RecoilCameraKick recoilCameraKick;

        [SerializeField] private bool automatic = true;
        [SerializeField] private bool requireAim = true;
        [SerializeField] private float fireRate = 10f;
        [SerializeField] private Transform traceOrigin;
        [SerializeField] private float range = 200f;
        [SerializeField] private LayerMask hitMask;
        [SerializeField] private float debugDuration = 0.2f;


        [Header("Camera Recoil Info")]
        [SerializeField] private float camShakeRecoil = 0.6f;
        [SerializeField] private float cameraKick = 0.12f;
        [SerializeField] private float cameraRecover = 0.18f;

        private bool _isFiring = false;
        private float _nextShootTime;
        public Character ParentCharacter { get; set; }

        public void OnFire(InputAction.CallbackContext ctx)
        {
            if (ctx.started) _isFiring = true;
            if (ctx.canceled) _isFiring = false;
            if (!automatic && ctx.performed) TryShoot();
        }
        private void Update()
        {
            if (automatic && _isFiring) TryShoot();
        }

        private void TryShoot()
        {
            if (requireAim && (ParentCharacter == null || !ParentCharacter.IsAiming)) return;
            if (Time.time < _nextShootTime) return;
            _nextShootTime = Time.time + 1f / Mathf.Max(1f, fireRate);
            ShootOnce();
        }

        private void ShootOnce()
        {
            if (animator) animator.SetTrigger("Fire");
            if (recoilCameraKick) recoilCameraKick.Kick(camShakeRecoil, peak: cameraKick, recover:cameraRecover);

            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            Vector3 from = traceOrigin ? traceOrigin.position : ray.origin;

            if (Physics.Raycast(ray, out var hit, range, hitMask, QueryTriggerInteraction.Ignore))
            {
                Vector3 to = hit.point;
                Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(ray.origin, to), Color.magenta, debugDuration);
                Debug.DrawLine(from, to, Color.yellow, debugDuration);

                var info = new HitInfo { damage = 10f, point = hit.point, normal = hit.normal };
                if (hit.collider.TryGetComponent<IHittable>(out var h))
                {
                    h.ApplyHit(info);
                }
            }
            else
            {
                Vector3 to = ray.origin + ray.direction * range;
                Debug.DrawRay(ray.origin, ray.direction * range, Color.gray, debugDuration);
                Debug.DrawLine(from, to, Color.cyan, debugDuration);
            }
        }
    }
}