using System.Collections;
using System.Collections.Generic; 

namespace NPC 
{
    public class RegistryNPC
    {
        public readonly List<NPC_AI_Base> npcList = new();

        public void AddNPC(NPC_AI_Base npc)
        {
            if (!npcList.Contains(npc))
            {
                npcList.Add(npc);
            }
        }
        public void RemoveNPC(NPC_AI_Base npc)
        {
            if (npcList.Contains(npc))
            {
                npcList.Remove(npc);
            }
        }
        public List<NPC_AI_Base> GetListNPC()
        {
            return npcList;
        }
    } 
}
