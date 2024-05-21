using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class TutoriolVoice : MonoBehaviour
{
    public AudioSource _skullAudio;

    public Voice _voice = new Voice();

    [Serializable]
    public class Voice
    {
        public AudioClip _start;
        public AudioClip _pillerFall;
        public AudioClip _overpiller;
        public AudioClip _ropeslice;
        public AudioClip _afterropeslice;
        public AudioClip _woodslice1;
        public AudioClip _woodslice2;
        public AudioClip _afterwoodslice;
        public AudioClip _SkullInfo;
        public AudioClip _exit;
    }

    private async void Start()
    {
        await Task.Delay(5000);
        PlayAudio(_voice._start);
    }

    public void PlayAudio(AudioClip _clip)
    {
        _skullAudio.clip = _clip;
        _skullAudio.Play();
    }
}
