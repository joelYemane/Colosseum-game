using System.Threading.Tasks;
using UnityEngine;

public class SkullAi : MonoBehaviour
{
    public GameObject _lookatObject;

    public AudioSource _voice;
    public Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        _lookatObject = other.gameObject;

        if (other.GetComponent<ObjectAudio>())
        {
            PlayAudio(other.GetComponent<ObjectAudio>()._audio);
        }
    }

    public async void PlayAudio(AudioClip _clip)
    {
        _voice.clip = _clip;
        _voice.Play();
        _animator.SetBool("Talking", true);
        await Task.Delay((int)_clip.length * 1000);
        _animator.SetBool("Talking", false);
    }
}
