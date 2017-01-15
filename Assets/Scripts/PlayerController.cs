using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject damageSprite;
	public GameObject winSprite;
	public ParticleSystem beam;
	public GameObject bullet;
	public GameObject camera;
	public GameObject messageBoxObject;
	private SpriteController damageSpriteController;
	private SpriteController winSpriteController;
	private bool isLinked;
	private GameObject linkedTarget;
	private Rigidbody rb;
	private MessageBox messageBox;
	void Start () {
		isLinked = false;
		linkedTarget = null;
		rb = GetComponent<Rigidbody> ();
		Physics.gravity = new Vector3(0, -0.2F, 0);
		damageSpriteController = damageSprite.GetComponent<SpriteController> ();
		winSpriteController = winSprite.GetComponent<SpriteController> ();
		GameObject levelObject = GameObject.Find ("LevelObject");
		//PlayerPrefs.DeleteAll ();
		LoadCurrentLevel ();
		Physics.bounceThreshold = 0;
		messageBox = messageBoxObject.GetComponent<MessageBox> ();
	}

	// Update is called once per frame
	void Update () {
		while (messageBox.HasMoreMessages()) {
			HandleMessage (messageBox.GetNextMessage ());
		}
	}

	void HandleMessage(Message message) {
		if (message == null)
			return;

		print ("message: " + message.action);
	}

	public void Success() {
		//TODO score?
		winSpriteController.Flash();
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
		SendShootMessage ();
	}

	void SendShootMessage() {
		Message message = new Message ();
		message.type = "shoot";
		message.action = "shoot";
		messageBox.SendMessage (message);
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
