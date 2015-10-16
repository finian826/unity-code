using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LevelManager : MonoBehaviour {


	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
	
	public void PanelOn(GameObject OnPanel){
		OnPanel.SetActive(true);
		
	}
	
	public void PanleOff(GameObject OffPanel) {
		OffPanel.SetActive(false);
	}
}
