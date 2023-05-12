using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioPlayer;
    public void playClip(string clip)
    {
        audioPlayer = GetComponent<AudioSource>();
        //Debug.Log("Sounds/" + clip);
        audioPlayer.clip = Resources.Load<AudioClip>("Sounds/" + clip);
        audioPlayer.Play();
    }
}


