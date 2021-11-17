using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string audio)
    {
        var Clip = Resources.Load(audio) as AudioClip;
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Clip, Random.Range(.3f,1f));
        }
    }
}
