using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
	public Vector3 targetPosition;
	public float initialVelocity = 5f;

	public Hex Hex { get { return hex; } set { hex = value; } }
	private Hex hex;

	private void Start()
	{
	}

	private void Update()
	{
		if (RayCastFloor())
		{

		}
		else
		{
			transform.position += Physics.gravity;
		}
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
				return true;
			}
		}
		return false;
	}
}
