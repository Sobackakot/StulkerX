using System.Collections.Generic;

namespace NPC
{
    public interface IRegistryNPC
    {
        List<NPC_AI_Base> npcList { get; set; }
        void AddNPC(NPC_AI_Base npc);
        void RemoveNPC(NPC_AI_Base npc);
        List<NPC_AI_Base> GetListNPC();
    }
}

