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
		int selectedAirplaneCost = 0;
		int selectedAirplaneWeight = 0;
		if (airplaneAI != null) {
			selectedAirplaneName = airplaneAI.airplaneTitle;
			selectedAirplaneSprite = airplaneAI.airplaneImage.texture;
			selectedAirplaneLength = airplaneAI.length;
			selectedAirplanePassengers = airplaneAI.GetPassengers();
			selectedAirplaneFuel = airplaneAI.GetFuel();
			selectedAirplaneCost = airplaneAI.GetCost();
			selectedAirplaneWeight = airplaneAI.GetWeight();
		}
		// Airplane Name

		GUI.Label (new Rect (5, Screen.height - airPanCurrent, Screen.width, 220), "Airpane: " + selectedAirplaneName);

		// Airplane Sprite
		GUI.Box (new Rect (10, Screen.height - airPanCurrent + 30, 240, 110), GUIContent.none );
		GUI.Label (new Rect (15, Screen.height - airPanCurrent + 30, 240, 30), "Profile:");
		GUI.DrawTexture (new Rect (25, Screen.height - airPanCurrent + 35, 200, 100), selectedAirplaneSprite);
		GUI.Label (new Rect (95, Screen.height - airPanCurrent + 120, 240, 30), selectedAirplaneLength + " Meters");

		//Column 1
		// Airplane Fuel
		GUI.Label (new Rect (300, Screen.height - airPanCurrent + 30, 240, 30), "Fuel: " + selectedAirplaneFuel.ToString("F2") + " Gal");
		DrawColorBar (305, Screen.height - airPanCurrent + 50, (int) selectedAirplaneFuel / 5000);

		//Airplane Cost
			//Breaking into string and adding commas.
			string costToDisplay = selectedAirplaneCost.ToString ();
			int originalLength = (costToDisplay.Length - 1) / 3;
			for (int x = 1 ; x <= originalLength; x++) {
				costToDisplay = costToDisplay.Insert(costToDisplay.Length - ((x * 3)+ x - 1), "," );

			}
		GUI.Label (new Rect (300, Screen.height - airPanCurrent + 60, 240, 30), "Cost Per Unit: " + costToDisplay + " USD");
		DrawColorBar (305, Screen.height - airPanCurrent + 80, selectedAirplaneCost / 25000000);

		//Airplane Weight
		GUI.Label (new Rect (300, Screen.height - airPanCurrent + 90, 240, 30), "Weight: " + selectedAirplaneWeight + " LBS");
		DrawColorBar (305, Screen.height - airPanCurrent + 110, selectedAirplaneWeight / 40000);


		//Column 2

		// Airplane Passengers
		GUI.Label (new Rect (500, Screen.height - airPanCurrent + 30, 240, 30), "Passengers: " + selectedAirplanePassengers);
		DrawColorBar (505, Screen.height - airPanCurrent + 50, selectedAirplanePassengers / 40);

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
	public Sprite colorbarOverlay;
	public Sprite colorbarFront;
	public Sprite colorbarMid;
	public Sprite colorbarEnd;

	private Color colorSegmentOne = new Color (0.0f, 0.098f, 0.6f);
	private Color colorSegmentTwo = new Color (0.0f, 0.85f, 1.0f);
	private Color colorSegmentThree = new Color (0.0f, 0.764f, 0.607f);
	private Color colorSegmentFour = new Color (0.349f, 0.831f, 0.09f);
	private Color colorSegmentFive = new Color (0.949f, 0.99f, 0.008f);
	private Color colorSegmentSix = new Color (1.0f, 0.482f, 0.0f);
	private Color colorSegmentSeven = new Color (0.878f, 0.043f, 0.043f);

	private void DrawColorBar(float x, float y, int segments) {

		//Draw all segments; Starting with last and falling backwards.
		Color prevGUIColor = GUI.color;
		switch(segments) 
		{
		default:
			goto case 6;
		case 6:
			GUI.color = colorSegmentSeven;
			GUI.DrawTexture ( new Rect (x + 110, y + 2, colorbarEnd.texture.width, colorbarEnd.texture.height), colorbarEnd.texture);
			goto case 5;
		case 5:
			GUI.color = colorSegmentSix;
			GUI.DrawTexture ( new Rect (x + 91, y + 2, colorbarMid.texture.width, colorbarMid.texture.height), colorbarMid.texture);
			goto case 4;
		case 4:
			GUI.color = colorSegmentFive;
			GUI.DrawTexture ( new Rect (x + 72, y + 2, colorbarMid.texture.width, colorbarMid.texture.height), colorbarMid.texture);
			goto case 3;
		case 3:
			GUI.color = colorSegmentFour;
			GUI.DrawTexture ( new Rect (x + 53, y + 2, colorbarMid.texture.width, colorbarMid.texture.height), colorbarMid.texture);
			goto case 2;
		case 2:
			GUI.color = colorSegmentThree;
			GUI.DrawTexture ( new Rect (x + 34, y + 2, colorbarMid.texture.width, colorbarMid.texture.height), colorbarMid.texture);
			goto case 1;
		case 1:
			GUI.color = colorSegmentTwo;
			GUI.DrawTexture ( new Rect (x + 16, y + 2, colorbarMid.texture.width, colorbarMid.texture.height), colorbarMid.texture);
			goto case 0;
		case 0:
			GUI.color = colorSegmentOne;
			GUI.DrawTexture ( new Rect (x + 2, y + 2, colorbarFront.texture.width, colorbarFront.texture.height), colorbarFront.texture);
			break;
		}
		GUI.color = prevGUIColor;

		GUI.DrawTexture (new Rect (x, y, colorbarOverlay.texture.width, colorbarOverlay.texture.height), colorbarOverlay.texture);

	}

}
