using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;  
    [SerializeField] private Text howManySave;
	[SerializeField] private Text howManyDead;
    [SerializeField] private Image canMove;

    public GameObject PauseScreen { get { return pauseScreen; }}
    [SerializeField] private GameObject pauseScreen;

    public GameObject BeginScreem { get { return beginScreen; }}
	[SerializeField] private GameObject beginScreen;
	[SerializeField] private GameObject hudScreen;

    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private Text endOrWin;
    [SerializeField] private Sprite[] stopPlayButton;
    [SerializeField] private Sprite[] winOrLoseImages;
    [SerializeField] private Image winOrLoseImage;
	[SerializeField] private Text[] limitTotal;
    [SerializeField] private Sprite[] muteOrNot;
	[SerializeField] private Image musicIcon;



	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
        limitTotal[0].text = GameManager.Instance.LimitPoints.ToString("00");
		limitTotal[1].text = GameManager.Instance.LimitPoints.ToString("00");
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
        howManySave.text = GameManager.Instance.PenguinsSave.ToString("00");
        howManyDead.text = GameManager.Instance.PenguinsDead.ToString("00");
    }

    public void UpdateStateMove()
    {
		canMove.sprite = PlayerController.Instance.CanMove ? stopPlayButton[1] : stopPlayButton[0];
	}

	public void SetPause()
    {
        if (beginScreen.activeSelf)
            return;

        hudScreen.SetActive(pauseScreen.activeSelf);
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void BeginPlay()
    {
        beginScreen.SetActive(false);
        hudScreen.SetActive(true);
        GameManager.Instance.BeginPlay = true;
    }

    public void EndGame(bool win)
    {
        endGameScreen.SetActive(true);
        hudScreen.SetActive(false);
        endOrWin.text = win ? "YOU SAVED THE PENGUINS!" : "YOU LET PENGUINS DIE!";
        winOrLoseImage.sprite = win ? winOrLoseImages[0] : winOrLoseImages[1];
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("PenguinsMap");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeVolumeOfMusic(Slider sliderMusic)
    {
        SoundManager.Instance.MusicSource.volume = sliderMusic.value;

        musicIcon.sprite = sliderMusic.value == 0 ? muteOrNot[0] : muteOrNot[1];
    }
    
}
