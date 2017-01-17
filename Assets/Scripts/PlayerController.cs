using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject bullet;
	private GameObject camera;
	private Rigidbody rb;
	void Start () {
		rb = GetComponent<Rigidbody> ();
		Physics.gravity = new Vector3(0, -0.2F, 0);
		//PlayerPrefs.DeleteAll ();
		LoadCurrentLevel ();
		Physics.bounceThreshold = 0;
		camera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	// Update is called once per frame
	void Update () {
	}


	public void Success() {
		//TODO score?
		LoadNextLevel ();
	}

	void LoadCurrentLevel() {
		int currentLevel = PlayerPrefs.GetInt ("CurrentLevel");
		if (currentLevel < 1) {
			PlayerPrefs.SetInt ("CurrentLevel", 1);
			currentLevel = 1;
		}
	}

	void LoadNextLevel() {
		int nextLevel = PlayerPrefs.GetInt ("CurrentLevel") + 1;
		PlayerPrefs.SetInt ("CurrentLevel", nextLevel);
		PlayerPrefs.Save ();
		LoadCurrentLevel ();
	}

	void Shoot() {
		GameObject newBullet = Instantiate (bullet);
		newBullet.SetActive (true);
		newBullet.transform.position = transform.position + camera.transform.forward;
		Rigidbody bulletRB = newBullet.GetComponent<Rigidbody> ();
		Vector3 theForwardDirection = camera.transform.TransformDirection (Vector3.forward);
		Vector3 realForward = camera.transform.forward;
		bulletRB.AddForce (theForwardDirection * 200f);
	}

	public void BulletCollided(GameObject bulletObj) {
		Destroy (bulletObj);
		Debug.Log ("Destroyed bullet");
	}

	public void BackToLevels() {
		Application.LoadLevel("LevelScene");
	}

	void OnEnable(){
		Cardboard.SDK.OnTrigger += TriggerPulled;
	}

	void OnDisable(){
		Cardboard.SDK.OnTrigger -= TriggerPulled;
	}

	void TriggerPulled() {
		Debug.Log("The trigger was pulled!");
		Shoot ();
	}
}
