using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {


	public GameObject DustCloud;

	// Use this for initialization
	private void Start () {
		
	}

	private void OnTriggerEnter2D(Collider2D other) {
		
		if(other.gameObject.layer == (int)EnumBase.Layers.Ground)
		{
			Instantiate(DustCloud, transform.position, DustCloud.transform.rotation);
			Debug.Log("MAtched Ground");
		}
		Debug.Log("Collided");
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
}
