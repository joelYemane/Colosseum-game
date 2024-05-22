using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoriolColl : MonoBehaviour
{
    public AudioClip _audio;

    public TutoriolVoice _voice;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            _voice.PlayAudio(_audio);
        }
    }
}
