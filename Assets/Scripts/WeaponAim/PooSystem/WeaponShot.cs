
using System.Collections.Generic;
using UnityEngine;

public class WeaponShot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    private List<Bullet> activeBullets = new List<Bullet>();
     
    private PoolSystem poolSystem;
    [field : SerializeField] private float speedMove { get; set; }
    [field : SerializeField] private float distanceMove { get; set; }
    private void Awake()
    {
        poolSystem = FindObjectOfType<PoolSystem>();
    }
    private void Start()
    {
        activeBullets.AddRange(FindObjectsOfType<Bullet>());
    }
    private void Update()
    {
        Shooting();
        UpdateBullet();
    }
    public void UpdateBullet()
    { 
        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = activeBullets[i];
            bool isFinal = bullet.MoveBullet();
            if (isFinal)
            {
                poolSystem.ReturnToPool(bullet);
                activeBullets.RemoveAt(i);
            }
        }
    }
    public void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bullet newBullet = poolSystem.ActiveObject(spawnPoint.position, spawnPoint.rotation);
            newBullet.InitializeBullet(spawnPoint.position,speedMove, distanceMove);
            activeBullets.Add(newBullet); 
        }
    }
}
