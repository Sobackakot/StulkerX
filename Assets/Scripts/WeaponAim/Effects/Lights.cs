 
using UnityEngine;

public class Lights : MonoBehaviour
{ 
    float time;
    public float LiveTime = 0.13f;
    public void EnableLight(bool isKeyDownLeft)
    {
        if (isKeyDownLeft)
        {
            gameObject.SetActive(true);
        }
    }
    public void DisableLight(float nextTime)
    {
        if (time < Time.time)
        {
            time = nextTime; 
            gameObject.SetActive(false);
        }
    }
}
