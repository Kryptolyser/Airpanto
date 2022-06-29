using DualPantoFramework;
using SpeechIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private SpeechOut speechOut;
	private LowerHandle lHandle;
	private UpperHandle uHandle;
	private AudioSource audioSource;

	public GameObject p1;

	async void Start()
    {
		p1 = GameObject.FindWithTag("P1");
		speechOut = new SpeechOut();
		lHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
		uHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
		audioSource = gameObject.GetComponent<AudioSource>();

		uHandle.Freeze();
		speechOut = new SpeechOut();
		await speechOut.Speak("Hello player. " +
			"Welcome to your greatest haptic experience. " +
			"This is Airpanto! " +
			"For now, try to hit the puck in front of you.");
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
		if (other.gameObject.CompareTag("P1"))
		{
            await speechOut.Speak("Wow, you are so good at this.");
		}
	}
}
