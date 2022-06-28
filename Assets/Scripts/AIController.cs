using UnityEngine;

[System.Serializable]
public class AIBoundarie
{
	public float xMin, xMax, zMin, zMax;
}

public class AIController : MonoBehaviour {

	private Rigidbody rb;
	private GameObject diskObject;
	private bool opponentSide = true;
    private float diskOffset;
	private Vector3 targetPos;

	public GameObject disk;
	public AIBoundarie boundarie;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		diskObject = GameObject.FindWithTag ("Disk");
	}

	void LateUpdate ()
	{
		var aiPos = new Vector3
			(
				Mathf.Clamp (rb.position.x, boundarie.xMin, boundarie.xMax),
				0.05f,
				Mathf.Clamp (rb.position.z, boundarie.zMin, boundarie.zMax)
			);

		float speed;

		if (diskObject.transform.position.z < 0)
        {
            if (opponentSide)
            {
                opponentSide = false;
                diskOffset = Random.Range(-1f, 1f);
            }
 
            speed = 10 * Random.Range(0.1f, 0.3f);

            targetPos = new Vector3
			(
				Mathf.Clamp(diskObject.transform.position.x + diskOffset, boundarie.xMin, boundarie.xMax),
				0.0f,
				5.0f
			);
        }
        else
        {
            opponentSide = true;
 
            speed = Random.Range(10 * 0.4f, 10);

            targetPos = new Vector3
			(
				Mathf.Clamp(diskObject.transform.position.x, boundarie.xMin, boundarie.xMax),
				0.0f,
                Mathf.Clamp(diskObject.transform.position.z, boundarie.zMin, boundarie.zMax)
			);
        }

		rb.MovePosition(Vector3.MoveTowards (aiPos, targetPos, speed * Time.fixedDeltaTime));
	}

}
