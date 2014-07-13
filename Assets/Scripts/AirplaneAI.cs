using UnityEngine;
using System.Collections;

public class AirplaneAI : MonoBehaviour {
	//Airplane Details
	public string airplaneTitle = "Default Airplane";
	public int maximumPassengers;
	public int baseWeight = 100000;
	public int fuelCapacity = 10000;
	public int costPerUnit = 70000000;
	public float length = 100;
	public Sprite airplaneImage;

	private int currentPassengers;
	private float currentFuel;
	

	//Selection Variables
	public Shader selectedShader;
	private Shader defaultShader;
	public bool isSelected;
	private bool isEnabled = false;

	//Detail Panel Objects
	private GameObject panel;
	// Use this for initialization
	void Start () {

		isSelected = false;
		defaultShader = this.renderer.material.shader;

		currentPassengers = maximumPassengers - (int)(Mathf.Sin (Random.value) * (maximumPassengers / 1.3));
		currentFuel = Mathf.Sin (Random.value) * (float)fuelCapacity;

	}
	
	// Update is called once per frame
	void Update () {

		if (isSelected) 
		{
			this.renderer.material.shader = selectedShader;

		} else {

			this.renderer.material.shader = defaultShader;

		}

	}

	public int GetPassengers() {
		return currentPassengers;
	}
	public float GetFuel() {
		return currentFuel;
	}



}
