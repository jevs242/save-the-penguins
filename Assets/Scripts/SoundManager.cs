using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] sfxClip;

	private void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int Clip , float pitch , float volume)
    {
        sfxSource.volume = volume;
        sfxSource.pitch = pitch;
        sfxSource.clip = sfxClip[Clip];
        sfxSource.Play();
    }
}
