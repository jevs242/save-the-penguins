using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{

    [SerializeField] bool ice;

    // Start is called before the first frame update
    void Start()
    {
        if (ice)
        {
            float randomPitch = Random.Range(1.0f, 1.5f);
		    SoundManager.Instance.PlaySFX(0, randomPitch, 0.3f);            
        }
        else
        {
			float randomPitch = Random.Range(2.0f, 2.5f);
			SoundManager.Instance.PlaySFX(0, randomPitch, 0.1f);
		}
		Destroy(gameObject, 3);
    }
}
