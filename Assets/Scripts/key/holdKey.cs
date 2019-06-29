using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holdKey : keyBar {
	[SerializeField]float dMiss = 1f;
	[SerializeField]float dHold = 4f;

	// Use this for initialization
	void Start () {
		onStart ();
	}

	public void spawn(){
		this.GetComponent<BoxCollider2D> ().size = new Vector2 (dHold, 1f);
		this.GetComponent<BoxCollider2D> ().offset = new Vector2 (dHold / 2, 0);
		GameObject holdBar = this.transform.GetChild (0).gameObject;
		holdBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (dHold - dMiss, 0.5f);
		holdBar.transform.localPosition = new Vector3 ((dHold + dMiss) / 2, 0, 0);
	}

	public void spawn_new(float dm,float dh){
		dMiss = dm;
		dHold = dh+dm;
		spawn ();
	}

	void onMiss(){
		Debug.Log ("miss");
	}

	void onHold(){
		Debug.Log ("holding");
	}
		
	
	// Update is called once per frame
	void Update () {
		autoMove ();
		if (Input.GetKey (KeyCode.Space) && onCursor) {
			float d = distance ();
			Debug.Log (d);
			if (d < dMiss) {
				onMiss ();
			} else if (d < dHold) {
				onHold ();
			}
			//onExit ();
		}
	}
}
