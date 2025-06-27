using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public interface IState
    {
        void EnterState();
        void ExitState();
        void UpdateState();
        void LateUpdateState();
        void FixedUpdateState();
    }
}

