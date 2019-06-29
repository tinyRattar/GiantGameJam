using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wahManager : MonoBehaviour {

	[SerializeField]List<holeBehavior> listHole;
	[SerializeField]float genTime;
	playerBehavior player;
	public bool onGen = false;
	float timerGen = 5f;

	// Use this for initialization
	void Start () {
		timerGen = genTime;
		player = GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ();
	}

	void generateOne(int index){
		listHole [index].setStay ();
		player.playSE (4);
	}

	// Update is called once per frame
	void Update () {
		if (onGen) {
			if (timerGen <= 0) {
				int ranI = Random.Range (0, listHole.Count);
				generateOne (ranI);
				timerGen = genTime;
			} else {
				timerGen -= Time.deltaTime;
			}
		}
	}
}
