using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject damageSprite;
	public GameObject winSprite;
	public ParticleSystem beam;
	private SpriteController damageSpriteController;
	private SpriteController winSpriteController;
	private bool isLinked;
	private GameObject linkedTarget;
	private Rigidbody rb;
	private LevelController levelController;
	void Start () {
		isLinked = false;
		linkedTarget = null;
		rb = GetComponent<Rigidbody> ();
		Physics.gravity = new Vector3(0, -0.2F, 0);
		damageSpriteController = damageSprite.GetComponent<SpriteController> ();
		winSpriteController = winSprite.GetComponent<SpriteController> ();
		GameObject levelObject = GameObject.Find ("LevelObject");
		levelController = levelObject.GetComponent<LevelController> ();
		//PlayerPrefs.DeleteAll ();
		LoadCurrentLevel ();
		Physics.bounceThreshold = 0;
	}

	public void NewTarget(GameObject target) {
		Unlink ();
		Link (target);
	}

	public void Link(GameObject target) {
		linkedTarget = target;
		linkedTarget.GetComponent<SphereController> ().SetLinked (true);
		beam.maxParticles = 100;
		isLinked = true;
	}

	public void PointParticles(GameObject target) {
		float distance = Vector3.Distance (transform.position, target.transform.position) - 1f;
		beam.startLifetime = distance / beam.startSpeed;
		beam.transform.LookAt (target.transform.position);
	}


	public void Unlink() {
		if (isLinked) {
			linkedTarget.GetComponent<SphereController> ().SetLinked (false);
		}
		isLinked = false;
		linkedTarget = null;
		beam.maxParticles = 0;
	}

	// Update is called once per frame
	void Update () {
		if (isLinked) {
			Vector3 velocityAdd = (linkedTarget.transform.position - transform.position).normalized / 50;
			rb.velocity += velocityAdd;
			PointParticles (linkedTarget);
		} else {
			//ensure no link is occuring
			linkedTarget = null;
			beam.maxParticles = 0;
			if (Vector3.Distance (transform.position, new Vector3 (0, 0, 0)) > 150) {
				Reset ();
			}
		}
	}

	//Used to move player back to start
	void Reset() {
		rb.velocity = new Vector3 (0, 0, 0);
		transform.position = new Vector3 (0, .5f, 0);
		damageSpriteController.Flash ();
		Handheld.Vibrate ();
		isLinked = false;
		linkedTarget = null;
	}


	//Used to clear links on new level
	public void Clear() {
		Unlink ();
	}

	public void Success() {
		//TODO score?
		winSpriteController.Flash();
		LoadNextLevel ();
	}

	void LoadCurrentLevel() {
		int currentLevel = PlayerPrefs.GetInt ("CurrentLevel");
		if (currentLevel < 1 || currentLevel > LevelProvider.GetLevels().Length) {
			PlayerPrefs.SetInt ("CurrentLevel", 1);
			currentLevel = 1;
		}
		levelController.LoadLevel (LevelProvider.GetLevel (currentLevel));
	}

	void LoadNextLevel() {
		int nextLevel = PlayerPrefs.GetInt ("CurrentLevel") + 1;
		PlayerPrefs.SetInt ("CurrentLevel", nextLevel);
		LevelProvider.OpenLevelLock (nextLevel);
		PlayerPrefs.Save ();
		LoadCurrentLevel ();
	}

	public void BackToLevels() {
		Application.LoadLevel("LevelScene");
	}
}
