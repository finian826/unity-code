using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public static int GameScore;
	public static int GameLives = 3;
	public TextController textControl;

	public void LoadLevel(string name){
		//Debug.Log ("Level load requested for: " + name);
		Application.LoadLevel(name);
		textControl = GameObject.FindObjectOfType<TextController>();
		textControl.PrintLivesLeft();
		textControl.PrintScore();
		Bricks.breakableCount=0;
	}

	public void QuitRequest() {
		//Debug.Log ("Quit requested");
		Application.Quit();
	}
	
	public void LoadNextLevel() {
		Bricks.breakableCount=0;
		Application.LoadLevel(Application.loadedLevel + 1);
	}
		
	public void BrickDestroyed(){
		if (Bricks.breakableCount <= 0){
			LoadNextLevel();
		}
	}
}
