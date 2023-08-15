using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
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

        NewMethodPlay();

	}

    public void RestartGrid()
    {
        foreach(GameObject hex in hexa)
        {
            hex.gameObject.SetActive(true);

			hex.GetComponent<Hex>().ResetGame();
        }
		NewMethodPlay();

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

    /// <summary>
    /// Move
    /// </summary>

	public void NewPlayerMove(Vector3 HexPosition)
	{
        GameObject hexaGameObject = new();

        foreach(GameObject hex in hexa)
        {
            if(hex.transform.position == HexPosition)
            {
                hexaGameObject = hex;
                break;
            }
        }

        Hex Hexa = hexaGameObject.GetComponent<Hex>();

        if(PlayerController.Instance.FirstPlay)
        {
            Hexa.IsBroken = false;
            Hexa.ResetUIHex();
            PlayerController.Instance.FirstPlay = false;
		}
		else
        {
			if (Hexa.IsBroken)
			{
				Hexa.gameObject.SetActive(false);
				SoundManager.Instance.PlaySFX(0, 1, 1);
				Instantiate(iceFbx, Hexa.transform.position, Quaternion.identity);
			}
		}
        ClearZone(hexaGameObject);
	}

    private void ClearZone(GameObject Hex)
    {

        if (Hex.GetComponent<Hex>().InfoHowManyNear.gameObject.activeSelf)
            return;

        foreach(GameObject hex in hexa)
        {
            if(Vector3.Distance(hex.transform.position , Hex.transform.position) <= PlayerController.Instance.DistanceAccepted && !hex.GetComponent<Hex>().IsBroken)
            {
                hex.GetComponent<Hex>().InfoHowManyNear.gameObject.SetActive(true);
            }
        }

	}

	/// <summary>
	/// Map
	/// </summary>

	private void NewMethodPlay()
    {
        int count = 0;
        while(count < GameManager.Instance.HowManyBroken)
        {
            foreach(GameObject hex in hexa)
            {
                Hex h = hex.GetComponent<Hex>();
                if(Random.Range(0 , 10) == 0 && !h.IsBeginHex)
                {
                    h.IsBroken = true;
					h.InfoHowManyNear.color = Color.red;
					count++;
                }

                if(count >= GameManager.Instance.HowManyBroken)
                {
                    break;
                }
            }
        }

        HowManyNear(true);
    }

    private void HowManyNear(bool begin)
    {
        foreach(GameObject hex in hexa)
        {
            hex.GetComponent<Hex>().ResetHowMany();
            foreach(GameObject hex2 in hexa)
            {
                if(hex != hex2)
                {
                    if(Vector3.Distance(hex.transform.position, hex2.transform.position) <= PlayerController.Instance.DistanceAccepted && hex2.GetComponent<Hex>().IsBroken)
                    {
                        hex.GetComponent<Hex>().HowManyNearBroken++;
					}
				}
            }
        }

        foreach(GameObject hex in hexa)
        {
			Hex hexa = hex.GetComponent<Hex>();
            hexa.ResetUIHex();
		}

        if(begin)
            FixBroken();
    }

    private void FixBroken()
    {
		foreach (GameObject hex in hexa)
		{

		    foreach (GameObject hex2 in hexa)
		    {

    			if (hex != hex2)
			    {
			    	Hex hexa = hex.GetComponent<Hex>();
			    	Hex hexa2 = hex2.GetComponent<Hex>();

    			    if (Vector3.Distance(hex.transform.position, hex2.transform.position) <= PlayerController.Instance.DistanceAccepted && hexa.HowManyNearBroken == hexa.HowManyLimit && !hexa.IsBeginHex)
			    	{
						hexa.HowManyNearBroken--;
			    		hexa2.IsBroken = false;
						hexa.ResetUIHex();
						HowManyNear(false);
    			    }

                    if(hexa.HowManyNearBroken == 0)
                    {
                        hexa.HowManyNearBroken++;
			    		hexa.IsBroken = true;
						hexa.ResetUIHex();
						HowManyNear(false);
						
					}
			    }
		    }   

    		
    	}

	}
    public void Show()
    {
	    foreach (GameObject hex in hexa)
	    {
		    hex.GetComponent<Hex>().Show();
	    }
    }
}

