using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heartBreaker : MonoBehaviour {
	[SerializeField]List<Animation> listEndHeart; 
	[SerializeField]List<Sprite> ListText;
	[SerializeField]Image shortword;
	float timer = 0.5f;
	bool onHeartShow = false;
	int count;
	int curIndex;
	// Use this for initialization
	void Start () {
	}

	public void breakHeart(){
		GameObject.Find ("shibai_BG").GetComponent<Animation> ().Play ();
	}

	public void setWord(){
		count = GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ().health;
		shortword.sprite = ListText [count - 1];
	}

	public void heartShow(){
		count = GameObject.Find ("playerCTRL").GetComponent<playerBehavior> ().health;
		onHeartShow = true;
	}

	// Update is called once per frame
	void Update () {
		if (onHeartShow) {
			if (timer < 0f) {
				if (curIndex < count) {
					listEndHeart [curIndex].Play ("heartIn");
					curIndex++;
					timer = 0.5f;
				} else {
					onHeartShow = false;
				}
			} else {
				timer -= Time.deltaTime;
			}
		}
	}
}
