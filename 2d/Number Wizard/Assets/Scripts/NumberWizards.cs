using UnityEngine;
using System.Collections;

public class NumberWizards : MonoBehaviour {

	// Use this for initialization
	int max;
	int min;
	int guess;
	
	void Start () {
		StartGame ();		
	}
	
	void StartGame() {
		max = 1000;
		min = 1;
		guess = 500;
		
		print ("========================");
		print ("Welcome to Number Wizard");
		print ("Pick a number in your head and don't tell anyone.");
		
		print ("The highest number you can pick is " + max);
		print ("The lowest number you can pick is " +min);
		
		print ("Is the number higher or lower then " + guess +"?");
		print ("Up Arrow for higher, Down Aroow for lower, Return for equal.");
		
		max = max + 1;
	}
	
	void NextGuess() {
		guess = (max + min) /2;
		print (" Higher or lower then " +guess + "?");
		print ("Up Arrow for higher, Down Aroow for lower, Return for equal.");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			// print ("Up arrow was pressed");
			min = guess;
			NextGuess ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			//print ("Down arrow was pressed");
			max = guess;
			NextGuess();
		}else if (Input.GetKeyDown (KeyCode.Return)) {
			print ("I won!");
			StartGame();
		}
		
	}
}
