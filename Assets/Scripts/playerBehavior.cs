using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerBehavior : MonoBehaviour {

	public bool onTouchBegan = false;
	public int health = 3;
	Animation oilAnim;
	Animation panAnim;
	Animation eggAnim;
	bubbleGenerator bGen;
	keyGenerator kGen;
	bgmManager bgm;
	bgmManager boil;
	bgmManager se;
	[SerializeField]List<Image> listImage;
	[SerializeField]public float keySpeed;
	float timerPreset = 0f;
	float timePreset = 2f;
	float timeBubbleMore = 0.5f;
	float timeOilShake = 1f;
	int oilStage = 0;
	int stage = -1;
	bool onGame = true;

	int score1 = 0;
	int score2 = 0;

	void init(){
		oilStage = 0;
		timerPreset = 0f;
		health = 3;
		stage = -1;
		score1 = 0;
		score2 = 0;
	}
	// Use this for initialization
	void Start () {
		oilAnim = GameObject.Find ("oil").GetComponent<Animation> ();
		panAnim = GameObject.Find ("pan").GetComponent<Animation> ();
		eggAnim = GameObject.Find ("egg1").GetComponent<Animation> ();
		bGen = GameObject.Find ("bubbleCore").GetComponent<bubbleGenerator> ();
		kGen = GameObject.Find ("keyGenerator").GetComponent<keyGenerator> ();

		bgm = GameObject.Find ("BGMManager").GetComponent<bgmManager> ();
		boil = GameObject.Find ("boilManager").GetComponent<bgmManager> ();
		se = GameObject.Find ("SEManager").GetComponent<bgmManager> ();
	}

	public void playSE(int index){
		se.playBGM (index);
	}

	public void healthDown(int value){
		if (onGame) {
			health -= value;
			if (health <= 0) {
				health = 0;
				onGameOver ();
			}
		}
		updateUI ();
	}

	public void healthUp(int value){
		//health += value;
		if (health > 3)
			health = 3;
		updateUI ();
	}

	void updateUI(){
		if (health < 1) {
			if (listImage [0].fillClockwise) {
				listImage [0].transform.parent.gameObject.GetComponent<Animation> ().Play ("heartOut");
			}
			listImage [0].fillClockwise = false;
		} else {
			listImage [0].fillClockwise = true;
		}
		if (health < 2) {
			if (listImage [1].fillClockwise) {
				listImage [1].transform.parent.gameObject.GetComponent<Animation> ().Play ("heartOut");
			}
			listImage [1].fillClockwise = false;
		} else {
			listImage [1].fillClockwise = true;
		}
		if (health < 3) {
			if (listImage [2].fillClockwise) {
				listImage [2].transform.parent.gameObject.GetComponent<Animation> ().Play ("heartOut");
			}
			listImage [2].fillClockwise = false;
		} else {
			listImage [2].fillClockwise = true;
		}
	}

	public void gamestart(){
		if(stage != -1){
			Debug.LogWarning ("something wrong");
			return;
		}
		stagePush();
	}

	public void stage1Recv(){
		if(stage != 1){
			Debug.LogWarning ("something wrong");
			return;
		}
		score1 += 1;
		if (score1 >= 1) {
			stagePush ();
		}
	}

	public void stage2Recv(){
		if(stage != 2){
			Debug.LogWarning ("something wrong");
			return;
		}
		score2 += 1;
		if (score2 >= 1) {
			stagePush ();
		}
	}

	public void stage3Recv(){
		if (stage != 3) {
			Debug.LogWarning ("something wrong");
			return;
		}
		stagePush ();
	}

	void stagePush(){
		switch (stage) {
		case -1: //main menu-> preset
			bgm.playBGM(0);
			oilStage = 0;
			stage = 0;
			bGen.stageChange (0);
			break;
		case 0: //preset-> stage1
			kGen.gameStart ();
			oilStage = 1;
			oilAnim.Play ("oil0");
			boil.playBGM (0);
			bGen.stageChange (1);
			stage = 1;
			break;
		case 1: // stage1->stage2
			//bGen.stageChange (2);
			bGen.stageChange (2);
			GameObject.Find ("eggbreakBar").GetComponent<RectTransform> ().localScale = Vector3.zero;
			GameObject.Find ("barIcon").GetComponent<RectTransform> ().localScale = Vector3.zero;
			GameObject.Find ("barIcon2").GetComponent<RectTransform> ().localScale = Vector3.one;
			GameObject.Find ("keyCursor").GetComponent<RectTransform> ().localScale = Vector3.one;
			GameObject.Find ("keyBar_mask").GetComponent<RectTransform> ().sizeDelta = new Vector2 (10f, 1.4f);
			GameObject.Find ("Whac-A-Hole").GetComponent<wahManager> ().onGen = true;
			eggAnim.Play ("eggIn");
			stage = 2;
			kGen.stageChange (2);
			boil.playBGM (1);
			break;
		case 2: 
			oilAnim.Play ("oil1");
			boil.playBGM (2);
			eggAnim.Play ("egg3");
			//GameObject.Find ("Whac-A-Hole").GetComponent<wahManager> ().onGen = false;
			GameObject.Find ("cat").GetComponent<catGenerator> ().onGen = true;
			// stage2->stage3
			//oilAnim.Play ("oil1");
			stage = 3;
			break;
		case 3:
			GameObject.Find ("cat").GetComponent<catGenerator> ().onGen = false;
			onGameWin ();
			stage = 4;

			break;
		}
	}

	void oilAutoChange(){
		if (oilStage == 1) {
			timerPreset += Time.deltaTime;
			if (timerPreset > timeBubbleMore) {
				oilStage = 2;
				timerPreset = 0f;
			}
		} else if (oilStage == 2) {
			timerPreset += Time.deltaTime;
			if (timerPreset > timeOilShake) {
				GameObject.Find ("eggCursor").GetComponent<eggBreaker> ().init ();
				GameObject.Find ("eggbreakBar").GetComponent<RectTransform> ().localScale = Vector3.one;
				GameObject.Find ("barIcon").GetComponent<RectTransform> ().localScale = Vector3.one;
				GameObject.Find ("eggCursor").GetComponent<Animation> ().Play ();
				GameObject.Find ("rawEgg").GetComponent<Animation> ().Play ();
				oilStage = 3;
				timerPreset = 0f;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		switch(stage){
		case -1: //main menu
			if (Input.GetKeyDown (KeyCode.Space)) { //oil in
				//stagePush ();
			}
			break;
		case 0: // stage 1
			timerPreset += Time.deltaTime;
			if (timerPreset > timePreset) {
				stagePush ();
				oilAnim.Play ("oilIn");
				timerPreset = 0;
			}

			break;
		case 1:
			break;
			
		}

		if (!onGame) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene ("main");
			}

		} else {
			if (Input.GetKeyDown (KeyCode.L)) {
				onGameWin ();
			}
		}

		oilAutoChange ();
	}

	public void onGameOver(){
		if (onGame) {
			boil.stopBGM ();
			GameObject.Find ("HappyEnd").GetComponent<Animation> ().Play ("be");
			bgm.playBGM(2);
			onGame = false;

			se.setDisable ();
		}
	}

	public void onGameWin(){
		if (onGame) {
			boil.stopBGM ();
			GameObject.Find ("HappyEnd").GetComponent<Animation> ().Play ("he");
			bgm.playBGM(1);
			onGame = false;

			se.setDisable ();
		}
	}

	public void gameRestart(){
		SceneManager.LoadScene ("main4phone");
	}
}
