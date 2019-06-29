using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggCTRL : MonoBehaviour {

	float timer = 2f;
	bool start = false;
	// Use this for initialization
	void Start () {
		
	}

	public void egg2(){
		timer = 2f;
		start = true;
	}

	// Update is called once per frame
	void Update () {
		if (start) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				this.GetComponent<Animation> ().Play ("egg2");
				start = false;
			}
		}
		
	}
}
