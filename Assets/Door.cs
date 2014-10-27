using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public string nextLevel;
	
	void Awake() {
		
	}

	void Update(){
		if(GameObject.FindGameObjectWithTag("Player") == null){
			Application.LoadLevel (nextLevel);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Player"){
			Destroy(other.gameObject);
		}

	}
}
