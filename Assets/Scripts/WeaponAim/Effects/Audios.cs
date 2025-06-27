 
using UnityEngine;
 
public class Audios : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
   

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioShooting(bool isKeyDownLeft)
    {
        if(isKeyDownLeft)
        {    
            audioSource.PlayOneShot(audioClip); 
        }
    }
}
