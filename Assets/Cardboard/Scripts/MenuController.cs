using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public Slider musicSlider;
	public Toggle particleToggle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadGame() {
		Application.LoadLevel("DemoScene");
	}

	public void LoadSettings() {
		Application.LoadLevel ("SettingsScene");
	}

	public void LoadStart() {
		Application.LoadLevel ("StartScene");
	}

	public void LoadLevelSelection() {
		Application.LoadLevel ("LevelScene");
	}

	public void LoadAboutScene() {
		Application.LoadLevel ("AboutScene");
	}

	public void SetMusicVolume() {
		AudioSource musicPlayer = GameObject.Find ("MusicPlayer").GetComponent<AudioSource> ();
		musicPlayer.volume = musicSlider.value;
	}

	//Reversed so we default to particles on
	public void SetParticleToggle() {
		if (particleToggle.isOn) {
			PlayerPrefs.SetInt ("ParticlesOff", 0);
		} else {
			PlayerPrefs.SetInt ("ParticlesOff", 1);
		}
		PlayerPrefs.Save ();
	}
}
