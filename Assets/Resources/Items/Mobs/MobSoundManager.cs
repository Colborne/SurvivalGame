using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public string damage;
    public string misc;
    public string dying;
    public string attack1;
    public string attack2;
    public string attack3;
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
            audioSource.PlayOneShot(Clip,Random.Range(.2f,.4f));
    }

    public void PlayDamage()
    {
        var Clip = Resources.Load("Sounds/" + damage) as AudioClip;
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Clip);
        }
    }
    public void PlayAttack1()
    {
        var Clip = Resources.Load("Sounds/" + attack1) as AudioClip;
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Clip);
        }
    }

    public void PlayAttack2()
    {
        var Clip = Resources.Load("Sounds/" + attack2) as AudioClip;
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Clip);
        }
    }
    
    public void PlayAttack3()
    {
        var Clip = Resources.Load("Sounds/" + attack3) as AudioClip;
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Clip);
        }
    }
    public void PlayDying()
    {
        var Clip = Resources.Load("Sounds/" + dying) as AudioClip;
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Clip);
        }
    }

    public void PlaySoundRandomlyInterrupt(string audio)
    {
        if(Random.Range(0,25) == 0)
        {
            var Clip = Resources.Load("Sounds/" + audio) as AudioClip;
            audioSource.PlayOneShot(Clip);   
        }  
    }

}
