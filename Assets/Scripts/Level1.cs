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
	private bool debug;

	public GameObject p1;

	async void Start()
    {
		
		p1 = GameObject.FindWithTag("P1");
		speechOut = new SpeechOut();
		speechOut.SetLanguage(SpeechBase.LANGUAGE.GERMAN);
		lHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
		uHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
		audioSource = gameObject.GetComponent<AudioSource>();
		debug = GameObject.Find("Panto").GetComponent<DualPantoSync>().debug;

		lHandle.Freeze();
		uHandle.Freeze();
		p1.transform.position = new Vector3(0, 0.05f, -7.0602f);
		transform.position = new Vector3(0, 0.05f, -3.52f);

		await lHandle.MoveToPosition(new Vector3(0,0, -3.52f), 3f);
		await uHandle.MoveToPosition(new Vector3(0,0, -3.52f), 3f);
		if (!debug)
		{
			await speechOut.Speak("Willkommen bei Airpanto! " +
				"Mach dich bereit und greife nach den beiden Armen vor dir. " +
				"Halte den unteren Arm nur ganz leicht fest, damit er sp�ter dem Puck folgen kann.", 1, SpeechBase.LANGUAGE.GERMAN);
			await Task.Delay(2000);
		}

		if (!debug)
		{
			Level level = GameObject.Find("Panto").GetComponent<Level>();
			await level.PlayIntroduction();
		}

		await GameObject.FindObjectOfType<PlayerController>().ActivatePlayer();
		await GameObject.FindObjectOfType<DiskController>().ActivateDisk();

		await speechOut.Speak("Deine erste Aufgabe: Versuche mit dem oberen Arm den Puck vor dir zu treffen. Viel Erfolg!", 1, SpeechBase.LANGUAGE.GERMAN);
		p1.GetComponent<PlayerController>().frozen = false;
		uHandle.Free();
		//await lHandle.SwitchTo(gameObject, 20);
		audioSource.Play();
	}

	
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			lHandle.Free();
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
            await speechOut.Speak("Super! Du hast den Puck getroffen. Gehen wir weiter zum nächsten Level.", 1, SpeechBase.LANGUAGE.GERMAN);
			lHandle.Free();
			uHandle.Free();
			SceneManager.LoadScene(sceneName: "Level 2");
		}
	}

    private void OnApplicationQuit()
    {
		speechOut.Stop();
    }
}
