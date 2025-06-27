 
using UnityEngine;

public class WeaponEffects : MonoBehaviour
{  
    [SerializeField] private ParticleSystem trailBulets;
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private ParticleSystem smokeStart;
    [SerializeField] private ParticleSystem smokeEnd;
     
     
    public void PlayParticleShooting(bool isKeyDownLeft)
    {
        if (isKeyDownLeft)
        {
            trailBulets.Play();
            fireEffect.Play();
            smokeStart.Play();
        }
    } 
}
