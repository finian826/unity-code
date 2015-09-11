using UnityEngine;
using System.Collections;

public class Bricks : MonoBehaviour {

	public static int breakableCount = 0;
	public AudioClip crack;
	public GameObject smoke;
	public Sprite[] hitSprites;
	
	private bool isBreakable;
	private int timesHit;
	private LevelManager levelManager;
	
	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		// Keep track of breakable bricks
		if (isBreakable) {
			breakableCount++;
		}
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D (Collision2D collision) {
		AudioSource.PlayClipAtPoint (crack, transform.position, 0.5f);
		if(isBreakable){
			HandleHits();
		}
	}
	
	void HandleHits() {
		timesHit ++;
		int maxHits = hitSprites.Length + 1;
		//SimulateWin();
		if (timesHit >= maxHits) {
			breakableCount--;
			//Debug.Log(breakableCount);
			levelManager.BrickDestroyed();
			
			GameObject smokepuff =Instantiate(smoke,gameObject.transform.position, Quaternion.identity) as GameObject;
			smokepuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
			Destroy(gameObject);
		} else {
			LoadSprites();
		}
	}
	
	void LoadSprites(){
		int spriteIndex = timesHit -1;
		if (hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite =hitSprites[spriteIndex];
		} else {
			Debug.LogError("Missing Sprite!");
		}
	}
	// TODO remove this method once we can actually win
	
	void SimulateWin() {
		levelManager.LoadNextLevel();
	}
	
		
}
