using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	public GameObject OneHitBrick, TwoHitBrick, ThreeHitBrick, SpecialBrick, Invincible, PlayFieldBG;
	int[, ,] BlockLevels;
	public Sprite[] Background;
	
	private bool DataLoaded = false;
	private int LevelCounter = 6;
	private Bricks bricks;
	private Ball ball;
	
	
	
	//private int TotalLevels = 1;
	
	// Use this for initialization
	void Start () {
		if (!DataLoaded) {
			LevelData();
		}
		bricks = GameObject.FindObjectOfType<Bricks>();
		ball = GameObject.FindObjectOfType<Ball>();
		
		NewLevel();
		}
	
	// Update is called once per frame
	void Update () {
	
	}
	void NewLevel() {
		string seed = System.DateTime.Now.ToString();
		//Debug.Log(seed);
		System.Random pseudoRandom = new System.Random(seed.GetHashCode());
		Bricks.breakableCount = 0;
		ball.BallReset();
		
		BuildLevel(pseudoRandom.Next(1,LevelCounter + 1));
		//BuildLevel(6);
		//bricks.CountBreakable();
		PlayFieldBG = GameObject.Find("PlaySpace").transform.Find ("Background").gameObject;
		PlayFieldBG.GetComponent<SpriteRenderer>().sprite = Background[pseudoRandom.Next(1,Background.Length)];
		
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
		BlockLevels = new int[LevelCounter,16,32];
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
		
		// Level 2
		// Setup Grid rows
		lvlCount = 1;
		rowCount = 0;
		colCount = 0;
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
		
		// Custom lvl 4
		// setup
		lvlCount = 3;
		rowCount = 0;
		colCount = 0;
		
		// place special bricks
		BlockLevels[lvlCount,1,14] = 4;
		BlockLevels[lvlCount,1,16] = 4;
		BlockLevels[lvlCount,2,0] = 4;
		BlockLevels[lvlCount,2,30] = 4;
		BlockLevels[lvlCount,5,0] = 4;
		BlockLevels[lvlCount,5,30] = 4;
		BlockLevels[lvlCount,13,14] = 4;
		BlockLevels[lvlCount,13,16] = 4;
		
		//place invinciables
		rowCount = 3;
		colCount = 0;
		do {
			BlockLevels[lvlCount,rowCount, colCount] = 5;
			colCount = colCount + 2;
		} while (colCount <= 9);
		do {
			BlockLevels[lvlCount, rowCount, colCount] = 1;
			colCount = colCount + 2;
		} while (colCount <=21);
		//colCount = 20;
		do {
			BlockLevels[lvlCount, rowCount, colCount] = 5;
			colCount = colCount + 2;
		} while (colCount <=31);
		rowCount = 0;
		do {
			BlockLevels[lvlCount, rowCount, 12] = 1;
			BlockLevels[lvlCount, rowCount, 18] = 1;
			if (rowCount == 3) {
				BlockLevels[lvlCount, rowCount, 14] = 1;
				BlockLevels[lvlCount, rowCount, 16] = 1;
			}
			rowCount++;
		} while (rowCount <=3);
		rowCount = 4;
		colCount = 0;
		do {
			BlockLevels[lvlCount, rowCount, 12] = 2;
			BlockLevels[lvlCount, rowCount, 14] = 1;
			BlockLevels[lvlCount, rowCount, 16] = 1;
			BlockLevels[lvlCount, rowCount, 18] = 2;
			if (rowCount == 6) {
				BlockLevels[lvlCount, rowCount, 0] = 2;
				BlockLevels[lvlCount, rowCount, 2] = 2;
				BlockLevels[lvlCount, rowCount, 4] = 2;
				BlockLevels[lvlCount, rowCount, 26] = 2;
				BlockLevels[lvlCount, rowCount, 28] = 2;
				BlockLevels[lvlCount, rowCount, 30] = 2;
			}
			rowCount++;
		} while (rowCount <=6);
		
		rowCount = 7;
		colCount = 0;
		do {
			if (colCount <= 6 || colCount >=24) {
				BlockLevels[lvlCount, rowCount, colCount] = 1;
			} else {
				BlockLevels[lvlCount, rowCount, colCount] = 5;
			}
			colCount = colCount+2;
		} while (colCount <= 31);
		
		BlockLevels[lvlCount, 8, 4] = 1;
		BlockLevels[lvlCount, 8, 26] = 1;
		BlockLevels[lvlCount, 9, 4] = 1;
		BlockLevels[lvlCount, 9, 26] = 1;
		BlockLevels[lvlCount, 10, 4] = 1;
		BlockLevels[lvlCount, 10, 26] = 1;
		
		rowCount = 11;
		colCount = 0;
		
		do {
			BlockLevels[lvlCount, rowCount, 4] = 5;
			BlockLevels[lvlCount, rowCount, 26] = 5;
			BlockLevels[lvlCount, rowCount, 12] = 5;
			BlockLevels[lvlCount, rowCount, 18] = 5;
			if (rowCount == 11) {
				BlockLevels[lvlCount, rowCount, 14] = 2;
				BlockLevels[lvlCount, rowCount, 16] = 2;
			} else if (rowCount == 14){
				BlockLevels[lvlCount, rowCount, 14] = 1;
				BlockLevels[lvlCount, rowCount, 16] = 1;
			} else if (rowCount == 15) {
				BlockLevels[lvlCount, rowCount, 0] = 5;
				BlockLevels[lvlCount, rowCount, 2] = 5;
				BlockLevels[lvlCount, rowCount, 28] = 5;
				BlockLevels[lvlCount, rowCount, 30] = 5;
				BlockLevels[lvlCount, rowCount, 14] = 5;
				BlockLevels[lvlCount, rowCount, 16] = 5;
			}
			
			rowCount++;
		} while (rowCount <=15);
		rowCount = 10;
		do {
			BlockLevels[lvlCount, rowCount, 0] = 2;
			BlockLevels[lvlCount, rowCount, 2] = 1;
			BlockLevels[lvlCount, rowCount, 28] = 1;
			BlockLevels[lvlCount, rowCount, 30] = 2;
			rowCount = rowCount + 2;
		} while (rowCount <=14);
		
		BlockLevels[lvlCount, 11, 0] = 1;
		BlockLevels[lvlCount, 11, 2] = 2;
		BlockLevels[lvlCount, 11, 28] = 2;
		BlockLevels[lvlCount, 11, 30] = 1;	
		BlockLevels[lvlCount, 13, 0] = 1;
		BlockLevels[lvlCount, 13, 2] = 2;
		BlockLevels[lvlCount, 13, 28] = 2;
		BlockLevels[lvlCount, 13, 30] = 1;
		
		// Level 5
		lvlCount = 4; // or what ever lvl
		rowCount = 0;
		colCount = 0;
			////row loop
		do {
			//col count
			colCount = 0;
			do {
				BlockLevels[lvlCount, rowCount, colCount] = 3;
				colCount = colCount + 2;
			} while (colCount <=31);
			rowCount++;
		} while (rowCount <=4);
		rowCount ++;
		do {
			//col count
			colCount = 0;
			do {
				BlockLevels[lvlCount, rowCount, colCount] = 2;
				colCount = colCount + 2;
			} while (colCount <=31);
			rowCount++;
		} while (rowCount <=10);
		rowCount++;
		do {
			//col count
			colCount = 0;
			do {
				BlockLevels[lvlCount, rowCount, colCount] = 1;
				colCount = colCount + 2;
			} while (colCount <=31);
			rowCount++;
		} while (rowCount <=15);
		
		
		
		// Level 6
		lvlCount = 5; // or what ever lvl
		rowCount = 0;
		colCount = 0;
		
		//row loop
		do {
			//col count
			colCount = 0;
			// place one hits
			BlockLevels[lvlCount, rowCount, 0 + rowCount * 2] = 1;
			BlockLevels[lvlCount, rowCount, 30 - rowCount * 2] = 1;
			if (rowCount <= 6) {
				BlockLevels[lvlCount, rowCount, 2 + rowCount * 2] = 2;
				BlockLevels[lvlCount, rowCount, 28 - rowCount * 2] = 2;
			}
			if (rowCount <= 5) {
				BlockLevels[lvlCount, rowCount, 4 + rowCount * 2] = 3;
				BlockLevels[lvlCount, rowCount, 26 - rowCount * 2] = 3;
			} 
			if (rowCount >= 10) {
				BlockLevels[lvlCount, rowCount, 12 - (rowCount - 10) *2] = 2;
				BlockLevels[lvlCount, rowCount, 18 + (rowCount - 10) *2] = 2;
				BlockLevels[lvlCount, rowCount, 14 - (rowCount - 10) *2] = 3;
				BlockLevels[lvlCount, rowCount, 16 + (rowCount - 10) *2] = 3;
			}
			do {
				if (rowCount == 6 || rowCount == 10) {
					if (colCount <= 8 || colCount >= 22) {
						BlockLevels[lvlCount, rowCount, colCount] = 3;
					} 
				} else if (rowCount == 7 || rowCount == 9) {
					if (colCount <=8 || colCount >=22) {
						BlockLevels[lvlCount, rowCount, colCount] = 2;
					}
				} else if (rowCount == 8) {
					if (colCount <=8 || colCount >=22) {
						BlockLevels[lvlCount, rowCount, colCount] = 5;
					}
				}
				colCount = colCount +2;
			} while (colCount <= 31);
			
			rowCount++;
		} while (rowCount <=15);
		rowCount = 0;
		colCount = 0;
		do {
			colCount = 0;
			do {
				if (rowCount == 0 || rowCount == 15) {
					if (colCount == 6 || colCount == 24) {
						BlockLevels[lvlCount, rowCount, colCount] = 1;
					} else if (colCount >=8 && colCount <= 22){
						BlockLevels[lvlCount, rowCount, colCount] = 5;
					}
				} else if (rowCount == 1 || rowCount == 14) {
					if ((colCount >=8 && colCount <= 12) || (colCount >=18 && colCount <= 22)) {
						BlockLevels[lvlCount, rowCount, colCount] = 1;
					} else if (colCount >= 14 && colCount <=16) {
						BlockLevels[lvlCount, rowCount, colCount] = 5;
					}
				} else if (rowCount == 2 || rowCount == 13) {
					if (colCount == 12 || colCount == 18) {
						BlockLevels[lvlCount, rowCount, colCount] = 1;
					}else if (colCount >=14 && colCount <=16) {
						BlockLevels[lvlCount, rowCount, colCount] = 5;
					}
					BlockLevels[lvlCount, rowCount, 10] = 2;
					BlockLevels[lvlCount, rowCount, 20] = 2;
				} else if (rowCount == 3 || rowCount == 12) {
					if (colCount >=12 && colCount <= 18) {
						BlockLevels[lvlCount, rowCount, colCount] = 1;
					}
				} else if (rowCount == 4 || rowCount == 11) {
					BlockLevels[lvlCount, rowCount, 14] = 2;
					BlockLevels[lvlCount, rowCount, 16] = 2;
				} else if (rowCount >= 5 || rowCount <= 10) {
					BlockLevels[lvlCount, rowCount, 10] = 1;
					BlockLevels[lvlCount, rowCount, 20] = 1;
				}
				colCount = colCount +2;
			} while (colCount <=31);
			rowCount++;
		} while (rowCount <= 15);
		BlockLevels[lvlCount, 9, 14] = 2;
		BlockLevels[lvlCount, 9, 16] = 2;
		BlockLevels[lvlCount, 7, 12] = 1;
		BlockLevels[lvlCount, 7, 18] = 1;
		BlockLevels[lvlCount, 8, 12] = 1;
		BlockLevels[lvlCount, 8, 18] = 1;
		
		// Level --
		//lvlCount = 3; // or what ever lvl
		//rowCount = 0;
		//colCount = 0;
		
		////row loop
		//do {
		//	//col count
		//	colCount = 0;
		//	do {
		//	
		//		colCount = colCount + 2;
		//	} while (colCount <=31);
		//	rowCount++;
		//} while (rowCount <=15);
	}
	
	public void BrickDestroyed(){
		if (Bricks.breakableCount <= 0){
			// reset counters and load next level
			Bricks.breakableCount = 0;
			
			var clones = GameObject.FindGameObjectsWithTag("Clone");
			foreach (var clone in clones){
				Destroy(clone);
			}
			NewLevel();
			
		}
	}
	
}
