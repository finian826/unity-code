using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Paddle paddle;
	
	private bool hasStarted = false;
	
	private Vector3 paddleToBallVector;
	
	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;
		//print (paddleToBallVector.y);
		
		BallReset();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted) {
			this.transform.position = paddle.transform.position + paddleToBallVector;
		}
		
		if (Input.GetMouseButtonDown(0) && !hasStarted) { 
			print ("Mouse left button click");
			this.rigidbody2D.velocity = new Vector2(2f,10f);
			hasStarted = true;
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		Vector2 tweak = new Vector2(Random.Range(0f,0.2f), Random.Range(0f,0.2f));
		if(hasStarted) {
			audio.Play ();
			rigidbody2D.velocity += tweak;
		}
	}
	
	public void BallReset() {
		if (hasStarted){
			hasStarted = false;
			paddle = GameObject.FindObjectOfType<Paddle>();
			this.transform.position = paddle.transform.position + paddleToBallVector;
		}
	}
}
