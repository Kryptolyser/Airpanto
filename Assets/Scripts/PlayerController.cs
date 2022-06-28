using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundarie
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public Boundarie boundarie;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
				Mathf.Clamp (rb.position.x, boundarie.xMin, boundarie.xMax),
				0.05f,
				Mathf.Clamp (rb.position.z, boundarie.zMin, boundarie.zMax)
		);
	}
}
