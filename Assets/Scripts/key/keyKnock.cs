using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyKnock : keyBar {
	[SerializeField]float dMiss = 0f;
	[SerializeField]float dHit1 = 0.5f;
	[SerializeField]float dPerfect = 1.0f;
	[SerializeField]float dHit2 = 1.5f;
	[SerializeField]int blockPoint = 5;

	[SerializeField]float knockMin = 8f;
	[SerializeField]float knockMax = 8f;
	[SerializeField]float knockAcc = 2f;
	[SerializeField]int mode = 1;
	bool turnFlag = true;
	float knockSpeed = 0f;
	bool onKnock = false;
	Animation panAnim;

	// Use this for initialization
	void Start () {
		onStart ();
		panAnim = GameObject.Find ("pan").GetComponent<Animation> ();
		//spawn ();
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
		updateBPCount ();
	}

	public void spawn_new(float dm,float dh1,float dp,float dh2,int bp){
		dMiss = dm;
		dHit1 = dh1+dm;
		dPerfect = dp+dh1+dm;
		dHit2 = dh2+dp+dh1+dm;
		blockPoint = bp;
		spawn ();
	}

	void onMiss(){
		Debug.Log ("miss");
		player.healthDown (1);
	}

	void onHit(){
		Debug.Log ("hit");
		player.healthDown (1);
		knockBack (knockMax,knockMax);

		if (mode == 1) {
			int a = Random.Range (0, 2);
			if (a == 0)
				panAnim.Play ("panCoolPerfect");
			else
				panAnim.Play ("panCoolPerfect_Left");
		} else {
			if (turnFlag)
				panAnim.Play ("panTurn0");
			else
				panAnim.Play ("panTurn");
			turnFlag = !turnFlag;
		}
	}

	void onPerfect(){
		updateBPCount ();
		if (onKnock)
			return;
		blockPoint -= 1;

		if (blockPoint <= 0) {
			onExit ();
			if (mode == 1) {
				player.stage2Recv ();
			} else {
				player.stage3Recv ();
			}
			Debug.Log ("knock key done");
		} else {
			knockBack ();
			if (mode == 1) {
				speed -= 1f;
			}
		}

		player.playSE (2);
		if (mode == 1) {
			int a = Random.Range (0, 2);
			if (a == 0)
				panAnim.Play ("panCool");
			else
				panAnim.Play ("panCool_Left");
		} else {
			if (turnFlag)
				panAnim.Play ("panTurn0");
			else
				panAnim.Play ("panTurn");
			turnFlag = !turnFlag;
		}
		//Debug.Log ("perfect");
	}

	protected override void onPass ()
	{
		if (distance () > 0) {
			knockBack (knockMax, knockMax);
			player.healthDown (3);
		}
	}

	protected override void onExit ()
	{
		GameObject.Find ("keyGenerator").GetComponent<keyGenerator> ().onGen = true;
		base.onExit ();
	}

	void knockBack(){
		onKnock = true;
		knockSpeed = Random.Range (knockMin, knockMax);
	}

	void knockBack(float min, float max){
		onKnock = true;
		knockSpeed = Random.Range (min, max);
	}

	void updateBPCount(){
		Debug.LogWarning ("todo: update bp");
	}

	protected override void autoMove ()
	{
		if (onKnock) {
			this.transform.Translate (new Vector3 (knockSpeed, 0f, 0f) * Time.deltaTime);
			knockSpeed -= Time.deltaTime * knockAcc;
			if (knockSpeed < 0) {
				onKnock = false;
			}
		} else {
			base.autoMove ();
		}
	}

	protected override void onEnter ()
	{
		base.onEnter ();
		panAnim.Play ("panHot");
	}


	// Update is called once per frame
	void Update () {
		autoMove ();
		if ((Input.GetKeyDown (KeyCode.F)||player.onTouchBegan) && onCursor && !onKnock) {
			float d = distance ();
			Debug.Log (d);
			//onCursor = false;
			if (d < dMiss) {
				onMiss ();
			} else if (d < dHit1) {
				onHit ();
			} else if (d < dPerfect) {
				onPerfect ();
			} else {
				onHit ();
			}
			//onExit ();
		}
	}
}
