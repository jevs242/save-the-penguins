using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyIceController : MonoBehaviour
{
    [SerializeField]private GameObject _grid;
	[SerializeField]private GameObject _iceFbx;
	[SerializeField]private List<GameObject> _hexa;

    public static DestroyIceController instance;

	private void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		foreach (Transform hex in _grid.transform.GetComponentInChildren<Transform>())
		{
			_hexa.Add(hex.gameObject);
		}
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RestartGrid()
    {
        foreach(GameObject hex in _hexa)
        {
            hex.gameObject.SetActive(true);
        }
	}

    public void PlayerMove(Vector3 PinguinLocation)
	{
        int count = 0;
        while(true)
        {
            int random = Random.Range(0, _hexa.Count);

            if(Vector3.Distance(PinguinLocation, _hexa[random].transform.position) <= PlayerController.instance.distanceAccepted && Vector3.Distance(PinguinLocation, _hexa[random].transform.position) >= 0.5f &&
                _hexa[random].gameObject.activeSelf == true)
            {
                _hexa[random].gameObject.SetActive(false);
                SoundManager.instance.PlaySFX(0 , 1 , 1);
                Instantiate(_iceFbx, _hexa[random].transform.position, Quaternion.identity);
                break;
            }

            if(count >= _hexa.Count * 2)
            {
                
                for(int i = 0; i < _hexa.Count; i++)
                {
                    if (_hexa[i].transform.position == PinguinLocation)
                    {
						_hexa[random].gameObject.SetActive(false);
						SoundManager.instance.PlaySFX(0, 1 , 1);
					}
				}
                break;
            }
            count++;
		}

	}
}
