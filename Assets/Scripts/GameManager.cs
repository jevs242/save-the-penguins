using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	[SerializeField] private GameObject penguin;


	public int PenguinsSave { get { return penguinsSave; } set { penguinsSave = value; } }
    private int penguinsSave;

	public int PenguinsDead { get { return penguinsDead; } set { penguinsDead = value; } }
    private int penguinsDead;

	public bool BeginPlay { get { return beginPlay; } set { beginPlay = value; } }
	private bool beginPlay;

	public int LimitPoints { get { return limitPoints; } }
	[SerializeField] private int limitPoints;

	public bool EndGame { get { return endGame; } set { endGame = value; } }
	private bool endGame;

	[SerializeField] private int howManyBroken = 60;
	public int HowManyBroken { get { return howManyBroken; } }

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
	}

	public void CheckGame()
	{
		if (penguinsDead >= limitPoints || penguinsSave >= limitPoints)
		{
			EndGame = true;
			UIManager.Instance.EndGame(penguinsSave >= limitPoints);

			if (penguinsSave >= limitPoints)
			{
				SoundManager.Instance.PlaySFX(4 , 1 , 1);
			}
			else
			{
				SoundManager.Instance.PlaySFX(5, 1, 1);
			}

			penguin.SetActive(false);
		}
	}
}
