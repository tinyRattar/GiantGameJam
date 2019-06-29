using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleBehavior : MonoBehaviour {
	[SerializeField]float lifeTime = 0f;
	[SerializeField]bool loadDefaultTime = false;
	[SerializeField]float lifeTimeMin = 1.5f;
	[SerializeField]float lifeTimeMax = 4f;
	[SerializeField]float showTime = 0.5f;
	float timerShow = 0f;


	// Use this for initialization
	void Start () {
		if (loadDefaultTime)
			lifeTime = Random.Range (lifeTimeMin, lifeTimeMax);
		timerShow = 0f;
	}

	public void setLifeTime(float value){
		lifeTime = value;
	}

	public void setShowTime(float value){
		showTime = value;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, timerShow / showTime);
		timerShow += Time.deltaTime;
		Destroy (this.gameObject, lifeTime);
	}
}
