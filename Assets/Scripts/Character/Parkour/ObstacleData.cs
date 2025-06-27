 
using UnityEngine;

[CreateAssetMenu(fileName = "new Obstacle", menuName = "Obstacle")]
public class ObstacleData : ScriptableObject
{
    public float maxWidth;
    public float minWidth;
    public float maxHeight;
    public float minHeight;   
    public string nameStateAnim = "";
    public Quaternion targetRotate {  get; private set; }
    public Vector3 matchPoint { get; private set; }

    [Header("Target Match")]
    [SerializeField] private AvatarTarget matchBody;
    [SerializeField] private Vector3 matchPosWeight = new Vector3(0, 1, 0); 
    [SerializeField] private float startTime;
    [SerializeField] private float targetTime;


    
    public bool CheckHeightObstacle(RaycastHit hitForward,RaycastHit hitDown, Transform charTrans)
    {
        float height = hitDown.point.y - charTrans.position.y;
        if (height > maxHeight || height < minHeight) return false;
        else
        { 
            targetRotate = Quaternion.LookRotation(-hitForward.normal);
            matchPoint = hitDown.point;
            return true;
        }
    }
    public AvatarTarget MatchBody => matchBody;
    public Vector3 MatchPosWeight => matchPosWeight;
    public float StartTime => startTime;
    public float TargetTime => targetTime;
}
 
