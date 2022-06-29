using DualPantoFramework;
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
	public bool frozen;

	private Rigidbody rb;
	private UpperHandle handle;

	async void Start ()
	{
		frozen = true;
		rb = GetComponent<Rigidbody>();
		handle = GameObject.Find("Panto").GetComponent<UpperHandle>();

		await handle.MoveToPosition(gameObject.transform.position, speed, true);
	}

	void Update ()
	{
		/* float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
				Mathf.Clamp (rb.position.x, boundarie.xMin, boundarie.xMax),
				0.05f,
				Mathf.Clamp (rb.position.z, boundarie.zMin, boundarie.zMax)
		); */

		if (!frozen)
			PantoMovement();
	}

	void PantoMovement() 
    {
        Vector3 handlePos = handle.GetPosition();
		handlePos.y = 0.05f;

		rb.velocity = (handlePos - rb.position) * speed;
		//rb.position = handlePos;

		//rb.position = new Vector3
		//(
		//		Mathf.Clamp(rb.position.x, boundarie.xMin, boundarie.xMax),
		//		0.05f,
		//		Mathf.Clamp(rb.position.z, boundarie.zMin, boundarie.zMax)
		//);

	}
}
