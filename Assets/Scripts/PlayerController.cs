using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance;
	private Camera mainCamera;
	[SerializeField]private GameObject penguin;
	private NavMeshAgent agent;

	public float DistanceAccepted { get { return distanceAccepted; } }
	private readonly float distanceAccepted = 1.25f;

	private Animator animator;
	private bool begin;

	public bool CanMove { get { return canMove; } set { canMove = value; } }
	private bool canMove = true;

	public bool FirstPlay { get { return firstPlay; } set { firstPlay = value; } }
	private bool firstPlay = true;

	[SerializeField] private Transform[] leftIce;
	[SerializeField] private Transform[] rightIce;
	[SerializeField] private Transform[] forwardIce;
	[SerializeField] private Transform[] backIce;
	[SerializeField] private Transform[] destinationToEnd;

	[SerializeField] private GameObject redflag;
	public GameObject SnowBallFBX { get { return snowBallFBX; } }
	[SerializeField] private GameObject snowBallFBX;

	private Vector3 begiPosition;
	private Quaternion beginRotation;

	public bool Dead { get { return dead; } set { dead = value; } }
	private bool dead;

	private void Awake()
	{
		Instance = this;
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		penguin = GameObject.Find("Penguin");
		begiPosition = transform.position;
		beginRotation = transform.rotation;
		mainCamera = Camera.main;
		UIManager.Instance.UpdateUI();
	}

	private void Update()
	{
		UIManager.Instance.UpdateStateMove();

		animator.SetFloat("Speed" ,agent.velocity.magnitude);
		if (UIManager.Instance.PauseScreen.activeSelf || UIManager.Instance.BeginScreem.activeSelf || GameManager.Instance.EndGame)
			return;

		PlayerMove();

		if(Input.GetMouseButtonDown(1) && !dead && canMove)
		{
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (Vector3.Distance(penguin.transform.position, hit.transform.position) <= distanceAccepted && Vector3.Distance(penguin.transform.position, hit.transform.position) != 0 && hit.collider.CompareTag("Hexa") && Vector3.Distance(penguin.transform.position, hit.transform.position) >= 0.5f)
				{
					Hex hex = hit.transform.gameObject.GetComponentInParent<Hex>();

					if(!hex.InfoHowManyNear.gameObject.activeSelf)
					{
						if(redflag)
						{
							if(!hex.HasFlag)
							{
								GameObject ball = Instantiate(redflag, hit.collider.transform.position + new Vector3(0,250), Quaternion.identity);
								hex.Ball = ball.GetComponent<SnowBall>();
								hex.HasFlag = true;
							}
							else
							{
								hex.HasFlag = false;

								if (hex.Ball != null)
								{
									Instantiate(snowBallFBX, hex.Ball.transform.position, Quaternion.identity);
									Destroy(hex.Ball.gameObject);
								}
							}
						}
					}

				}
			}
		}

	}

	private void PlayerMove()
	{
		if (Input.GetMouseButtonDown(0) && !dead && canMove)
		{
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{

				if (Vector3.Distance(penguin.transform.position, hit.transform.position) <= distanceAccepted && Vector3.Distance(penguin.transform.position, hit.transform.position) != 0 && hit.collider.CompareTag("Hexa") && Vector3.Distance(penguin.transform.position, hit.transform.position) >= 0.5f)
				{
					DestroyIceController.Instance.NewPlayerMove(hit.transform.parent.transform.position);

					agent.destination = hit.transform.parent.transform.position;
					canMove = false;
					SoundManager.Instance.PlaySFX(1, 2, 1f);

					StartCoroutine(DelayToNextMove(hit.transform.parent.transform.position));
				}
			}
			begin = true;
		}
		if (!RayCastFloor() && begin)
		{
			transform.position += Physics.gravity * Time.deltaTime;
			agent.enabled = false;
		}
	}

	IEnumerator DelayToNextMove(Vector3 PenguinLocation)
	{
		yield return new WaitForSeconds(1);
		if(!dead)
		{
			canMove = true;
		}
		CheckIfDone(PenguinLocation);
	}

	private bool RayCastFloor()
	{
		// Create a raycast from the object's position straight down
		Ray ray = new(transform.position, Vector3.down);

		// Set the maximum distance for the raycast
		float maxDistance = 10f; // Adjust this value according to your scene

		// Perform the raycast
		if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
		{
			// Check if the hit object is the floor
			if (hit.collider.CompareTag("Hexa") || hit.collider.CompareTag("Floor"))
			{
				agent.enabled = true;
				return true;
			}
		}
		return false;
	}


	private void CheckIfDone(Vector3 PenguinLocation)
	{
		if(agent.enabled)
		{
			if(CheckIceArray(PenguinLocation, leftIce))
			{
				agent.destination = destinationToEnd[0].transform.position;
			}
			else if (CheckIceArray(PenguinLocation, rightIce))
			{
				agent.destination = destinationToEnd[1].transform.position;

			}
			else if (CheckIceArray(PenguinLocation, forwardIce))
			{
				agent.destination = destinationToEnd[2].transform.position;
			}
			else if (CheckIceArray(PenguinLocation, backIce))
			{
				agent.destination = destinationToEnd[3].transform.position;
			}
		}
	}

	private bool CheckIceArray(Vector3 PenguinLocation , Transform[] Ices)
	{
		foreach (Transform ice in Ices)
		{
			if (ice)
			{
				if (ice.transform.position == PenguinLocation)
				{
					GameManager.Instance.PenguinsSave++;
					canMove = false;
					StartCoroutine(RestartGame());
					return true;
				}
			}
		}
		return false;
	}

	private IEnumerator RestartGame()
	{
		DestroyIceController.Instance.Show();
		SoundManager.Instance.PlaySFX(2, 1, 1);
		yield return new WaitForSeconds(5);
		agent.enabled = false;
		GameManager.Instance.CheckGame();
		UIManager.Instance.UpdateUI();
		DestroyIceController.Instance.RestartGrid();
		RestartPenguin();
	}

	public void RestartPenguin()
	{
		firstPlay = true;
		begin = false;
		transform.SetPositionAndRotation(begiPosition, beginRotation);
		if(penguin.activeSelf)
			StartCoroutine(RefreshAgent());
	}

	IEnumerator RefreshAgent()
	{
		yield return new WaitForNextFrameUnit();
		agent.enabled = true;
		dead = false;
		canMove = true;
	}
}
