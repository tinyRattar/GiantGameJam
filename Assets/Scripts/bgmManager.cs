using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmManager : MonoBehaviour {

	[SerializeField]List<AudioClip> audioList;
	[SerializeField]bool se = false;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = this.gameObject.GetComponent<AudioSource> ();
	}

	public void playBGM(int index){
		if (se) {
			GameObject go = GameObject.Instantiate (this.gameObject);
			AudioSource audioS = go.GetComponent<AudioSource> ();
			audioS.clip = audioList [index];
			audioS.Play ();
			Destroy (go, 3f);
		} else {
			audio.clip = audioList [index];
			audio.Play ();
		}
	}

	public void stopBGM(){
		audio.Stop ();
	}

	public void setDisable(){
		audio.volume = 0f;
	}

	// Update is called once per frame
	void Update () {
	}
}
