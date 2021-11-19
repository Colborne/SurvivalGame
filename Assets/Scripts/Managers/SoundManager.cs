using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioSource audioSource;
    PlayerLocomotion playerLocomotion;
    private void Awake() 
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    public void PlaySound(string audio)
    {
        var Clip = Resources.Load(audio) as AudioClip;
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Clip);
        }
    }

    public void Step(AnimationEvent animationEvent)
    {
        var Clip = Resources.Load("Sounds/Footstep_Dirt_0" + Random.Range(0,1).ToString()) as AudioClip;
        if(!footstepSource.isPlaying && animationEvent.animatorClipInfo.weight > 0.5 && playerLocomotion.isGrounded)
            footstepSource.PlayOneShot(Clip,Random.Range(.3f,.5f));
    }

    public void Sneak(AnimationEvent animationEvent)
    {
        var Clip = Resources.Load("Sounds/Footstep_Dirt_0" + Random.Range(0,1).ToString()) as AudioClip;
        if(!footstepSource.isPlaying&& animationEvent.animatorClipInfo.weight > 0.5)
            footstepSource.PlayOneShot(Clip,Random.Range(.1f,.2f));
    }

    public void Jump(AnimationEvent animationEvent)
    {
        var Clip = Resources.Load("Sounds/Footstep_Dirt_04") as AudioClip;
        if(!footstepSource.isPlaying &&animationEvent.animatorClipInfo.weight > 0.5)
            footstepSource.PlayOneShot(Clip,Random.Range(.4f,.7f));
    }
}
