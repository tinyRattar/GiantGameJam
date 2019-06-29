using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchInput : MonoBehaviour {

	playerBehavior player;
	bool onclick = false;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ();
	}

	public void onButtonClick(){
		onclick = true;
	}
	// Update is called once per frame
	void Update () {
		if (onclick) {
			player.onTouchBegan = true;
			onclick = false;
		} else {
			player.onTouchBegan = false;
		}
//		int i = 0;
//		player.onTouchBegan = false;
//		while (i < Input.touchCount) {
//			if (Input.GetTouch (i).phase == TouchPhase.Began) {
//				player.onTouchBegan = true;
//			}
//		}
	}
}
