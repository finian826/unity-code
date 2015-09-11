using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Debug.Log ("Level load requested for: " + name);
		Application.LoadLevel(name);
	}

	public void QuitRequest() {
		Debug.Log ("Quit requested");
		Application.Quit();
	}
	
	public void LoadNextLevel() {
	
		Application.LoadLevel(Application.loadedLevel + 1);
	}
		
	public void BrickDestroyed(){
		if (Bricks.breakableCount <= 0){
			LoadNextLevel();
		}
	}
}
