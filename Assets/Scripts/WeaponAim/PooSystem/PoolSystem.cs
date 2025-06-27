using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 

public class PoolSystem : MonoBehaviour
{
    private Queue<Bullet> poolBullets = new Queue<Bullet>();
    [SerializeField] private GameObject bulletPrefub;
    private Transform poolTrans;
    private int poolCount = 100;

    private void Awake()
    {
        poolTrans = GetComponent<Transform>();
        InitializePool(); 
    }

    public Bullet ActiveObject(Vector3 position, Quaternion rotation)
    {
        Bullet bullet;
        if(poolBullets.Count > 0)
        {
            bullet = poolBullets.Dequeue();
            bullet.gameObject.SetActive(true);
        }
        else bullet = CreateNewObject();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        return bullet;
    }
    public void ReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); 
        bullet.transform.position = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;    
        poolBullets.Enqueue(bullet); 
    }
    private void InitializePool()
    {
        for(int i = 0; i < poolCount; i++)
        {
            Bullet newBullet = CreateNewObject();
            newBullet.gameObject.SetActive(false);
            poolBullets.Enqueue(newBullet);
        }
    }
    private Bullet CreateNewObject()
    {
        GameObject newObject = Instantiate(bulletPrefub);
        newObject.transform.SetParent(poolTrans); 
        return newObject.GetComponent<Bullet>();
    }
}
