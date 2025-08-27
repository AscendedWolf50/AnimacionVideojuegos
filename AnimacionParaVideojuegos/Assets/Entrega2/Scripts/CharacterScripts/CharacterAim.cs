using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace GA.Sessions.Class_03.Scripts
{
    public class CharacterAim:MonoBehaviour,ICharacterComponent
    {
        public Character ParentCharacter { get; set; }
        
        [SerializeField] private CinemachineCamera aimCamera;
        [SerializeField] private FloatDamper aimDamper;
        [SerializeField] private AimConstraint aimConstraint;
        [SerializeField] private Animator anim;

     

        public void OnAim(InputAction.CallbackContext ctx)
        {
            if(!ctx.started && !ctx.canceled)return;
            
            aimCamera?.gameObject.SetActive(ctx.started);
            ParentCharacter.IsAiming = ctx.started;
            aimConstraint.enabled = ctx.started;
            aimDamper.TargetValue = ctx.started ? 1 : 0;
        }

        private void Update()
        {
            aimDamper.Update();
            aimConstraint.weight = aimDamper.CurrentValue;
            anim.SetLayerWeight(1,aimDamper.CurrentValue);
        }
    }
}