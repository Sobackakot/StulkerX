using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour 
{
    public interface IUnitBehaviour
    {
        void EnableBeh();
        void DisableBeh();
        void UpdateBeh();
        void LateUpdateBeh();
        void FixedUpdateBeh();
    }
}

