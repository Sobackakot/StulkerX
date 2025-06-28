using System.Collections.Generic;

namespace NPC.Target
{
    public interface IRegistryTarget
    {
        List<ITargetable> activeTargets { get; set; }
        void RegisterTarget(ITargetable target);
        void UnregisterTarget(ITargetable target);
        List<ITargetable> GetTargets();
    }
}