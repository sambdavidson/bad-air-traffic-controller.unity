using UnityEngine;
using System.Collections;

public class PanelRenderer : MonoBehaviour {

	//Public Prefabs
	public SelectionController controller;

	public GameObject airplaneSpritePrefab;
	public GameObject airplaneTitlePrefab;

	//Private GameObjects
	private GameObject airplaneSprite;
	private GameObject airplaneTitle;

	private GameObject parentAirplane;

	public bool isEnabled = false;

	private string panelTitle;
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

		//Drawing in front of the active camera
		this.transform.LookAt (Camera.main.transform);
		this.transform.position = new Vector3 (Camera.main.transform.position.x + Camera.main.transform.rotation.x * 2,
		                                      Camera.main.transform.position.y + Camera.main.transform.rotation.y * 2,
		                                      Camera.main.transform.position.z + Camera.main.transform.rotation.z * 2);

	
	}

	public void InitializePanel(GameObject parent, string title, int passengers, int weight, int fuel, int cost, Sprite image) {

		//Set Local Variables
		parentAirplane = parent;
 		panelTitle = title;
		numPassengers = passengers;
		currentWeight = weight;
		currentFuel = fuel;
		costPerUnit = cost;
		airplaneImage = image;

		//Instantiate Panel Components
		airplaneSprite = (GameObject) Instantiate (airplaneSpritePrefab);
		airplaneTitle = (GameObject) Instantiate (airplaneTitlePrefab);


		// Airplane Sprite
		airplaneSprite.GetComponent<SpriteRenderer> ().sprite = airplaneImage;
		airplaneSprite.transform.parent = this.transform;
		airplaneSprite.transform.localPosition = new Vector3 (0.0f, 5.0f, 0.0f );

		// Airplane Title
		airplaneTitle.GetComponent<TextMesh> ().text = panelTitle;
		airplaneTitle.transform.parent = this.transform;
		airplaneTitle.transform.localPosition = new Vector3 (0.0f, 8.0f, 0.0f );

		this.Disable();


	}

	public void Enable() {
		airplaneSprite.GetComponent<SpriteRenderer> ().enabled = true;
		airplaneTitle.GetComponent<MeshRenderer> ().enabled = true;
		isEnabled = true;
	}
	public void Disable() {
		airplaneSprite.GetComponent<SpriteRenderer> ().enabled = false;
		airplaneTitle.GetComponent<MeshRenderer> ().enabled = false;
		isEnabled = false;
		
	}
}
