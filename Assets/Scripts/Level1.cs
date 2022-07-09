using DualPantoFramework;
using SpeechIO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    private SpeechOut speechOut;
	private LowerHandle lHandle;
	private UpperHandle uHandle;
	private AudioSource audioSource;
	private bool hitPuck = false;

	public GameObject p1;

	async void Start()
    {
		p1 = GameObject.FindWithTag("P1");
		speechOut = new SpeechOut();
		lHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
		uHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
		audioSource = gameObject.GetComponent<AudioSource>();

		p1.transform.position = new Vector3(0, 0.05f, -7.0602f);
		transform.position = new Vector3(0, 0.05f, -3.52f);

		await lHandle.MoveToPosition(new Vector3(0,0, -3.52f), 3f);
		await uHandle.MoveToPosition(new Vector3(0,0, -3.52f), 3f);
		await speechOut.Speak("Welcome to Airpanto! Please get ready and grab the handles in front of you.");
		await Task.Delay(2000);

		await GameObject.FindObjectOfType<PlayerController>().ActivatePlayer();
		await GameObject.FindObjectOfType<DiskController>().ActivateDisk();

		Level level = GameObject.Find("Panto").GetComponent<Level>();
		await level.PlayIntroduction();

		await GameObject.FindObjectOfType<DiskController>().ActivateDisk();

		
		speechOut = new SpeechOut();
		await speechOut.Speak("Try hitting the puck in front of you.");
		p1.GetComponent<PlayerController>().frozen = false;
		uHandle.Free();
		await lHandle.SwitchTo(gameObject, 20);
		audioSource.Play();
	}

	
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene(sceneName: "Level 2");
		}
	}


	async void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("P1") && 
				!p1.GetComponent<PlayerController>().frozen && 
				!hitPuck)
		{
			hitPuck = true;
            await speechOut.Speak("Wow, you are so good at this.");
		}
	}
}
