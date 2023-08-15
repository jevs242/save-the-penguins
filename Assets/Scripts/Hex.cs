using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hex : MonoBehaviour
{
    private bool isBroken;
    public bool IsBroken { get { return isBroken; } set { isBroken = value; } }

    public bool IsBeginHex { get { return isBeginHex; } }
    [SerializeField] bool isBeginHex;

    [SerializeField]private Text infoHowManyNear;
    public Text InfoHowManyNear { get { return infoHowManyNear; } set { infoHowManyNear = value; } }

    private int howManyNearBroken;
    public int HowManyNearBroken { get { return howManyNearBroken; } set {  howManyNearBroken = value; } }

    [SerializeField] private int howManyLimit = 6;
    public int HowManyLimit { get { return howManyLimit; }}


    // Start is called before the first frame update
    void Start()
    {
		InfoHowManyNear.gameObject.SetActive(IsBeginHex);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void Show()
	{
		InfoHowManyNear.gameObject.SetActive(true);
	}

	public void ResetGame()
    {
		InfoHowManyNear.gameObject.SetActive(IsBeginHex);
		howManyNearBroken = 0;
		IsBroken = false;
        ResetUIHex();
	}

    public void ResetUIHex()
    {
		InfoHowManyNear.text = HowManyNearBroken.ToString();
        if(isBroken)
        {
            InfoHowManyNear.gameObject.SetActive(false);
			InfoHowManyNear.color = Color.red;
		}
        else
        {
			InfoHowManyNear.color = Color.white;
		}
	}

    public void ResetHowMany()
    {
		howManyNearBroken = 0;
	}
}
