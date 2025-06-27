using System.Collections;
using UnityEngine; 

[RequireComponent(typeof (Rigidbody))] 
public class ZombieMove : MonoBehaviour
{
    private ZombieAnim animZ;
    private Transform tarTr;
    private Rigidbody zomRb;
    private Transform zomTr;

    [Range(3, 30), SerializeField] private float minDistanceLoockTarget = 30;
    [Range(3, 20), SerializeField] private float minDistanceFollowTarget = 15;
    [Range(0.5f, 5), SerializeField] private float minDistanceAttackTarget = 3;
    [Range(3, 8), SerializeField] private float timer = 5f;
    [Range(3, 8), SerializeField] private float waitIdle = 3f;
    [Range(15, 45), SerializeField] private float minAngle = 30f;
    [Range(60, 120), SerializeField] private float maxAngle = 120f;
    [Range(3, 6), SerializeField] private float speedMove = 5f;
    [Range(1, 45), SerializeField] private float angleRotate = 3f;
     
    private Vector3 targetMove;
    private Quaternion targetRotation;

    [field: SerializeField] public bool isScreamer { get; private set; }
    [field: SerializeField] public bool isLoockTarget { get; private set; }
    [field: SerializeField] public bool isFollowTarget { get; private set; }
    [field: SerializeField] public bool isAttackTarget { get; private set; }

    [field: SerializeField] public bool isIdle { get; private set; }
    [field: SerializeField] public bool isRundomMove { get; private set; }
    [field: SerializeField] public bool isRundomRotate { get; private set; }
    

    private void Awake()
    {
        zomRb = GetComponent<Rigidbody>();
        zomTr = GetComponent<Transform>();
        tarTr = FindObjectOfType<CharacterInspector>().transform;
        animZ = GetComponent<ZombieAnim>();
    }
    private void OnEnable()
    {
        isScreamer = true;
    }
    private void Update()
    {
        isRundomMove = !isIdle && !isFollowTarget;
        isRundomRotate = !isLoockTarget;
        isLoockTarget = IsMinDistance(minDistanceLoockTarget);
        isAttackTarget = IsMinDistance(minDistanceAttackTarget);
        isFollowTarget = IsMinDistance(minDistanceFollowTarget);
        RandomTimer();
        AttackState();
        IdleState();
        ScreamerState();
    }
    void FixedUpdate()
    {
        if (isAttackTarget) return;
        RandomRotateState();
        TargetRotateState();
        RandomMoveState();
        TargetMoveState(); 
    }

   
    private void ScreamerState()
    {
        if (isLoockTarget && isScreamer)
        {
            isScreamer = false;
            animZ.ScreamAnim();
            StartCoroutine(IdleWaitTime());
        } 
    } 
    private void AttackState()
    {
        if (isAttackTarget)
        {
            StartCoroutine(IdleWaitTime());
            animZ.AttackAnim(); 
        } 
    }
    private void IdleState()
    {
        if (isIdle && !isRundomMove && !isFollowTarget)
        {
            animZ.MoveAnim(0);
            StartCoroutine(IdleWaitTime());
        } 
    }
    
    private void TargetMoveState()
    {
        if (!isIdle && isFollowTarget && !isScreamer && !isAttackTarget)
        {
            animZ.MoveAnim(1);
            Moving(targetMove);
        } 
    }
    private void TargetRotateState()
    {
        if (isLoockTarget)
        {
            targetMove = (tarTr.position - zomTr.position).normalized;
            targetRotation = Quaternion.LookRotation(new Vector3(targetMove.x, 0, targetMove.z));
            Rotating(targetRotation);
        }
    }
    private void RandomMoveState()
    {
        if (!isIdle && isRundomMove && !isAttackTarget)
        {
            animZ.MoveAnim(1);
            Moving(zomTr.forward);
        }
    } 
    private void RandomRotateState()
    {
        if (isRundomRotate)
        {
            float currentY = zomTr.eulerAngles.y;
            float turnAmount = Random.Range(minAngle, maxAngle);
            if (Random.value > 0.5f) turnAmount *= -1; 
            float newY = currentY + turnAmount;
            targetRotation = Quaternion.Euler(0, newY, 0);
            Rotating(targetRotation);
        } 
    }

   

    private IEnumerator IdleWaitTime()
    {
        isIdle = true; 
        yield return new WaitForSeconds(waitIdle);
        isIdle = false; 
    }
    private void RandomTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(3, 10);
            isIdle = !isFollowTarget;
        }
    }
    private void Rotating(Quaternion targetRotation)
    { 
        Quaternion newRot = Quaternion.Slerp(zomTr.rotation, targetRotation, angleRotate * Time.fixedDeltaTime);
        zomRb.MoveRotation(newRot);
    }
    private void Moving(Vector3 targetMove)
    {
        zomRb.MovePosition(zomRb.position + targetMove.normalized * speedMove * Time.fixedDeltaTime);
    }
    private bool IsMinDistance(float MinDistance)
    {
        float distance = Vector3.Distance(zomTr.position, tarTr.position);
        return distance <= MinDistance;
    }
}
