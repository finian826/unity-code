using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	public GameObject OneHitBrick, TwoHitBrick, ThreeHitBrick, SpecialBrick, Invincible;
	int[, ,] BlockLevels;
	public Sprite[] Background;
	
	private bool DataLoaded = false;
	private int LevelCounter;
	//private int TotalLevels = 1;
	
	// Use this for initialization
	void Start () {
		if (!DataLoaded) {
			LevelData();
		}
		BuildLevel(3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void BuildLevel(int RequestedLevel){
		//Place blocks on the playing field
		int lvlCount, rowCount, colCount;
		float RowStart, ColStart, RowOffset, ColOffset;
		
		lvlCount = RequestedLevel -1;
		rowCount = 0;
		colCount = 0;
		
		RowStart = 10.88f;
		RowOffset = 0.32f;
		ColStart = 0.5f;
		ColOffset = 0.5f;
		
		//Row Loop
		do {
			// Col Loop
			colCount = 0;
			ColStart = 0.5f;
			do {
				if (BlockLevels[lvlCount, rowCount, colCount] == 1) {
					Instantiate(OneHitBrick, new Vector3 (ColStart, RowStart, 0), Quaternion.identity);
				} else if (BlockLevels[lvlCount, rowCount, colCount] == 2){
					Instantiate(TwoHitBrick, new Vector3 (ColStart, RowStart, 0), Quaternion.identity);
				} else if (BlockLevels[lvlCount, rowCount, colCount] == 3) {
					Instantiate(ThreeHitBrick, new Vector3 (ColStart, RowStart, 0), Quaternion.identity);
				} else if (BlockLevels[lvlCount, rowCount, colCount] == 4) {
					Instantiate(SpecialBrick, new Vector3 (ColStart, RowStart, 0), Quaternion.identity);
				} else if (BlockLevels[lvlCount, rowCount, colCount] == 5) {
					Instantiate(Invincible, new Vector3 (ColStart, RowStart, 0), Quaternion.identity);
				} else {
					// process no block
				}
			colCount++;
			ColStart = ColStart + ColOffset;
			} while (colCount <=31);
			rowCount++;
			RowStart = RowStart - RowOffset;
		} while (rowCount <= 15);
		
		
	}
	
	void LevelData(){
		// store level data to BlockLevels
		DataLoaded = true;
		BlockLevels = new int[6,16,32];
		int lvlCount, rowCount, colCount;
		
		string seed = System.DateTime.Now.ToString();
		//Debug.Log(seed);
		System.Random pseudoRandom = new System.Random(seed.GetHashCode());
		//Debug.Log("Seed = " + seed.GetHashCode);
		
		lvlCount = 0;
		// Rows is from 0 to 15
		rowCount = 0;
		// Cols are from 0 to 30
		colCount = 0;
		
		// Level 2
		// Setup Grid rows
		lvlCount = 1;
		do {
			//process Cols
			colCount = 0;
			//Debug.Log("Row = " + rowCount);
			//Debug.Log("Col = " + colCount);
			do {
				// Setup Bricks in the rows.
				if (rowCount == 0 || rowCount == 5 || rowCount == 7 || rowCount >=10) {
					BlockLevels[lvlCount, rowCount, colCount] = 100;
				} else if (rowCount == 1 || rowCount == 4 || rowCount == 8) {
					BlockLevels[lvlCount, rowCount, colCount] = 2;
				}else if (rowCount == 2 || rowCount == 3 || rowCount == 9) {
					BlockLevels[lvlCount, rowCount, colCount] = 1;
				}else if (rowCount == 6) {
					BlockLevels[lvlCount, rowCount, colCount] = 3;
				}
				colCount = colCount + 2;
				
			} while (colCount <=31);
		rowCount++;
			
		} while (rowCount <= 15);
		
		//level 1
		//setup grid row
		lvlCount=0;
		rowCount=0;
		colCount=0;
		
		do {
			//process cols
			colCount=0;
			do {
				if (rowCount > 5 && rowCount < 12) {
					BlockLevels[lvlCount, rowCount, colCount] = 1;
				} else if (rowCount > 1 && rowCount < 6) {
					BlockLevels[lvlCount, rowCount, colCount] = 2;
				} else if (rowCount < 2) {
					BlockLevels[lvlCount, rowCount, colCount] = 3;
				} else {
					BlockLevels[lvlCount, rowCount, colCount] = 100;
				}
				colCount = colCount + 2;
			} while (colCount<=31);
			rowCount++;
		} while (rowCount <=15);
		
		//level 3
		//setup
		lvlCount = 2;
		rowCount=0;
		colCount=0;
		
		do {
			BlockLevels[lvlCount, rowCount, colCount] = 100;
			BlockLevels[lvlCount, rowCount +1, colCount] = 100;
			colCount = colCount +2;
		} while (colCount <= 31);
		rowCount = rowCount + 2;
		do {
			colCount = 0;
			do {
				BlockLevels[lvlCount, rowCount, colCount] = pseudoRandom.Next(1,4);
				colCount = colCount +2;
			} while (colCount <= 31);
			rowCount = rowCount + 2;
		} while (rowCount <=15);
			
		
	}
	
	public void BrickDestroyed(){
		if (Bricks.breakableCount <= 0){
			// reset counters and load next level
		}
	}
	
}
