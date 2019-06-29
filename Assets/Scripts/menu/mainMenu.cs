using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour {

	bool started = false;
	// Use this for initialization
	void Start () {
		
	}

	public void gameStart(){
		if (!started) {
			GameObject.Find ("HappyEnd").GetComponent<Animation> ().Play ("camera_start");
			this.GetComponent<Animation> ().Play ("gamestart_menu");
			GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ().gamestart ();
			started = true;
		}
	}

	public void HelperIn(){
		this.GetComponent<Animation> ().Play ("helperIn");
	}

	public void HelperOut(){
		this.GetComponent<Animation> ().Play ("helperOut");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
