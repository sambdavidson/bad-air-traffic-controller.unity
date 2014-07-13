using UnityEngine;
using System.Collections;

public class TowerCameraMovement : MonoBehaviour {

	private float horizontalRotateSpeed = 40.0f;
	private float verticalRotateSpeed = 30.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!this.GetComponent<Camera>().enabled)
			return;
		//Arrow Keys
		if (Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow)) {
			this.transform.Rotate(Vector3.up * Time.deltaTime * horizontalRotateSpeed, Space.World);
		} else if(Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow)) {
			this.transform.Rotate(Vector3.down * Time.deltaTime * horizontalRotateSpeed, Space.World);
		}
		
		if (Input.GetKey (KeyCode.UpArrow) && !Input.GetKey (KeyCode.DownArrow)) {
			this.transform.Rotate(Vector3.left * Time.deltaTime * horizontalRotateSpeed, Space.World);
			print (this.transform.rotation.x + " | " + this.transform.rotation.y + " | " + this.transform.rotation.z);
		} else if(Input.GetKey (KeyCode.DownArrow) && !Input.GetKey (KeyCode.UpArrow)) {
			this.transform.Rotate(Vector3.right * Time.deltaTime * horizontalRotateSpeed, Space.World);
		}
		
		//Mouse Wheel Zoom
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - 2.0f, this.transform.position.z);
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 2.0f, this.transform.position.z);
		}

	
	}
}
