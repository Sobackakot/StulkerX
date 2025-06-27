 
using UnityEngine;
using Zenject;

namespace NPC.Target
{
    public class TargetInspector : MonoBehaviour, ITargetable
    {
        [Inject]
        private void Container(RegistryTargets regist)
        {
            this.regist = regist;
        }
        private RegistryTargets regist;
        public Transform targetTr { get; set; }
        private Transform npcTr;

        public readonly float rangePoint = 3;
        public bool isHasInteract { get; private set; }
        public bool isFocus { get; private set; }

        [field : SerializeField] public TargetType TargetType { get; set; }
       

        public bool isAlive { get; private set; }
        private void Awake()
        {
            targetTr = GetComponent<Transform>();  
        }
        private void OnEnable()
        {
            isAlive = true;
            regist.RegisterTarget(this);
        }
        private void OnDisable()
        {
            isAlive = false;
            regist.UnregisterTarget(this);
        }

        private void FixedUpdate()
        { 
            if (isFocus && !isHasInteract)
            {
                float distance = Vector3.Distance(targetTr.position, npcTr.position);
                if(distance <= rangePoint)
                {
                    isHasInteract = true;
                    InteractOnNewPoint();
                }
            }
        }
        public void InteractOnNewPoint()
        { 
        }
        public bool IsAlive()
        {
            return isAlive;
        }
        public void OnFocused(Transform npcTr)
        { 
            this.npcTr = npcTr;
            isHasInteract = false;
            isFocus = true;
        }
        public void OnDefocus()
        {
            npcTr = null;
            isHasInteract = true;
            isFocus = false;
        } 
    } 
}
