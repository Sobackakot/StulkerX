using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace State.CoreFSM
{
    public interface IFSM
    { 
        void TransitionFSM();
        void UpdateFSM();
        void LateUpdateFSM();
        void FixedUpdateFSM();
    }
}