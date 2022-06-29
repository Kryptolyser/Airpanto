using DualPantoFramework;
using SpeechIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    private SpeechOut speechOut;
	private LowerHandle lHandle;
	private UpperHandle uHandle;
	private AudioSource audioSource;
	private Rigidbody rb;
	private MeshCollider collider;
	private bool hitByPlayer;

	public GameObject p1;

	async void Start()
    {
		rb = GetComponent<Rigidbody>();
		collider = GetComponent<MeshCollider>();
		p1 = GameObject.FindWithTag("P1");
		speechOut = new SpeechOut();
		lHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
		uHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
		audioSource = gameObject.GetComponent<AudioSource>();
		hitByPlayer = false;

		uHandle.Freeze();
		speechOut = new SpeechOut();
        await speechOut.Speak("Hello player. " +
            "Welcome to your second greatest haptic experience ever. " +
            "This is Airpanto! " +
            "There is a puck coming at you. Try to reflect it.");
        p1.GetComponent<PlayerController>().frozen = false;
		uHandle.Free();
		await lHandle.SwitchTo(gameObject, 5);

		Reset();
	}

    void Update()
    {
        
    }

	async void Reset() 
	{
		gameObject.transform.position = new Vector3(-3.85f, 0f, 3.32f);
		collider.enabled = true;
		audioSource.Play();

		rb.velocity = new Vector3(Random.Range(0.5f, 1.5f), 0f, -5f);
	}

	async void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Untagged"))
			return;

		if (other.gameObject.CompareTag("P1"))
		{
			hitByPlayer = true;
			await speechOut.Speak("You are amazing! Good job!");
		}
		else
		{
			rb.velocity = new Vector3();
			collider.enabled = false;
			if (!hitByPlayer)
			{
				await speechOut.Speak("Ops, try again. And this time, hit the puck.");
				Reset();
			}
		}
	}
}
