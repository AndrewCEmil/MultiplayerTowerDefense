using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {

	// Use this for initialization
	GameObject player;
	PlayerController playerController;
	private bool isColliding;
	void Start () {
		gameObject.transform.position = new Vector3 (2, 0, 2);
		player = GameObject.Find ("Player");
		playerController = player.GetComponent<PlayerController> ();
		isColliding = false;
	}

	void Place() {
		gameObject.transform.position = GenerateNewPosition ();
	}

	Vector3[] GetPositions() {
		Vector3[] wellPositions = GetWellPositions();
		wellPositions [0] = player.transform.position;
		return wellPositions;
	}

	void Update () {
		isColliding = false;
	}
	Vector3[] GetWellPositions() {
		GameObject[] wells = GameObject.FindGameObjectsWithTag ("Well");
		Vector3[] positions = new Vector3[wells.Length + 1];
		for (int i = 1; i < wells.Length + 1; i++) {
			positions [i] = wells [i - 1].transform.position;
		}
		return positions;
	}

	float GetNearestWellDistance(Vector3 newPosition) {
		Vector3[] wells = GetWellPositions ();
		float min = float.MaxValue;
		foreach(Vector3 well in wells) {
			if (Vector3.Distance (newPosition, well) < min) {
				min = Vector3.Distance (newPosition, well);
			}
		}
		return min;
	}

	private Vector3 GenerateNewPosition() {
		Vector3 pos = Random.insideUnitSphere * 15.0f;
		pos.y = Mathf.Abs(pos.y);
		if (GetNearestWellDistance (pos) < 10) {
			return GenerateNewPosition ();
		}
		return pos;
	}


	void OnTriggerEnter(Collider other) {
		if(other.name == "Player" && !isColliding) {
			Place ();
			Handheld.Vibrate ();
			playerController.Success ();
		}
		isColliding = true;
	}
}
