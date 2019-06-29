using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellCopyBehavior : MonoBehaviour {

	eggBreaker eg;
	// Use this for initialization
	void Start () {
		eg = GameObject.Find ("eggCursor").GetComponent<eggBreaker> ();
	}

	public void resetPos(){
		eg.resetPos ();
	}

	public void resetPos_fromCrash(){
		eg.resetPos_fromCrash ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
