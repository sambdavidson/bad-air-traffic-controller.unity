using UnityEngine;
using System.Collections;

public class AirplaneSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	//Editor Icon
	void OnDrawGizmos() {
		Gizmos.DrawIcon(transform.position, "controller.png", true);
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) 
		{
			Debug.DrawLine (ray.origin, hit.point, Color.cyan);
		}
		if (Input.GetMouseButtonDown (0)) 
		{
			GameObject clicked = hit.collider.gameObject;
			AirplaneAI airplaneAI = clicked.GetComponent<AirplaneAI>();
			if( airplaneAI != null ) {
				airplaneAI.isSelected = !airplaneAI.isSelected;
			}

		}
	}

}
