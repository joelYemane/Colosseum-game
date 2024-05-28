using System;
using System.Threading.Tasks;
using UnityEngine;

public class TutoriolVoice : MonoBehaviour
{
    public AudioSource _skullAudio;
    public Animator _animator;

    public Voice _voice = new Voice();

    public Transform _player;

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

    public async void PlayAudio(AudioClip _clip)
    {
        _skullAudio.clip = _clip;
        _skullAudio.Play();
        _animator.SetBool("Talking", true);
        await Task.Delay((int)_clip.length * 1000);
        _animator.SetBool("Talking", false);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 2);
    }
}