using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eggBreaker : MonoBehaviour {
	bool onCursor = true;
	playerBehavior player;
	GameObject rawEgg;
	GameObject eggShellCopy;
	[SerializeField]int health = 3;
	float range = 0.8f;
	bool onStage = false;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ();
		rawEgg = GameObject.Find ("rawEgg");
		eggShellCopy = GameObject.Find ("eggShell_copy");
	}

	public void init(){
		health = 3;
		rawEgg.GetComponentInChildren<Image> ().color = new Color (1f, 1f, 1f, 1f);
		onStage = true;
		//rawEgg.GetComponent<Animation> ().Play ("rawEggIn");
	}


	void hit(){
		health -= 1;
		updateUI ();
		//rawEgg.GetComponentInChildren<Image> ().color = new Color (1f, 1f, 1f, 0f);
		player.playSE(0);
		eggShellCopy.GetComponent<Animation>().Play("rawEggHit");
		if (health <= 0) {
			eggBreak ();
		}
	}

	void eggBreak(){
		Debug.Log ("egg break");
		player.stage1Recv ();
		rawEgg.transform.localScale = Vector3.zero;
		onStage = false;
	}

	void crash(){
		player.healthDown (1);
		player.playSE(1);
		rawEgg.GetComponentInChildren<Image> ().color = new Color (1f, 1f, 1f, 0f);
		eggShellCopy.GetComponent<Animation>().Play("rawEggCrash");
		health = 3;
		updateUI ();
	}

	public void resetPos(){
		rawEgg.GetComponentInChildren<Image> ().color = new Color (1f, 1f, 1f, 1f);
		//rawEgg.GetComponent<Animation> ().Play ("rawEggSwing");
	}

	public void resetPos_fromCrash(){
		health = 3;
		rawEgg.GetComponentInChildren<Image> ().color = new Color (1f, 1f, 1f, 1f);
		updateUI ();
	}
		
	void updateUI(){
		//Debug.LogWarning ("egg update");
		if (health <= 2) {
			GameObject.Find("crash1").GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
		} else {
			GameObject.Find("crash1").GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0f);
		}
		if (health <= 1) {
			GameObject.Find("crash2").GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
		} else {
			GameObject.Find("crash2").GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0f);
		}
	}

	// Update is called once per frame
	void Update () {
		if (onStage) {
			if ((Input.GetKeyDown (KeyCode.F)||player.onTouchBegan) && onCursor) {
				float x = this.transform.position.x;
				//Debug.Log (x);
				if (Mathf.Abs (x) < range) {
					hit ();
				} else {
					crash ();

				}
			}
		}
	}
}
