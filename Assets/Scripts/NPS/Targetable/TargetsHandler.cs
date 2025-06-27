using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPC.Target 
{
    public class TargetsHandler
    { 
        public TargetsHandler(RegistryTargets registry, RaycastNPC ray)
        {
            this.registry = registry;
            this.ray = ray;
        }
        private readonly RegistryTargets registry;
        private readonly RaycastNPC ray;
        public List<ITargetable> nearTargets = new List<ITargetable>();
        public List<ITargetable> detectedTargets = new List<ITargetable>();
        public List<ITargetable> rayHitTargets = new List<ITargetable>();
        public ITargetable CurrentTarget { get; private set; }

        public event Action onNearbyEnemy;
        public event Action onDetectedEnemy; 
        public event Action onRayHitEnemy; 
        public List<ITargetable> GetNearbyTargets()
        {
            return nearTargets;
        }
        public List<ITargetable> GetDetectedTargets()
        {
            return detectedTargets;
        }
        public List<ITargetable> GetRaycastHitTargets()
        {
            return rayHitTargets;
        }

        public ITargetable GetTarget => CurrentTarget;
        public void SearchNearbyTargets(Vector3 npcPosition, float visionRadius)
        {
            List<ITargetable> targets = registry.GetTargets();
            foreach (var target in targets)
            {
                if (target != null && target.IsAlive() && IsMinRadius(npcPosition, target,visionRadius))
                {
                    if (nearTargets.Contains(target)) return;
                    if (IsEnemy(target.TargetType))
                    {
                        nearTargets.Add(target); 
                    }
                        
                }
            } 
            if (nearTargets.Count >0)
                onNearbyEnemy?.Invoke();
        }
        public void SearchDetectedTargets(Vector3 npcPosition, Vector3 npcDirection, float viewAngle)
        {
            foreach (var target in nearTargets)
            { 
                if (IsMinAngle(target, npcPosition, npcDirection, viewAngle))
                {
                    if (detectedTargets.Contains(target)) return;
                    detectedTargets.Add(target);
                    
                }
            } 
            if (detectedTargets.Count > 0)
            {
                nearTargets.Clear();
                onDetectedEnemy?.Invoke();
            } 
        } 
        public void SearchRaycastHitTargets()
        {
            foreach(var target in detectedTargets)
            {
                if (!rayHitTargets.Contains(target) && ray.RaycastForward(target.targetTr.position))
                {
                    rayHitTargets.Add(target); 
                } 
            } 
            if (rayHitTargets.Count > 0)
            {
                detectedTargets.Clear();
                onRayHitEnemy?.Invoke();
            }    
        }
        public void SearchBestTarget(Vector3 npcPosition)
        {
            float bestScore = float.MaxValue;
            ITargetable best = null;
            foreach (var target in rayHitTargets)
            {
                float score = Vector3.Distance(npcPosition, target.targetTr.position) + GetPriorityWeight(target.TargetType);
                if (score < bestScore)
                {
                    bestScore = score;
                    best = target;
                }
            }
            CurrentTarget = best; 
        }
        private bool IsEnemy(TargetType targetType)
        {
             return targetType == TargetType.Enemy; 
        }
        private bool IsMinAngle(ITargetable target, Vector3 npcPosition,Vector3 npcDirection,  float viewAngle)
        {
            Vector3 direction = (target.targetTr.position - npcPosition).normalized;
            float angle = Vector3.Angle(npcDirection, direction);
            return angle < viewAngle / 2f;
        }
        private bool IsMinRadius(Vector3 npcPosition, ITargetable target, float visionRadius)
        {
            return Vector3.Distance(npcPosition, target.targetTr.position) <= visionRadius;
        }
        private float GetPriorityWeight(TargetType type)
        {
            return type switch
            {
                TargetType.Player => 5f,
                TargetType.Enemy => 10f,
                TargetType.Objective => 15f,
                _ => 20f
            };
        } 
    }
}


