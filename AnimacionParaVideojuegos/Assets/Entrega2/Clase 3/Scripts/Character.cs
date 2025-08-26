using System;
using GA.Sessions.Class_03.Scripts;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Character : MonoBehaviour
{
    private Transform lockTarget;
    private bool isAiming;

    public Transform LockTarget
    {
        get => lockTarget;
        set => lockTarget = value;
    }

    public bool IsAiming
    {
        get => isAiming;
        set => isAiming = value;
    }

    private void Awake()
    {
        RegisterComponents();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RegisterComponents()
    {
        foreach (ICharacterComponent characterComponent in GetComponentsInChildren<ICharacterComponent>())
        {
            characterComponent.ParentCharacter = this;
        }
    }
}
