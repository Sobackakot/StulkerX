using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FireMeshRenderer : MonoBehaviour
{
    private Transform trFireMesh;
    private MeshRenderer currentFireMesh;
    [SerializeField] private List<Material> fireEffectMaterials = new List<Material>();

    [Range(0.2f, 0.5f),SerializeField] private float minScale = 0.5f;
    [Range(0.5f, 1f),SerializeField] private float maxScale = 1f;
    private float time;
    private void Awake()
    {
        trFireMesh = GetComponent<Transform>();
        currentFireMesh = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        ShowFireEffect();
    }
    public void EnableRenderer(bool isKeyDownLeft)
    {
        if (isKeyDownLeft)
        {
            gameObject?.SetActive(true);
        }
    }
    public void DisableRenderer(float nextTime)
    {
        if (time < Time.time)
        {
            time = nextTime;
            gameObject?.SetActive(false);
        }
    }
    private void ShowFireEffect()
    {
        ResetTransform();
        currentFireMesh.sharedMaterial = fireEffectMaterials[Random.Range(0, fireEffectMaterials.Count)];
        trFireMesh.localScale *= Random.Range(minScale, maxScale);
        trFireMesh.rotation *= Quaternion.AngleAxis(Random.value * 360f, Vector3.forward); 
    }
     
    private void ResetTransform()
    {
        trFireMesh.localScale = Vector3.one; 
    }
}
