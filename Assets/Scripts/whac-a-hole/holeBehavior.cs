using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeBehavior : MonoBehaviour {

	[SerializeField]float stayTime = 3.0f;
	[SerializeField]Animation tarAnim;
	playerBehavior player;
	float timerStay;
	bool flagAnim = false;
	bool onStay = false;
	//Camera _camera;
	Animation anima;

	public void setStay(){
		flagAnim = true;
		onStay = true;
		timerStay = stayTime;
		anima.Play ("holeUp");
		//Debug.LogWarning ("todo: anima_setStay");
	}

	void setHide(){
		onStay = false;
		timerStay = stayTime;
	}

	void onMiss(){
		Debug.Log ("miss");
		player.healthDown (1);
		anima.Play ("holeDown");
		setHide ();
	}

	void onHit(){
		Debug.Log ("hit");
		player.healthUp (1);
		anima.Play ("holeDown");
		tarAnim.Play ();
		setHide ();
		player.playSE (5);
	}

	public void animAlert(){
		anima.Play ("holeStay");
	}

	public void onButton(){
		if (onStay)
			onHit ();
	}

	// Use this for initialization
	void Start () {
		anima = this.GetComponent<Animation> ();
		player = GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ();
		//setStay ();
	}
	
	// Update is called once per frame
	void Update () {
		if (onStay) {
//			if (flagAnim && timerStay < stayTime - 2f) {
//				flagAnim = false;
//				anima.Play ("holeStay");
//			}
			timerStay -= Time.deltaTime;
			if (timerStay <= 0)
				onMiss ();
		}
	}
}
