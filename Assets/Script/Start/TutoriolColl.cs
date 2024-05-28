using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TutoriolColl : MonoBehaviour
{
    public AudioClip _audio;

    public TutoriolVoice _voice;
    public bool _exit;
    public Animator _gateAni;

    private async void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            _voice.PlayAudio(_audio);

            if (!_exit)
            {
                Destroy(this);
            }

            else
            {
                await Task.Delay((int)_audio.length * 1000);
                _gateAni.SetBool("Exit", true);
                Destroy(this);
            }
        }
    }
}
