using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string audio)
    {
        var Clip = Resources.Load("Sounds/" + audio) as AudioClip;
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Clip);
        }
    }

    public void PlaySoundInterrupt(string audio)
    {
        var Clip = Resources.Load("Sounds/" + audio) as AudioClip;
        audioSource.PlayOneShot(Clip);     
    }

    public void FootstepLarge(AnimationEvent animationEvent)
    {
        var Clip = Resources.Load("Sounds/LargeFootsteps") as AudioClip;
        if(!audioSource.isPlaying && animationEvent.animatorClipInfo.weight > 0.5)
            audioSource.PlayOneShot(Clip,Random.Range(.3f,.5f));
    }

}