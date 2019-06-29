using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireCTRL : MonoBehaviour {

	float curTemp;

	float curSpeed;
	float tarSpeed;

	[SerializeField]float threshHot;
	[SerializeField]float threshCold;

	[SerializeField]float adjustSpeed= 2f;
	float autoMoveSpeed = 0f;
	[SerializeField]float distrubChangeTime = 2.0f; //min
	[SerializeField]float distrubChangeTime_offset = 0.1f;

	[SerializeField]float distrubProbability= 0.3f;
	[SerializeField]float distrubRange= 1f;

	float timerDistrubChange = 255f;
	float timerDistrubChange_offset = 0f;

	[SerializeField]bool onMove;


	// Use this for initialization
	void Start () {
		curTemp = 0;
	}

	void autoDistrub(){
		if (timerDistrubChange > distrubChangeTime) {
			if (timerDistrubChange_offset > distrubChangeTime_offset) {
				timerDistrubChange_offset = 0f;
				if (Random.Range (0, 1f) < distrubProbability) {
					timerDistrubChange = 0f;
					curSpeed = autoMoveSpeed;
					tarSpeed = Random.Range (-distrubRange, distrubRange);
					//autoMoveSpeed = Random.Range (-distrubRange, distrubRange);
				}
			} else {
				timerDistrubChange_offset += Time.deltaTime;
			}
		} else {
			autoMoveSpeed = Mathf.Lerp (curSpeed, tarSpeed, timerDistrubChange / 1f);
		}
		timerDistrubChange += Time.deltaTime;

		this.transform.Translate (new Vector3 (autoMoveSpeed, 0, 0) * Time.deltaTime);
	}

	void autoDistrub_accelerate(){
		
	}

	// Update is called once per frame
	void Update () {
		if (onMove) {
			autoDistrub ();
			if (Input.GetKey (KeyCode.A)) {
				this.transform.Translate (new Vector3 (-adjustSpeed, 0, 0) * Time.deltaTime);
			} else if (Input.GetKey (KeyCode.D)) {
				this.transform.Translate (new Vector3 (adjustSpeed, 0, 0) * Time.deltaTime);
			}
		}
		/*
		if (Input.GetKey (KeyCode.A)) {
			//this.transform.Translate (new Vector3 (-adjustSpeed, 0, 0) * Time.deltaTime);
			if (accelerate >= 0f) {
				accelerate = 0f;
			}
			accelerate -= Time.deltaTime * 2f;
			if (accelerate < -6f)
				accelerate = -6f;
		} else if (Input.GetKey (KeyCode.D)) {
			//this.transform.Translate (new Vector3 (adjustSpeed, 0, 0) * Time.deltaTime);
			if (accelerate <= 0f) {
				accelerate = 0f;
			}
			accelerate += Time.deltaTime * 2f;
			if (accelerate > 6f)
				accelerate = 6f;
		} else {
			accelerate = 0f;
		}
		speed = speed + accelerate * Time.deltaTime;
		this.transform.Translate (new Vector3 (speed, 0, 0) * Time.deltaTime);
		*/
		
	}
}
