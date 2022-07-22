using DualPantoFramework;
using SpeechIO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    private SpeechOut speechOut;
	private LowerHandle lHandle;
	private UpperHandle uHandle;
	private AudioSource audioSource;
	private Rigidbody rb;
	private bool scoredGoal = false;
	private bool debug;

	public GameObject p1;

	async void Start()
    {
		p1 = GameObject.FindWithTag("P1");
		speechOut = new SpeechOut();
		lHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
		uHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
		audioSource = gameObject.GetComponent<AudioSource>();
		rb = gameObject.GetComponent<Rigidbody>();
		debug = GameObject.Find("Panto").GetComponent<DualPantoSync>().debug;

		p1.transform.position = new Vector3(0, 0.05f, -7.0602f);
		transform.position = new Vector3(-2.5f, 0.05f, -0.6f);

		//await lHandle.MoveToPosition(new Vector3(0, 0, -3.52f), 3f);
		//await uHandle.MoveToPosition(new Vector3(0, 0, -3.52f), 3f);
		//if (!debug)
		//{
		//	await speechOut.Speak("Please get ready and grab the handles in front of you.");
		//	await Task.Delay(2000);
		//}

		if (!debug)
		{
			Level level = GameObject.Find("Panto").GetComponent<Level>();
			await level.PlayIntroduction();
		}

		await GameObject.FindObjectOfType<PlayerController>().ActivatePlayer();
		await GameObject.FindObjectOfType<DiskController>().ActivateDisk();

		await speechOut.Speak("Der Puck kommt auf dich zu. Schieﬂe ihn in das Tor.", 1, SpeechBase.LANGUAGE.GERMAN);
		p1.GetComponent<PlayerController>().frozen = false;
		uHandle.Free();
		await lHandle.SwitchTo(gameObject, 20);
		audioSource.Play();
		rb.velocity = new Vector3(2.5f, 0f, -5f);
	}

    void Update()
    {
        
    }

	async void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player2Goal") && !scoredGoal)
		{
			scoredGoal = true;
			await speechOut.Speak("Klasse! Du hast gerade ein Tor geschossen!", 1, SpeechBase.LANGUAGE.GERMAN);
		}

		//if (other.gameObject.CompareTag("Untagged"))
		//	return;

		//if (other.gameObject.CompareTag("P1"))
		//{
		//	hitByPlayer = true;
		//	await speechOut.Speak("You are amazing! Good job!");
		//}
		//else
		//{
		//	rb.velocity = new Vector3();
		//	collider.enabled = false;
		//	if (!hitByPlayer)
		//	{
		//		await speechOut.Speak("Ops, try again. And this time, hit the puck.");
		//		Reset();
		//	}
		//}
	}
}
