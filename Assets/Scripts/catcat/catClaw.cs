using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catClaw : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.name);
		if (other.tag == "pan") {
			GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ().healthDown (3);
		}
	}
}
