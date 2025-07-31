using UnityEngine;

namespace GA.Sessions.Class_02.Scripts.Input
{
    public class PlayerInput : ICharacterInput
    {
        public float GetSpeedInput()
        {
            return new Vector2(
                UnityEngine.Input.GetAxis("Horizontal"),
                UnityEngine.Input.GetAxis("Vertical")
            ).magnitude;
        }
    }
}