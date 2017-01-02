using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelsController : MonoBehaviour {

	public GameObject baseSelector;
	static float theta = Mathf.PI / 6.0f;
	static float radius = 5f;
	void Start () {
		GenerateSelectors ();
	}

	public void HandleLevelSelection(int level) {
		//TODO
		PlayerPrefs.SetInt ("CurrentLevel", level);
		PlayerPrefs.Save ();
		Application.LoadLevel("DemoScene");
	}

	void GenerateSelectors() {
		foreach (Level level in LevelProvider.GetLevels()) {
			GenerateSelector (level);
		}
	}

	void GenerateSelector(Level level) {
		GameObject newSelector = Instantiate (baseSelector);
		newSelector.transform.position = GetPosition (level.level);
		newSelector.transform.Rotate (new Vector3 (0f, GetRotation (level.level), 0f));
		Text text = newSelector.GetComponentInChildren<Text> ();
		text.text = level.name;
		Button button = newSelector.GetComponentInChildren<Button> ();
		if (!level.locked) {
			button.onClick.AddListener (() => {
				HandleLevelSelection (level.level);
			});
		} else {
			button.interactable = false;
		}
	}

	Vector3 GetPosition(int level) {
		float adjacent = Mathf.Cos (((float)level) * theta) * radius;
		float opposite = Mathf.Sin (((float)level) * theta) * radius;
		return new Vector3 (opposite, 2, adjacent);
	}

	float GetRotation(int level) {
		return ((float)level) * theta * 360f / (Mathf.PI * 2f);
	}
}
