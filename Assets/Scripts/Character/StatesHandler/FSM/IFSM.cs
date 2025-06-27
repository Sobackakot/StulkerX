using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State.CoreFSM
{
    public interface IFSM
    {
        void Transition();
    }
}