using Character.Context;
using Character.InputEvents;
using Character.MainCamera;
using Character.MainCamera.Raycast;
using StateData.Character;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class CharacterInspector : MonoBehaviour 
{
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject capOnHead;
     
    [SerializeField] public string OnCollisionTag = "Parkour";

    public readonly List<ObstacleData> obstaclesData = new List<ObstacleData>();
    public Animator animator { get; set; }
    public StateMachineAnimator stateMachin { get; set; }
    public Transform charTrans { get; set; }

    public IRaycastHitFPS raycastHitFPS { get; set; }
    public IRaycastHitItem raycastHitItem { get; set; }
    public IRaycastHitLootBox raycastHitLootBox { get; set; }
    public IRaycastHitParcour raycastHitParcour { get; set; }

    public Rigidbody rbCharacter { get; set; } 
    public Transform targetAim { get; private set; }
    public FreeCameraCharacter tirdCam { get; private set; }
    public FirstCameraCharacter firstCam { get; private set; }
    public Transform currCamTr { get; private set; }
    public Transform firstCamTr { get; private set; }
    public Transform tirdCamTr { get; private set; }

    public float speedSprint { get; set; } = 5f;
    public float speedRunForward { get; set; } = 3f;
    public float speedRunBack { get; set; } = 2f;
    public float speedWalkForward { get; set; } = 1.5f;
    public float speedWalkBack { get; set; } = 0.5f;
    public float jumpForce { get; set; } = 3f;
    public float angleRotate { get; set; } = 45f;

    public Vector3 newDirection { get; set; }
    public Vector3 directionForward { get; set; }
    public Vector3 directionRight { get; set; }
    public Vector3 inputAxis { get; set; }


    public FireEffectsMain weaponEfect { get; private set; }
    public IContextStates contextStates { get; private set; }
    public IContextCommands contextCommands { get; private set; }
    public IInputEvents inputEvent { get; private set; }

    [Inject] 
    private void Construct(IContextStates contextStates, IContextCommands contextCommands, IInputEvents inputEvent, FireEffectsMain weaponEfect)
    {
        this.contextStates = contextStates;
        this.contextCommands = contextCommands;
        this.inputEvent = inputEvent;
        this.weaponEfect = weaponEfect;
    }

    private void Awake()
    {
        rbCharacter = GetComponent<Rigidbody>();

        targetAim = FindObjectOfType<TargetRayPointAim>()?.transform;

        tirdCam = FindFirstObjectByType<FreeCameraCharacter>();
        tirdCamTr = tirdCam?.transform;

        firstCam = FindObjectOfType<FirstCameraCharacter>();
        firstCamTr = firstCam?.transform;

        currCamTr = tirdCamTr;

        charTrans = GetComponent<Transform>();

        raycastHitFPS = FindObjectOfType<RaycastPointCamera>();
        raycastHitItem = FindObjectOfType<RaycastPointCamera>();
        raycastHitLootBox = FindObjectOfType<RaycastPointCamera>();
        raycastHitParcour = FindObjectOfType<RaycastPointCamera>();

        animator = GetComponent<Animator>();
        stateMachin = animator?.GetBehaviour<StateMachineAnimator>(); 
    }
    private void OnEnable()
    {
        if (inputEvent == null) return;
        inputEvent.OnSwichCamera += SetActiveCamera;
    }
    private void OnDisable()
    {
        if (inputEvent == null) return;
        inputEvent.OnSwichCamera -= SetActiveCamera;
    }
    public void UpdateDirectionMove()
    { 
        inputAxis = contextStates.InputAxis;
        directionForward = Vector3.ProjectOnPlane(currCamTr.forward, Vector3.up).normalized;
        directionRight = Vector3.ProjectOnPlane(currCamTr.right, Vector3.up).normalized;
        newDirection = (inputAxis.z * directionForward) + (inputAxis.x * directionRight).normalized; 
    }
    public void SetActiveCamera()
    {
        bool isActive = contextStates.IsFirstCamera;
        firstCam.enabled = isActive;
        tirdCam.enabled = !isActive;
        head.SetActive(!isActive);
        capOnHead.SetActive(!isActive);
        currCamTr = contextStates.IsFirstCamera ? firstCamTr : tirdCamTr;
    }


    private void OnCollisionStay(Collision collision)
    {
        contextCommands.SetIsCollision(true);
    }
    private void OnCollisionExit(Collision collision)
    {
        contextCommands.SetIsCollision(false);
    }  
}
