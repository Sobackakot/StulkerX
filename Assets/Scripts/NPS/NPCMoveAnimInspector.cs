using UnityEngine;
using UnityEngine.AI;

public class NPCMoveAnimInspector : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        MoveAnimation();
    }
    public void MoveAnimation()
    {  
        if (agent.velocity.sqrMagnitude > 0.2f)
        {
            anim.SetFloat("X", SpeedCalculate(), 0.2f, Time.smoothDeltaTime);
            anim.SetFloat("Y", SpeedCalculate(), 0.2f, Time.smoothDeltaTime);
        }
        else
        {
            anim.SetFloat("Y", 0, 0.2f, Time.smoothDeltaTime);
            anim.SetFloat("X", 0, 0.2f, Time.smoothDeltaTime);
        }
         
    }
    private float SpeedCalculate()
    {
        return agent.velocity.magnitude / agent.speed;
    }
}
