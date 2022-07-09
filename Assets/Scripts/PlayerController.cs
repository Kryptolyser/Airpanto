using DualPantoFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[System.Serializable]
public class Boundarie
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public Boundarie boundarie;
	public bool frozen = true;
	public AudioClip[] audioSources;

	private Rigidbody rb;
	private UpperHandle handle;
	private float lastDiskCollision;
	private AudioSource audioSource;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		handle = GameObject.Find("Panto").GetComponent<UpperHandle>();
		lastDiskCollision = Time.time;
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	public async Task ActivatePlayer()
	{
		await handle.SwitchTo(gameObject, 20f);
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

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag == "Disk" &&
				Time.time - lastDiskCollision > 0.5)
		{
			PlayRandomHitSound();
			lastDiskCollision = Time.time;
		}
    }

	void PlayRandomHitSound() 
	{
		audioSource.clip = audioSources[Random.Range(0, audioSources.Length)];
		audioSource.Play();
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
