using UnityEngine;
using System.Collections;

public class AirplaneAI : MonoBehaviour {
	//Airplane Details
	public string airplaneTitle = "Default Airplane";
	public int maximumPassengers;
	public int baseWeight = 100000;
	public int fuelCapacity = 10000;
	public int costPerUnit = 70000000;
	public Sprite airplaneImage;

	//Selection Variables
	public Shader selectedShader;
	private Shader defaultShader;
	public bool isSelected;
	private bool isEnabled = false;

	//Detail Panel Objects
	private PanelRenderer panel;
	// Use this for initialization
	void Start () {

		isSelected = false;
		defaultShader = this.renderer.material.shader;

		panel = new PanelRenderer ();
		panel.InitializePanel (this.gameObject, airplaneTitle, maximumPassengers, baseWeight, fuelCapacity, costPerUnit, airplaneImage);

	}
	
	// Update is called once per frame
	void Update () {

		if (isSelected) 
		{
			if(!panel.isEnabled) {
				this.renderer.material.shader = selectedShader;
				panel.Enable();

			}


		} else {
			if(isEnabled) {
				this.renderer.material.shader = defaultShader;
				panel.Disable ();
			}

		}

	}



}
