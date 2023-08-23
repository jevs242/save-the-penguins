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

    public bool HasFlag { get { return hasFlag; } set { hasFlag = value; } }
    private bool hasFlag;

    public SnowBall Ball { get { return ball; } set { ball = value; } }
    private SnowBall ball;



    // Start is called before the first frame update
    void Start()
    {
		infoHowManyNear.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void Show()
	{
		infoHowManyNear.gameObject.SetActive(true);

		if (ball != null)
			Destroy(ball.gameObject);

		if (isBroken)
        {
			Instantiate(DestroyIceController.Instance.IceFbx, this.transform.position, Quaternion.identity);
			gameObject.SetActive(false);
        }
	}

	public void ResetGame()
    {
		infoHowManyNear.gameObject.SetActive(IsBeginHex);
		howManyNearBroken = 0;
		isBroken = false;
        ResetUIHex();

        if(Ball != null)
		    Destroy(Ball.gameObject);
	}

    public void ResetUIHex()
    {
		infoHowManyNear.text = howManyNearBroken.ToString();
        if(isBroken)
        {
            infoHowManyNear.enabled = false;
            infoHowManyNear.gameObject.SetActive(false);
			infoHowManyNear.color = Color.red;
		}
        else
        {
			infoHowManyNear.enabled = true;
			infoHowManyNear.gameObject.SetActive(false);
			infoHowManyNear.color = Color.white;
		}
	}

    public void ResetHowMany()
    {
		howManyNearBroken = 0;
	}
}
