using UnityEngine;


[RequireComponent(typeof(Animator))]
public class ZombieAnim : MonoBehaviour
{
    private Animator animZ;
    private void Awake()
    {
        animZ = GetComponent<Animator>();
    }
    public void MoveAnim(float move)
    {
        animZ.SetFloat("Move", Mathf.Abs(move), 0.2f, Time.deltaTime);
    }
    public void AttackAnim()
    {
        animZ.SetTrigger("AttackTrigger");
    }
    public void ScreamAnim()
    {
        animZ.SetTrigger("ScreamTrigger");
    }
    public void DeadAnim()
    {
        animZ.SetTrigger("DeadTrigger");
    }
}
