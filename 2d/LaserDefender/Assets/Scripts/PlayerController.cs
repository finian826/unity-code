using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 15.0f;
	public float padding = 1.0f;
	
	float xmin;
	float xmax;
	float ymin;
	float ymax;
	
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 righttmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		//Vector3 top = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		//Vector3 bottom = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distance));
		xmin=leftmost.x + padding;
		xmax=righttmost.x - padding;
		//ymin=bottom.y;
		//ymax=top.y;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.UpArrow)) {
			// do something
			//transform.position += new Vector3(0, speed * Time.deltaTime,0);
			transform.position += Vector3.up * speed * Time.deltaTime;
			Debug.Log("Up");
		} else if(Input.GetKey(KeyCode.DownArrow)) {
			//do something
			//transform.position += new Vector3(0, -speed * Time.deltaTime,0);
			transform.position += Vector3.down * speed * Time.deltaTime;
			Debug.Log ("down");
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			//do something
			//transform.position += new Vector3(speed * Time.deltaTime, 0,0);
			transform.position += Vector3.right * speed * Time.deltaTime;
			Debug.Log ("Right");
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			//do something
			//transform.position += new Vector3(-speed * Time.deltaTime, 0,0);
			transform.position += Vector3.left * speed * Time.deltaTime;
			Debug.Log ("Left");
		}
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
		
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
