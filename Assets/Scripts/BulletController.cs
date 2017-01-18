using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject player;
	public int damage;
	private PlayerController playerController;
	// Use this for initialization
	void Start () {
		playerController = player.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (!other.CompareTag ("Player")) {
			Debug.Log ("COLLIDED");
			playerController.BulletCollided (gameObject);
		}
	}

	public int Damage() {
		return damage;
	}
}
