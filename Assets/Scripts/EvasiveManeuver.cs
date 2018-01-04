using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

	public Vector2 startWait;
	public float dodge;
	public float smoothing;
	public Boundary boundary;
	public float tilt;
	private float currentSpeed;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;

	private float targetManeuver;
	private Rigidbody rigidbody;
	void Start () 
	{
		rigidbody = GetComponent<Rigidbody> ();
		currentSpeed = rigidbody.velocity.z;
		StartCoroutine (Evade ());
	}

	IEnumerator Evade()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

		while (true)
		{
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = Random.Range (1, dodge) * Mathf.Sign (transform.position.z);
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}
	
	void FixedUpdate () 
	{
		float newManuver = Mathf.MoveTowards (rigidbody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rigidbody.velocity = new Vector3 (newManuver, 0.0f, currentSpeed);
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler 
		(
			0.0f,
			0.0f,
			GetComponent<Rigidbody>().velocity.x * -tilt
		);
	}
}
