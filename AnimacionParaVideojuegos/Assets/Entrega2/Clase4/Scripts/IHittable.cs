using UnityEngine;

namespace GA.Sessions.Class_04.Scripts
{
    public interface IHittable
    {
        void ApplyHit(HitInfo hitInfo);
    }

    public struct HitInfo
    {
        public float damage;
        public Vector3 point;
        public Vector3 normal;
    }
}