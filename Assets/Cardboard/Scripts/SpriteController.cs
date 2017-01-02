using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour {

	// Use this for initialization
	SpriteRenderer spriteRenderer;
	private float len;
	private float currentOpacity;
	void Start () {
		len = 1.0f;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.color = new Color (1f, 1f, 1f, 0f);
	}

	public void Flash() {
		currentOpacity = .4f;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentOpacity > 0) {
			currentOpacity -= currentOpacity * Time.deltaTime / len;
			spriteRenderer.color = new Color (1f, 1f, 1f, currentOpacity);
		}
	}
}
