using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC.Target
{
    public class RegistryTargets: IRegistryTarget
    {
        public RegistryTargets()
        {
            activeTargets = new();
        }

        public List<ITargetable> activeTargets { get; set; }

        public void RegisterTarget(ITargetable target)
        {
            if (!activeTargets.Contains(target))
            {
                activeTargets?.Add(target);
            }
        }
        public void UnregisterTarget(ITargetable target)
        {
            if (activeTargets.Contains(target))
            {
                activeTargets?.Remove(target);
            }
        }
        public List<ITargetable> GetTargets() => activeTargets;
    }
}

