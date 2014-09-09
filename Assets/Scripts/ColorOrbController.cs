using UnityEngine;
using System.Collections;

public class ColorOrbController : MonoBehaviour {

	private Color color;
	private PlayerController playerController;

	void Awake() {
		GameObject player = GameObject.Find ("Player");
		playerController = player.GetComponent<PlayerController> ();

		if (transform.tag == "Red")
			color = new Color (1, 0, 0);
		else if (transform.tag == "Blue")
			color = new Color (0, 0, 1);
		else if (transform.tag == "Yellow")
			color = new Color (1, 1, 0);
		transform.renderer.material.color = color;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && playerController != null) {
			if (playerController.color != null) {
				GameObject[] blocksToHide = GameObject.FindGameObjectsWithTag(playerController.color);
				for(int i=0; i < blocksToHide.Length; i++) {
					if(blocksToHide[i].layer == LayerMask.NameToLayer("Ground")) {
						blocksToHide[i].renderer.enabled = false;
						blocksToHide[i].collider2D.enabled = false;
					}
				}
			}
			GameObject[] blocksToShow = GameObject.FindGameObjectsWithTag(transform.tag);
			for(int i=0; i < blocksToShow.Length; i++) {
				if(blocksToShow[i].layer == LayerMask.NameToLayer("Ground")) {
					blocksToShow[i].renderer.enabled = true;
					blocksToShow[i].collider2D.enabled = true;
					blocksToShow[i].renderer.material.color = color;
				}
			}
			playerController.color = transform.tag;
			Destroy(transform.gameObject);
		}
	}
}
