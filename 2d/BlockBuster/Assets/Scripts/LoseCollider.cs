using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	private LevelManager levelManager;
	private Ball ball;	
	private TextController textControl;
	
	void OnTriggerEnter2D (Collider2D trigger) {
		//print("Trigger");
		// decrese lives until 0 then end
		levelManager=GameObject.FindObjectOfType<LevelManager>();
		textControl=GameObject.FindObjectOfType<TextController>();
		ball = GameObject.FindObjectOfType<Ball>();
		LevelManager.GameLives--;
		//Debug.Log (LevelManager.GameLives);
		if (LevelManager.GameLives <=0) {
			levelManager.LoadLevel("Lost Game");
		} else {
			ball.BallReset();
			textControl.PrintLivesLeft();
		}
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		//print("Collision");
	}
}
