using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = new Vector3 (0, 0, 0);
	}

	void LateUpdate () {
		if (player != null) { //Not used for scenes where you dont move
			transform.position = player.transform.position + offset;
		}
	}
}