using System.Collections.Generic;

namespace NPC 
{
    public class RegistryNPC: IRegistryNPC
    {
        public RegistryNPC()
        {
            npcList = new();
        }
        public List<NPC_AI_Base> npcList { get; set; }

        public void AddNPC(NPC_AI_Base npc)
        {
            if (!npcList.Contains(npc))
            {
                npcList?.Add(npc);
            }
        }
        public void RemoveNPC(NPC_AI_Base npc)
        {
            if (npcList.Contains(npc))
            {
                npcList?.Remove(npc);
            }
        }
        public List<NPC_AI_Base> GetListNPC()
        {
            return npcList;
        }
    } 
}
