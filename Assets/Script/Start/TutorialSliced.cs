using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSliced : MonoBehaviour
{
    public AudioClip _audio;

    public TutoriolVoice _voice;

    private void OnDestroy()
    {
        _voice.PlayAudio(_audio);
    }
}
