using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

	// Use this for initialization

	public float scrollSpeed;
	public float tileSizeZ;

	private Vector3 startPosition;
	void Start () {
		startPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
