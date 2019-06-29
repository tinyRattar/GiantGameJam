using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyBar : MonoBehaviour {
	[SerializeField]protected float speed;
	[SerializeField]protected Vector3 spawnVector = new Vector3(5f,-2f,0);
	protected playerBehavior player;

	protected bool onCursor = false;
	// Use this for initialization
	protected void onStart () {
		player = GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ();
		speed = -player.keySpeed;
	}

	protected virtual void onExit(){
		speed = 0f;
		this.GetComponent<Animation> ().Play ();
		Destroy (this.gameObject, 0.5f);
	}

	protected virtual void onPass(){
		player.healthDown (3);
		Destroy (this.gameObject, 1f);
	}

	protected virtual void onEnter(){

	}
		
//	public virtual void func2 (){
//		Debug.Log ("base func2");
//	}
	//abstract void func3();
	// Update is called once per frame

	protected float distance(){
		//GameObject cursor = GameObject.Find ("keyCursor");
		return -(this.transform.position.x + 4f);
	}

	protected virtual void autoMove () {
		this.transform.Translate (new Vector3 (speed, 0, 0)*Time.deltaTime);
	}

	protected void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "cursor") {
			Debug.Log ("tirrgerEnter");
			onCursor = true;
			onEnter ();
		}
	}

	protected void OnTriggerExit2D(Collider2D other){
		if (other.tag == "cursor") {
			Debug.Log ("tirrgerExit");
			onCursor = false;
			onPass ();
		}
	}
}
