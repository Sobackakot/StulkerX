using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC.Target
{
    public interface ITargetable
    {
        Transform targetTr { get; set; }
        bool IsAlive();
        void OnFocused(Transform npcTr);
        void OnDefocus();
        void InteractOnNewPoint();
        TargetType TargetType { get; set; }

    }

    public enum TargetType
    {
        Player,
        Enemy,
        Ally,
        Neutral,
        Objective
    }
}


