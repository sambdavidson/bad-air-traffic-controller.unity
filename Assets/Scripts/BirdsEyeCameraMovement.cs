using UnityEngine;
using System.Collections;

public class BirdsEyeCameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Don't do anything if the camera is not active.
		if (!this.GetComponent<Camera>().enabled)
						return;
		//Arrow Keys
		if ( (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) && !(Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A))) {
			this.transform.position = new Vector3 (this.transform.position.x + 1.0f, this.transform.position.y, this.transform.position.z);
		} else if( (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) && !(Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D))){
			this.transform.position = new Vector3 (this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z);
		}

		if ((Input.GetKey (KeyCode.UpArrow) || Input.GetKey ("w")) && !(Input.GetKey (KeyCode.DownArrow) || Input.GetKey ("s"))) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + 1.0f);
		} else if((Input.GetKey (KeyCode.DownArrow) || Input.GetKey ("s"))&& !(Input.GetKey (KeyCode.UpArrow) || Input.GetKey ("w"))) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 1.0f);
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
