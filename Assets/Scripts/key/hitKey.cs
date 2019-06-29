using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitKey : keyBar {
	[SerializeField]float dMiss = 1f;
	[SerializeField]float dHit1 = 2f;
	[SerializeField]float dPerfect = 2.5f;
	[SerializeField]float dHit2 = 3f;

	// Use this for initialization
	void Start () {
		onStart ();
	}

	public void spawn(){
		this.GetComponent<BoxCollider2D> ().size = new Vector2 (dHit2, 1f);
		this.GetComponent<BoxCollider2D> ().offset = new Vector2 (dHit2 / 2, 0);
		GameObject hitBar = this.transform.GetChild (0).gameObject;
		GameObject perfectBar = this.transform.GetChild (1).gameObject;
		hitBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (dHit2 - dMiss, 0.5f);
		hitBar.transform.localPosition = new Vector3 ((dHit2 + dMiss) / 2, 0, 0);
		perfectBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (dPerfect - dHit1, 0.5f);
		perfectBar.transform.localPosition = new Vector3 ((dPerfect + dHit1) / 2, 0, 0);
	}

	public void spawn_new(float dm,float dh1,float dp,float dh2){
		dMiss = dm;
		dHit1 = dh1+dm;
		dPerfect = dp+dh1+dm;
		dHit2 = dh2+dp+dh1+dm;
		spawn ();
	}

	void onMiss(){
		Debug.Log ("miss");
		player.healthDown (3);
	}

	void onHit(){
		Debug.Log ("hit");
		player.stage1Recv ();
		player.healthDown (1);
	}

	void onPerfect(){
		Debug.Log ("perfect");
		player.stage1Recv ();
		player.healthUp (1);
	}

	public void onEggBreak(){
		float d = distance ();
		//Debug.Log (d);
		onCursor = false;
		if (d < dMiss) {
			onMiss ();
		} else if (d < dHit1) {
			onHit ();
		} else if (d < dPerfect) {
			onPerfect ();
		} else {
			onHit ();
		}
		onExit ();
	}

	// Update is called once per frame
	void Update () {
		autoMove ();
//		if (Input.GetKeyDown (KeyCode.Space) && onCursor) {
//			float d = distance ();
//			Debug.Log (d);
//			onCursor = false;
//			if (d < dMiss) {
//				onMiss ();
//			} else if (d < dHit1) {
//				onHit ();
//			} else if (d < dPerfect) {
//				onPerfect ();
//			} else {
//				onHit ();
//			}
//			onExit ();
//		}
	}
}
