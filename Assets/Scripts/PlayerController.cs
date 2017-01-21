using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject bullet;
	public GameObject camera;
	public GameObject messageBoxObject;
	private GameObject linkedTarget;
	private MessageBox messageBox;
	void Start () {
		Physics.gravity = new Vector3(0, -0.2F, 0);
		GameObject levelObject = GameObject.Find ("LevelObject");
		Physics.bounceThreshold = 0;
		messageBox = messageBoxObject.GetComponent<MessageBox> ();
	}

	// Update is called once per frame
	void Update () {
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
