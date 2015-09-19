using UnityEngine;
using System.Collections;

public class Bricks : MonoBehaviour {

	public static int breakableCount = 0;
	public AudioClip crack;
	public GameObject smoke;
	public Sprite[] hitSprites;
	public int blockHitPts = 15;
	public int blockDestPts = 25;
	
	private bool isBreakable;
	private int timesHit;
	private LevelManager levelManager;
	private LevelGenerator levelGenerator;
	private TextController textControl;
	
	// Use this for initialization
	void Start () {
		CountBreakable();
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		levelGenerator = GameObject.FindObjectOfType<LevelGenerator>();
		textControl = GameObject.FindObjectOfType<TextController>();
		
	}
	
	public void CountBreakable() {
		isBreakable = (this.tag == "Breakable");
		// Keep track of breakable bricks
		if (isBreakable) {
			breakableCount++;
		}
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
			//increse score
			LevelManager.GameScore = LevelManager.GameScore + blockDestPts;
			textControl.PrintScore();
			//levelManager.BrickDestroyed();
			levelGenerator.BrickDestroyed();
			PuffSmoke();
			Destroy(gameObject);
		} else {
			//increase score
			LevelManager.GameScore = LevelManager.GameScore + blockHitPts;
			textControl.PrintScore();
			LoadSprites();
		}
	}
	void PuffSmoke(){
		GameObject smokepuff =Instantiate(smoke,gameObject.transform.position, Quaternion.identity) as GameObject;
		smokepuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
		
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
