using UnityEngine;
using System.Collections;

public class PanelRenderer : MonoBehaviour {

	public GameObject airplaneSpritePrefab;
	private GameObject airplaneSprite;

	private GameObject parentAirplane;
	public bool isEnabled = false;

	private string airplaneTitle;
	private int numPassengers;
	private int currentWeight;
	private int currentFuel;
	private int costPerUnit;
	private Sprite airplaneImage;
	// Use this for initialization
	void Start () {
		//Detail Panel

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitializePanel(GameObject parent, string title, int passengers, int weight, int fuel, int cost, Sprite image) {

		airplaneSprite = (GameObject) Instantiate (airplaneSpritePrefab);
		airplaneSprite.transform.position = new Vector3 (parentAirplane.transform.position.x,
		                                                 parentAirplane.transform.position.y + 5.0f,
		                                                 parentAirplane.transform.position.z);
		airplaneSprite.GetComponent<SpriteRenderer> ().sprite = airplaneImage;
		print ("Success");
		parentAirplane = parent;
 		airplaneTitle = title;
		numPassengers = passengers;
		currentWeight = weight;
		currentFuel = fuel;
		costPerUnit = cost;
		airplaneImage = image;

	}

	public void Enable() {
		airplaneSprite.GetComponent<SpriteRenderer> ().enabled = true;
		isEnabled = true;
	}
	public void Disable() {
		airplaneSprite.GetComponent<SpriteRenderer> ().enabled = false;
		isEnabled = false;
		
	}
}
