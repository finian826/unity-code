using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberWizard : MonoBehaviour {

	// Use this for initialization
	int max;
	int min;
	int guess;
	
	public Text text;	
	public int maxGuessesAllowed = 5;
	
	void Start () {
		StartGame ();		
	}
	
	void StartGame() {
		max = 1000;
		min = 1;

		NextGuess ();
	}
	
	void NextGuess() {
		//guess = (max + min) /2;
		guess = Random.Range(min, max+1);
		text.text = guess.ToString();
		maxGuessesAllowed = maxGuessesAllowed - 1;
		if(maxGuessesAllowed <=0) {
			Application.LoadLevel("Win");
		}
	}
		
	public void GuessHigher(){
		min = guess;
		NextGuess ();
	}
	
	public void GuessLower(){
		max = guess;
		NextGuess();
	}
}
