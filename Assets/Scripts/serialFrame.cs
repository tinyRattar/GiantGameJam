using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class serialFrame : MonoBehaviour {
	[SerializeField]List<Sprite> lsp;
	[SerializeField]Image img;
	[SerializeField]int index = 0;
	int strides = 1;

	// Use this for initialization
	void Start () {
		//img = this.GetComponent<Image> ();
	}

	public void NextSp(){
		if ((index + strides < lsp.Count) && (index + strides >= 0)) {
			index += strides;
			img.sprite = lsp [index];
		} else {
			Debug.LogWarning ("wrong index");
		}
	}

	public void turnBack(){
		strides = -strides;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
