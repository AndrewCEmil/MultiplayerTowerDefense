using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject messageBoxObject;
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
		if (health < 0) {
			Die ();
		}
	}

	void Die() {
		//TODO
		Debug.Log("I am dead");
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("ENEMY COLLIDED");
		if (other.CompareTag ("Bullet")) {
			Hit (other.gameObject.GetComponent<BulletController> ());
		}
	}
}
