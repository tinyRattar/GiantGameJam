using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyGenerator : MonoBehaviour {

	[SerializeField]GameObject canvas;
	[SerializeField]List<GameObject> listKey;
	[SerializeField]float genTime = 1f;
	[SerializeField]protected Vector3 spawnVector = new Vector3(5f,-2f,0);
	Queue qKey = new Queue ();
	float timerGen = 0f;
	public bool onGen = true;

	public void gameStart(){
		qKey.Clear ();
		onGen = true;
	}

	public void stageChange(int stage){
		switch (stage) {
		case 2:
			qKey.Enqueue (-3);
			qKey.Enqueue (1);
			qKey.Enqueue (-3);
			qKey.Enqueue (2);
			qKey.Enqueue (-5);
			break;
		}
	}
	// Use this for initialization
	void Start () {
		qKey.Enqueue (0);
		qKey.Enqueue (-1);
		qKey.Enqueue (0);
		qKey.Enqueue (-1);
		qKey.Enqueue (0);
		qKey.Enqueue (-1);
		qKey.Enqueue (0);
		qKey.Enqueue (-2);
		qKey.Enqueue (1);
		qKey.Enqueue (-2);
		qKey.Enqueue (1);

		for (int i = 0; i < 10; i++) {
			qKey.Enqueue (-Random.Range(1,2));
			qKey.Enqueue (Random.Range(0,2));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (onGen) {
			if (timerGen < 0) {
				timerGen = genTime;
				if (qKey.Count > 0) {
					int kType = (int)qKey.Dequeue ();
					if (kType >= 0) {
						listKey [kType].transform.position = spawnVector;
						if (kType == 0) {
							listKey [kType].GetComponent<hitKey> ().spawn_new (0f, 15f, 12f, 13f);
						} else if (kType == 1) {
							listKey [kType].GetComponent<keyKnock> ().spawn_new (0f, 1.5f, 1f, 1.5f, 5);
							onGen = false;
						}else if (kType == 2) {
							listKey [kType].GetComponent<keyKnock> ().spawn_new (0f, 1.5f, 1f, 1.5f, 6);
							onGen = false;
						}

						GameObject.Instantiate (listKey [kType], canvas.transform);

						//qKey.Enqueue (-Random.Range(1,2));
						//qKey.Enqueue (Random.Range(0,2));
					} else {
						timerGen = (float)-kType;
					}
				} 
			} else {
				timerGen -= Time.deltaTime;
			}
		}
	}
}
