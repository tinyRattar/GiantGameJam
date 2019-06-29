using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleGenerator : MonoBehaviour {
	[SerializeField]GameObject bubble;
	[SerializeField]float genTime = 0.5f;
	[SerializeField]float genBigTime = 1f;
	[SerializeField]float lifeTimeMin = 1f;
	[SerializeField]float lifeTimeMax = 2f;
	[SerializeField]float radiusMin = 1f;
	[SerializeField]float radiusMax = 2f;
	[SerializeField]float bubbleSizeMin = 0.2f;
	[SerializeField]float bubbleSizeMax = 0.5f;
	float timerGen;
	float timerGenBig;
	int stage;
	bool onGen = false;
	bool onGenBig = false;

	[SerializeField]float timeS1 = 3f;
	//[SerializeField]float timeS2 = 0.25f;
	[SerializeField]float timerS = 0f;

	// Use this for initialization
	void Start () {
		timerGen = genTime;
	}

	public void stageChange(int value){
		switch (value) {
		case 0: //main menu
			onGen = false;
			break;
		case 1: // stage 1
			onGen = true;
			genTime = 0.3f;
			radiusMin = 1f;
			radiusMax = 1.65f;
			bubbleSizeMin = 0.2f;
			bubbleSizeMax = 0.4f;
			lifeTimeMin = 1f;
			lifeTimeMax = 2f;
			bubble.GetComponent<bubbleBehavior> ().setShowTime (0.5f);
			timerS = timeS1;
			break;
		case 2:
			genTime = 0.02f;
			radiusMin = 1f;
			radiusMax = 1.65f;
			bubbleSizeMin = 0.2f;
			bubbleSizeMax = 0.4f;
			lifeTimeMin = 0.4f;
			lifeTimeMax = 0.8f;
			bubble.GetComponent<bubbleBehavior> ().setShowTime (0.1f);
			onGenBig = true;
			//timerS = timeS2;
			break;
		case 3:
			break;
		}
		stage = value;
	}

	void genOne(){
		float x = Random.Range(-2f,2f);
		float y = Random.Range (-2f, 2f);
		float d = Mathf.Sqrt (x * x + y * y);
		if (d < radiusMax) {
			float s = Random.Range (bubbleSizeMin, bubbleSizeMax);
			bubble.GetComponent<RectTransform> ().sizeDelta = new Vector2 (s, s);
			bubble.transform.localPosition = new Vector3 (x, y, 0);
			bubble.GetComponent<bubbleBehavior> ().setLifeTime (Random.Range (lifeTimeMin, lifeTimeMax));
			GameObject.Instantiate (bubble, this.transform);
		} else {
			genOne ();
		}
	}

	void genBigOne(){
		float x = Random.Range(-2f,2f);
		float y = Random.Range (-2f, 2f);
		float d = Mathf.Sqrt (x * x + y * y);
		if (d < radiusMax) {
			float s = Random.Range (bubbleSizeMin, bubbleSizeMax);
			bubble.GetComponent<RectTransform> ().sizeDelta = new Vector2 (s*2, s*2);
			bubble.transform.localPosition = new Vector3 (x, y, 0);
			bubble.GetComponent<bubbleBehavior> ().setLifeTime (Random.Range (lifeTimeMin*2, lifeTimeMax*2));
			GameObject.Instantiate (bubble, this.transform);
		} else {
			genOne ();
		}
	}
	// Update is called once per frame
	void Update () {
		if (onGen) {
			if (timerGen < 0) {
				timerGen = genTime;
				genOne ();
			} else {
				timerGen -= Time.deltaTime;
			}
			if (timerGenBig < 0 && onGenBig) {
				timerGenBig = genBigTime;
				genBigOne ();
			} else {
				timerGenBig -= Time.deltaTime;
			}
		}
	}
}
