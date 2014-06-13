using UnityEngine;
using System.Collections;

public class AirplaneAI : MonoBehaviour {
	//Airplane Details
	public string airplaneTitle = "Default Airplane";
	public int maximumPassengers;
	public int baseWeight = 100000;
	public int fuelCapacity = 10000;
	public int costPerUnit = 70000000;
	public Texture airplaneImage;

	//Selection Variables
	public Shader selectedShader;
	private Shader defaultShader;
	public bool isSelected;

	//Detail Panel Objects

	// Use this for initialization
	void Start () {

		isSelected = false;
		defaultShader = this.renderer.material.shader;

	}
	
	// Update is called once per frame
	void Update () {

		if (isSelected) 
		{
			this.renderer.material.shader = selectedShader;
			EnablePanel();

		} else {
			this.renderer.material.shader = defaultShader;
			DisablePanel();
		}
	}

	void EnablePanel() {

	}
	void DisablePanel() {

	}



}
