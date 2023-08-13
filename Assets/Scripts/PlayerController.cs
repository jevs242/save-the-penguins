using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	private Camera mainCamera;
	private GameObject pinguin;
	public static PlayerController instance;
	private float _distanceAccepted = 1.25f;
	public float distanceAccepted { get { return _distanceAccepted; } }
	private NavMeshAgent agent;


	public Transform destination; // Assign the destination point in the Inspector
	public float speed = 5.0f; // Adjust the speed of movement
	private Animator animator;
	private bool _begin;

	public bool canMove { get { return _canMove; } }
	private bool _canMove = true;

	[SerializeField] private Transform[] leftIce;
	[SerializeField] private Transform[] rightIce;
	[SerializeField] private Transform[] forwardIce;
	[SerializeField] private Transform[] backIce;
	[SerializeField] private Transform[] destinationToEnd;

	private Vector3 _begiPosition;
	private Quaternion _beginRotation;

	public bool dead { get { return _dead; } set { _dead = value; } }
	private bool _dead;

	private void Awake()
	{
		instance = this;
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		_begiPosition = transform.position;
		_beginRotation = transform.rotation;
		mainCamera = Camera.main;
		pinguin = GameObject.Find("Pinguin").gameObject;
		UIManager.instance.UpdateUI();
	}

	private void Update()
	{
		UIManager.instance.UpdateStateMove();

		animator.SetFloat("Speed" ,agent.velocity.magnitude);
		if (UIManager.instance.pauseScreen.activeSelf || UIManager.instance.beginScreem.activeSelf)
			return;

		// Check if the left mouse button is pressed
		if (Input.GetMouseButtonDown(0) && !_dead && _canMove)
		{
			// Cast a ray from the mouse position into the scene
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits an object
			if (Physics.Raycast(ray, out hit))
			{
				GameObject clickedObject = hit.collider.gameObject;

				if (Vector3.Distance(pinguin.transform.position, hit.transform.position) <= _distanceAccepted && Vector3.Distance(pinguin.transform.position, hit.transform.position) != 0 && hit.collider.CompareTag("Hexa") && Vector3.Distance(pinguin.transform.position, hit.transform.position) >= 0.5f)
				{
					destination = hit.transform.parent.transform;
					DestroyIceController.instance.PlayerMove(pinguin.transform.position);
					
					agent.destination = hit.transform.parent.transform.position;
					_canMove = false;
					SoundManager.instance.PlaySFX(1, 2 , 0.1f);

					StartCoroutine(DelayToNextMove(hit.transform.parent.transform.position));
				}
			}
			
			_begin = true;
		}

		if(!RayCastFloor() && _begin)
		{
			transform.position += Physics.gravity * Time.deltaTime;
			agent.enabled = false;
		}

	}

	IEnumerator DelayToNextMove(Vector3 PinguinLocation)
	{
		yield return new WaitForSeconds(1.5f);
		_canMove = true;

		CheckIfDone(PinguinLocation);
	}

	private bool RayCastFloor()
	{
		// Create a raycast from the object's position straight down
		Ray ray = new Ray(transform.position, Vector3.down);

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


	private void CheckIfDone(Vector3 PinguinLocation)
	{
		if(agent.enabled)
		{
			if(CheckIceArray(PinguinLocation, leftIce))
			{
				agent.destination = destinationToEnd[0].transform.position;
			}
			else if (CheckIceArray(PinguinLocation, rightIce))
			{
				agent.destination = destinationToEnd[1].transform.position;

			}
			else if (CheckIceArray(PinguinLocation, forwardIce))
			{
				agent.destination = destinationToEnd[2].transform.position;
			}
			else if (CheckIceArray(PinguinLocation, backIce))
			{
				agent.destination = destinationToEnd[3].transform.position;
			}
		}
	}

	private bool CheckIceArray(Vector3 PinguinLocation , Transform[] Ices)
	{
		foreach (Transform ice in Ices)
		{
			if (ice)
			{
				if (ice.transform.position == PinguinLocation)
				{
					GameManager.instance.pinguinsSave++;
					_canMove = false;
					StartCoroutine(RestartGame());
					return true;
				}
			}
		}
		return false;
	}

	private IEnumerator RestartGame()
	{
		SoundManager.instance.PlaySFX(2, 1, 1);
		yield return new WaitForSeconds(2);
		agent.enabled = false;
		UIManager.instance.UpdateUI();
		DestroyIceController.instance.RestartGrid();
		RestartPinguin();
	}

	public void RestartPinguin()
	{
		_begin = false;
		transform.position = _begiPosition;
		transform.rotation = _beginRotation;
		StartCoroutine(RefreshAgent());
	}

	IEnumerator RefreshAgent()
	{
		yield return new WaitForNextFrameUnit();
		agent.enabled = true;
		_dead = false;
		_canMove = true;
	}
}
