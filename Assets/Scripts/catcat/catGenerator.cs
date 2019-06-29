using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catGenerator : MonoBehaviour {
	[SerializeField]GameObject catHand;
	[SerializeField]float genTime = 5f;
	playerBehavior player;
	public bool onGen=false;
	float timerGen = 5f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ();
	}

	public void genOne(){
		float theta = Random.Range (30f, 45f);
		int a = Random.Range (0, 2);
		if (a==0)
			theta = -theta;
		GameObject go = GameObject.Instantiate (catHand,this.transform);
		go.transform.Rotate (new Vector3(0,0,theta));
		player.playSE (3);
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
		}
		
	}
}
