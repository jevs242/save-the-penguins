using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] sfxClip;

	private void Awake()
	{
		Instance = this;
	}

    public void PlaySFX(int Clip , float pitch , float volume)
    {
        sfxSource.volume = volume;
        sfxSource.pitch = pitch;
        sfxSource.clip = sfxClip[Clip];
        sfxSource.Play();
    }
}
