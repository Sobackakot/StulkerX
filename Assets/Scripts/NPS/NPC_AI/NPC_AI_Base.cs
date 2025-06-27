
using NPC.Data;
using NPC.Main; 
using NPC.Target;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace NPC
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class NPC_AI_Base : MonoBehaviour
    {
        [Inject]
        private void Container(RegistryTargets registryTargets, RegistryNPC npcRegistr)
        {  
            this.registryTargets = registryTargets;
            this.npcRegistr = npcRegistr;
            data = new NPC_Data(); 
        } 
        private RegistryTargets registryTargets;
        private RegistryNPC npcRegistr;
        public NPC_Data data { get; private set; }

        public TargetsHandler target { get; private set; } 
        public ITargetable currentTarget { get; private set; }
        public Transform npcTr { get; private set; }
        public Rigidbody npcRb { get; private set; }
          
        public NavMeshAgent agent { get; private set; } 
        public Transform targetTr { get; private set; } 
       
        [field: SerializeField] public float rotateAngle { get; private set; } = 4f;

        public float speedWalk { get; private set; } = 2.5f;
        public float speedRan { get; private set; } = 5f;
        public float speedSprint { get; private set; } = 7f;

        public float minRadiusTrigger { get; private set; } = 2f; 
        public float visionRadius { get; private set; } = 20f;
        public float viewAngle { get; private set; } = 120f;
        public float minAngleRotate { get; private set; } = 45f;
        public float maxAngleRotate { get; private set; } = 125f;

       

        public Vector3 inputAxis { get; private set; } 

        private void Awake()
        {  
            npcRb = GetComponent<Rigidbody>();
            npcTr = GetComponent<Transform>();
            agent = GetComponent<NavMeshAgent>();
            target = new TargetsHandler(registryTargets, GetComponentInChildren<RaycastNPC>());
        }
        private void OnEnable()
        {
            npcRegistr.AddNPC(this);
        }
        private void OnDisable()
        {
            npcRegistr.RemoveNPC(this);
        } 
      
        public void StoppedDestination()
        {
            float distance = Vector3.Distance(npcTr.position, targetTr.position);
            if (distance <= 2)
            {
                agent.velocity = Vector3.zero;
                ResetFocus(); 
            }
        }
        public void SetFocus(ITargetable newTarget)
        { 
            currentTarget?.OnDefocus();
            currentTarget = newTarget; 
            targetTr = newTarget.targetTr;// null references after the function is triggered
            currentTarget?.OnFocused(npcTr); 
        }
        public void ResetFocus()
        { 
            currentTarget?.OnDefocus();
            currentTarget = null;
            targetTr =null; 
        }
        public bool IsMinDistance(float minDistance)
        {
            float distance = Vector3.Distance(npcTr.position, targetTr.position);
            if (distance < minDistance) return true;
            else return false;
        }
    }
}


