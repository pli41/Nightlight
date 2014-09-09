using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public string nextLevel;

	void Awake() {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (nextLevel == null)
			nextLevel = "Scene";

		Application.LoadLevel (nextLevel);
	}
}