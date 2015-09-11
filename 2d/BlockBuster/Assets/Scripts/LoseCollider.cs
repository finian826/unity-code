using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	private LevelManager levelManager;
	
	
	void OnTriggerEnter2D (Collider2D trigger) {
		//print("Trigger");
		levelManager=GameObject.FindObjectOfType<LevelManager>();
		levelManager.LoadLevel("Lost Game");
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		//print("Collision");
	}
}
