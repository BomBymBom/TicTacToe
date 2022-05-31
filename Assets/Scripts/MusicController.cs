using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip[] _music;
    private int _soundIndex = 1;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySound(1.5f));
    }

    IEnumerator PlaySound(float time)
    {
        if (_soundIndex == 1) _soundIndex = 0;
        else _soundIndex = 1;

        yield return new WaitForSeconds(time);
        _audioSource.clip = _music[_soundIndex];
        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);
        StartCoroutine(PlaySound(20f));
    }
}
