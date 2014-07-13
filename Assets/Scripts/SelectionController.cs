using UnityEngine;
using System.Collections;

public class SelectionController : MonoBehaviour {
	
	public Camera primaryCamera;
	public Camera secondaryCamera;

	public Camera currentCamera;

	private AirplaneAI airplaneAI;
	private AirplaneAI prevAirplaneAI;


	// Use this for initialization
	void Start () {
		currentCamera = primaryCamera;
	}
	//Editor Icon
	void OnDrawGizmos() {
		Gizmos.DrawIcon(transform.position, "controller.png", true);
	}
	
	// Update is called once per frame
	void Update () {

		//Mouse Click detection
		Ray ray = currentCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) 
		{
			Debug.DrawLine (ray.origin, hit.point, Color.cyan);
		}
		if (Input.GetMouseButtonDown (0)) 
		{

			GameObject clicked = hit.collider.gameObject;
			if(airplaneAI != null) {
				airplaneAI.isSelected = !airplaneAI.isSelected;
				prevAirplaneAI = airplaneAI;
				airplaneAI = null;
			}
			airplaneAI = clicked.GetComponent<AirplaneAI>();
			if( airplaneAI != null ) {
				airplaneAI.isSelected = !airplaneAI.isSelected;
				if(airPanCurrent == airPanMin)
					ToggleInfoPanel();
			} else {
				if(airPanCurrent == airPanMax)
					ToggleInfoPanel();
			}

		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			ToggleInfoPanel();
		}

		//Camera Changing
		if (Input.GetKey (KeyCode.F1)) {
			primaryCamera.enabled = true;
			secondaryCamera.enabled = false;
			currentCamera = primaryCamera;
		}
		if (Input.GetKey (KeyCode.F2)) {
			primaryCamera.enabled = false;
			secondaryCamera.enabled = true;
			currentCamera = secondaryCamera;
		}
	}

	//Gui Animation Variables
	private int airPanMin = 20;
	private int airPanMax = 150;
	private float airPanCurrent = 20;
	private float airPanAnimSpeed = 1000.0f;
	private byte airPanAnimState = 0; // 0 - Done; 1 - Opening; 2 - Closing;

	public GUISkin BATCSkin;

	void OnGUI () {

		GUI.skin = BATCSkin;
	
		GUI.color = Color.black;
		GUI.Label (new Rect (1, 1, 200, 200), currentCamera.name);
		GUI.color = Color.white;
		GUI.Label (new Rect (0, 0, 200, 200), currentCamera.name);

		GUI.Box (new Rect (0, Screen.height - airPanCurrent, Screen.width, 220), GUIContent.none);

		//Airplane Info Vars
		string selectedAirplaneName = "None Selected";
		Texture2D selectedAirplaneSprite = null;
		float selectedAirplaneLength = 000.0f;
		int selectedAirplanePassengers = 0;
		float selectedAirplaneFuel = 0;
		if (airplaneAI != null) {
			selectedAirplaneName = airplaneAI.airplaneTitle;
			selectedAirplaneSprite = airplaneAI.airplaneImage.texture;
			selectedAirplaneLength = airplaneAI.length;
			selectedAirplanePassengers = airplaneAI.GetPassengers();
			selectedAirplaneFuel = airplaneAI.GetFuel();
		}
		// Airplane Name

		GUI.Label (new Rect (5, Screen.height - airPanCurrent, Screen.width, 220), "Airpane: " + selectedAirplaneName);

		// Airplane Sprite
		GUI.Box (new Rect (10, Screen.height - airPanCurrent + 30, 240, 110), GUIContent.none );
		GUI.Label (new Rect (15, Screen.height - airPanCurrent + 30, 240, 30), "Profile:");
		GUI.DrawTexture (new Rect (25, Screen.height - airPanCurrent + 35, 200, 100), selectedAirplaneSprite);
		GUI.Label (new Rect (95, Screen.height - airPanCurrent + 120, 240, 30), selectedAirplaneLength + " Meters");

		// Airplane Passengers
		GUI.Label (new Rect (300, Screen.height - airPanCurrent + 30, 240, 30), "Passengers: " + selectedAirplanePassengers);

		// Airplane Fuel
		GUI.Label (new Rect (300, Screen.height - airPanCurrent + 60, 240, 30), "Fuel: " + selectedAirplaneFuel.ToString("F2") + " Gal");

		// Spacebar Text
		string spacebarLabel = "Spacebar: ▼";
		if (airPanCurrent < airPanMax)
			spacebarLabel = "Spacebar: ▲";

		GUI.Label (new Rect ((Screen.width) - 90, Screen.height - airPanCurrent, 90, 23), spacebarLabel);

		if (airPanAnimState != 0) {
			if(airPanAnimState == 1) { //Opening
				if(airPanCurrent <= airPanMax - 1) {
					airPanCurrent += Time.deltaTime * airPanAnimSpeed;
				} else {
					airPanCurrent = airPanMax;
					airPanAnimState = 0;
				}
			} else if(airPanAnimState == 2) { //Closing
				if(airPanCurrent >= airPanMin + 1) {
					airPanCurrent -= Time.deltaTime * airPanAnimSpeed;
				} else {
					airPanCurrent = airPanMin;
					airPanAnimState = 0;
				}
			}
		}
		
	}
	 
	private void ToggleInfoPanel() 
	{
		if(airPanAnimState == 0) {
			if(airPanCurrent == airPanMin) {
				airPanAnimState = 1;
			}
			if(airPanCurrent == airPanMax) {
				airPanAnimState = 2;
			}
		}

	}

}
