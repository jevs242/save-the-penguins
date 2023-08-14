using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyIceController : MonoBehaviour
{
    public static DestroyIceController Instance;

    [SerializeField]private GameObject grid;
	[SerializeField]private GameObject iceFbx;
	[SerializeField]private List<GameObject> hexa;

	private void Awake()
	{
		Instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		foreach (Transform hex in grid.transform.GetComponentInChildren<Transform>())
		{
			hexa.Add(hex.gameObject);
		}
    }

    public void RestartGrid()
    {
        foreach(GameObject hex in hexa)
        {
            hex.gameObject.SetActive(true);
        }
	}

    public void PlayerMove(Vector3 PenguinLocation)
	{
        int count = 0;
        while(true)
        {
            int random = Random.Range(0, hexa.Count);

            if(Vector3.Distance(PenguinLocation, hexa[random].transform.position) <= PlayerController.Instance.DistanceAccepted && Vector3.Distance(PenguinLocation, hexa[random].transform.position) >= 0.5f &&
                hexa[random].gameObject.activeSelf == true)
            {
                hexa[random].gameObject.SetActive(false);
                SoundManager.Instance.PlaySFX(0 , 1 , 1);
                Instantiate(iceFbx, hexa[random].transform.position, Quaternion.identity);
                break;
            }

            if(count >= hexa.Count * 2)
            {
                
                for(int i = 0; i < hexa.Count; i++)
                {
                    if (hexa[i].transform.position == PenguinLocation)
                    {
						hexa[random].gameObject.SetActive(false);
						SoundManager.Instance.PlaySFX(0, 1 , 1);
					}
				}
                break;
            }
            count++;
		}
	}
}
