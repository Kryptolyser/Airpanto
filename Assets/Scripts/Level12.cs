using DualPantoFramework;
using SpeechIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level12 : MonoBehaviour
{
    private SpeechOut speechOut;
	private LowerHandle lHandle;
	private UpperHandle uHandle;
	private AudioSource audioSource;

	public GameObject p1;

	async void Start()
    {

		p1 = GameObject.FindWithTag("P1");

		Level level = GameObject.Find("Panto").GetComponent<Level>();
		await level.PlayIntroduction();


		
		p1.transform.position = new Vector3(0, 0.05f, -5);
		transform.position = new Vector3(0, 0.05f, 0);
		await GameObject.FindObjectOfType<PlayerController>().ActivatePlayer();
		await GameObject.FindObjectOfType<DiskController>().ActivateDisk();
		speechOut = new SpeechOut();
		lHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
		uHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
		audioSource = gameObject.GetComponent<AudioSource>();

		uHandle.Freeze();
		speechOut = new SpeechOut();
		await speechOut.Speak("Try to score!");
		
		p1.GetComponent<PlayerController>().frozen = false;
		uHandle.Free();
		await lHandle.SwitchTo(gameObject, 5);
		audioSource.Play();
	}

	
    void Update()
    {
        
    }


	async void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player2Goal") && !p1.GetComponent<PlayerController>().frozen)
		{
            await speechOut.Speak("Good Job!");
		}
	}
}
