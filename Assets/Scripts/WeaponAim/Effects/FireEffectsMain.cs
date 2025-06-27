 
using State.Character;
using StateData.Character;
using UnityEngine;

public class FireEffectsMain  
{   
    public FireEffectsMain(
        Audios audios,
        CharacterStateContext stateData, 
        WeaponEffects particles, 
        Lights lights,
        FireMeshRenderer fireMesh)
    {
        this.audios = audios;
        this.stateData = stateData;
        this.particles = particles;
        this.lights = lights;
        this.fireMesh = fireMesh;
    }
    private Audios audios;
    private CharacterStateContext stateData;
    private WeaponEffects particles;
    private Lights lights;
    private FireMeshRenderer fireMesh;

    private float nextTime;
    private float intervalTime = 1;
    private float coefficient = 9.8f;
 
    public void LateTick()
    {
        if (stateData.isAim && nextTime < Time.time)
        {
            nextTime = Time.time + intervalTime / coefficient;
            particles.PlayParticleShooting(stateData.isFire);
            audios.PlayAudioShooting(stateData.isFire);
            lights.EnableLight(stateData.isFire);
            fireMesh.EnableRenderer(stateData.isFire);
        }
        else
        {
            lights.DisableLight(nextTime);
            fireMesh.DisableRenderer(nextTime);
        }
    } 
}
