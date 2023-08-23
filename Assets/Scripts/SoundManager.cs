using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
	public AudioSource MusicSource { get { return musicSource; } }
	[SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
	[SerializeField] private AudioSource penguinSource;

	[SerializeField] private AudioClip[] sfxClip;

	private void Awake()
	{
		Instance = this;
	}

    public void PlaySFX(int Clip , float pitch , float volume)
    {
        if (Clip == 3 || Clip == 2)
        {
			penguinSource.volume = volume;
			penguinSource.pitch = pitch;
			penguinSource.clip = sfxClip[Clip];
			penguinSource.Play();
            return;
		}
		sfxSource.volume = volume;
        sfxSource.pitch = pitch;
        sfxSource.clip = sfxClip[Clip];
        sfxSource.Play();
    }

	public void StopMusic()
	{
		musicSource.Stop();
	}
}
