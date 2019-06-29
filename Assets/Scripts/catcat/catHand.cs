using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class catHand : MonoBehaviour {
	bool onMove = true;
	bool onBack = false;
	[SerializeField]float speed = 0.5f;
	[SerializeField]float backSpeed = 2f;
	[SerializeField]Sprite sp;
	[SerializeField]Image cathand;
	GameObject claw;
	// Use this for initialization
	void Start () {
		claw = this.transform.GetChild (0).gameObject;
	}

	public void onHit(){
		onMove = false;
		this.GetComponent<Animation> ().Play ();
		cathand.sprite = sp;
	}

	public void startBack(){
		onMove = false;
		onBack = true;
		Destroy (this.gameObject, 2f);
	}

	// Update is called once per frame
	void Update () {
		if (onMove) {
			claw.transform.Translate (new Vector3 (0, speed, 0) * Time.deltaTime);
		} else if (onBack) {
			claw.transform.Translate (new Vector3 (0, -backSpeed, 0) * Time.deltaTime);
		}
		
	}

//	protected void OnTriggerEnter2D(Collider2D other){
//		if (other.tag == "pan") {
//			GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ().healthDown (3);
//		}
//	}
}
