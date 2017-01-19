using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject messageBoxObject;
	public string enemyId;
	public int hitPoints;

	private int health;
	private MessageBox messageBox;
	void Start () {
		messageBox = messageBoxObject.GetComponent<MessageBox> ();
		health = hitPoints;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Hit(BulletController bulletController) {
		Debug.Log ("got the bullet controller");
		TakeDamage (bulletController.Damage ());
	}

	void TakeDamage (int damageAmount) {
		health = health - damageAmount;
		messageBox.SendMessage (CreateDamageMessage (damageAmount));
		if (health < 0) {
			Die ();
		}
	}

	Message CreateDamageMessage(int damangeAmmount) {
		Message message = new Message ();
		message.action = "" + damangeAmmount;
		message.type = "damage";
		return message;
	}

	Message CreateDeadMessage() {
		Message message = new Message ();
		message.type = "die";
		message.action = "die";
		return message;
	}


	void Die() {
		//TODO
		Debug.Log("I am dead");
		messageBox.SendMessage (CreateDeadMessage());
		gameObject.SetActive (false);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("ENEMY COLLIDED");
		if (other.CompareTag ("Bullet")) {
			Hit (other.gameObject.GetComponent<BulletController> ());
		}
	}
}
