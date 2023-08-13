using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;  
    [SerializeField] private Text _howManySave;
	[SerializeField] private Text _howManyDead;
    [SerializeField] private Image _canMove;

    public GameObject pauseScreen { get { return _pauseScreen; }}
    [SerializeField] private GameObject _pauseScreen;

    public GameObject beginScreem { get { return _beginScreen; }}
	[SerializeField] private GameObject _beginScreen;
	[SerializeField] private GameObject _hudScreen;



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
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            SetPause();
        }
    }

    public void UpdateUI()
    {
        _howManySave.text = GameManager.instance.pinguinsSave.ToString("00");
        _howManyDead.text = GameManager.instance.pinguinsDead.ToString("00");
    }

    public void UpdateStateMove()
    {
		_canMove.color = PlayerController.instance.canMove ? UnityEngine.Color.green : UnityEngine.Color.red;
	}

	public void SetPause()
    {
        if (_beginScreen.activeSelf)
            return;

        _hudScreen.SetActive(_pauseScreen.activeSelf);
        _pauseScreen.SetActive(_pauseScreen.activeSelf ? false : true);
    }

    public void BeginPlay()
    {
        _beginScreen.SetActive(false);
        _hudScreen.SetActive(true);
        GameManager.instance.beginPlay = true;
    }
    
}
