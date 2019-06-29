using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fireCTRL2 : MonoBehaviour {

	[SerializeField]float speed;
	[SerializeField]Vector3 spawnPos;
	[SerializeField]GameObject keyBar;

	[SerializeField]float keyPosY;
	[SerializeField]float keyPosMin;
	[SerializeField]float keyPosMax;

	[SerializeField]float keyRangeMin;
	[SerializeField]float keyRangeMax;
	// Use this for initialization
	void Start () {
		genKeyBar ();
	}

	void genKeyBar(){
		keyBar.transform.position = new Vector3 (Random.Range (keyPosMin, keyPosMax), keyPosY, 0);
		keyBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Random.Range (keyRangeMin, keyRangeMax),0.5f);
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector3 (speed, 0, 0) * Time.deltaTime);
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (Mathf.Abs (this.transform.position.x - keyBar.transform.position.x) <= keyBar.GetComponent<RectTransform> ().sizeDelta.x) {
				Debug.Log ("yes!");
			}else{
				Debug.Log ("no!");
			}
			this.transform.position = spawnPos;
			genKeyBar ();
		}
	}
}
