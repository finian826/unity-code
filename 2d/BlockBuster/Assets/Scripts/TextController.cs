using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {
	public Text lives;
	public Text score;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PrintLivesLeft() {
		lives.text = "Lives: " + LevelManager.GameLives.ToString();
	}
	
	public void PrintScore() {
		score.text = "Score: " + LevelManager.GameScore.ToString();
	}
}
