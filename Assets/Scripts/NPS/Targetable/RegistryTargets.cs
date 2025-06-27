using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC.Target
{
    public class RegistryTargets
    {
        private readonly List<ITargetable> activeTargets = new();

        public void RegisterTarget(ITargetable target)
        {
            if (!activeTargets.Contains(target))
            {
                activeTargets.Add(target);
            }
        }
        public void UnregisterTarget(ITargetable target)
        {
            if (activeTargets.Contains(target))
            {
                activeTargets.Remove(target);
            }
        }
        public List<ITargetable> GetTargets() => activeTargets;
    }
}

