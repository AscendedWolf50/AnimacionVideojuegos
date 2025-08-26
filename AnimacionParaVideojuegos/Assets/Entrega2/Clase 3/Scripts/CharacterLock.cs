#if UNITY_EDITOR
using UnityEditor; // para Handles en el editor
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GA.Sessions.Class_03.Scripts
{
    public class CharacterLock : MonoBehaviour, ICharacterComponent
    {
        [Header("Detección")]
        [SerializeField] private Camera camera;
        [SerializeField] private LayerMask detectionMask;
        [SerializeField] private float detectionRadius = 15f;
        [SerializeField] private float detectionAngle = 60f; 

        [Header("Opcional (solo para dibujar gizmos)")]
        [SerializeField] private Color gizmoColor = new Color(0.1f, 0.6f, 1f, 0.6f);

        public Character ParentCharacter { get; set; }

       
        private List<Transform> candidates = new List<Transform>();
        private int currentIndex = -1;

        
        // OnLock
        public void OnLock(InputAction.CallbackContext ctx)
        {
            if (!ctx.started) return;

           
            if (ParentCharacter != null && ParentCharacter.LockTarget != null)
            {
                ParentCharacter.LockTarget = null;
                currentIndex = -1;
                return;
            }

           
            Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);
            if (detectedObjects.Length == 0) return;

            int closestIndex = -1;
            float bestScore = float.MaxValue; // menor = mejor

            Vector3 camForward = camera != null ? camera.transform.forward : transform.forward;
            for (int i = 0; i < detectedObjects.Length; i++)
            {
                Collider col = detectedObjects[i];
                if (col == null) continue;
                if (col.transform == transform) continue;

                Vector3 dirFromCamera = col.transform.position - (camera != null ? camera.transform.position : transform.position);
                float angle = Vector3.Angle(camForward, dirFromCamera.normalized);

               
                if (angle > detectionAngle) continue;

                float distance = Vector3.Distance(transform.position, col.transform.position);

               
                float score = angle * 10f + distance; 
                if (score < bestScore)
                {
                    bestScore = score;
                    closestIndex = i;
                }
            }

            if (closestIndex == -1)
            {
                
                float bestDist = float.MaxValue;
                for (int i = 0; i < detectedObjects.Length; i++)
                {
                    float d = Vector3.Distance(transform.position, detectedObjects[i].transform.position);
                    if (d < bestDist)
                    {
                        bestDist = d;
                        closestIndex = i;
                    }
                }
            }

            if (closestIndex != -1)
            {
                ParentCharacter.LockTarget = detectedObjects[closestIndex].transform;

                
                UpdateCandidates();
                currentIndex = candidates.IndexOf(ParentCharacter.LockTarget);
                if (currentIndex < 0) currentIndex = 0;
            }
        }

        // OnSwitchTarget: vinculado al input para "cambiar objetivo" (ej. Q)
        public void OnSwitchTarget(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            NextTarget();
        }

       
        public void OnNextTarget()
        {
            NextTarget();
        }

        // --- alternar ---
        private void NextTarget()
        {
            UpdateCandidates();

            if (candidates.Count == 0)
            {
                ParentCharacter.LockTarget = null;
                currentIndex = -1;
                return;
            }

         
            if (ParentCharacter.LockTarget == null)
            {
                currentIndex = 0;
                ParentCharacter.LockTarget = candidates[currentIndex];
                return;
            }

           
            int idx = candidates.IndexOf(ParentCharacter.LockTarget);
            if (idx == -1)
            {
               
                currentIndex = 0;
                ParentCharacter.LockTarget = candidates[currentIndex];
                return;
            }

            // avanza circularmente
            currentIndex = (idx + 1) % candidates.Count;
            ParentCharacter.LockTarget = candidates[currentIndex];
        }

       
        private void UpdateCandidates()
        {
            candidates.Clear();
            Collider[] cols = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);
            foreach (var col in cols)
            {
                if (col == null) continue;
                if (col.transform == transform) continue;
                candidates.Add(col.transform);
            }

            if (camera == null) return;

           
            Vector3 camForward = Vector3.ProjectOnPlane(camera.transform.forward, camera.transform.up).normalized;
            Vector3 camUp = camera.transform.up;

            candidates.Sort((a, b) =>
            {
                Vector3 da = Vector3.ProjectOnPlane(a.position - camera.transform.position, camUp).normalized;
                Vector3 db = Vector3.ProjectOnPlane(b.position - camera.transform.position, camUp).normalized;
                float angleA = Vector3.SignedAngle(camForward, da, camUp);
                float angleB = Vector3.SignedAngle(camForward, db, camUp);
                return angleA.CompareTo(angleB);
            });
        }

       
        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

           
            UpdateCandidates();

            Gizmos.color = Color.yellow;
            for (int i = 0; i < candidates.Count; i++)
            {
                var t = candidates[i];
                if (t == null) continue;
                Gizmos.DrawLine(transform.position, t.position);
                Gizmos.DrawSphere(t.position, 0.12f);
#if UNITY_EDITOR
                Handles.Label(t.position + Vector3.up * 0.25f, $"{t.name} [{i}]");
#endif
            }

            if (ParentCharacter != null && ParentCharacter.LockTarget != null)
            {
                Gizmos.color = Color.red;
                var tgt = ParentCharacter.LockTarget;
                Gizmos.DrawLine(transform.position, tgt.position);
                Gizmos.DrawSphere(tgt.position, 0.22f);
#if UNITY_EDITOR
                Handles.Label(tgt.position + Vector3.up * 0.4f, $"LOCK -> {tgt.name}");
#endif
            }
        }
    }
}
