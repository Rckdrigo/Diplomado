using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;

public class RayCastListener : Singleton<RayCastListener> {

	public delegate void RayCastEventListener();
	public event RayCastEventListener RayCastRight;
	public event RayCastEventListener RayCastLeft;

	[HideInInspector()]
	public RaycastHit hitRight;
	[HideInInspector()]
	public RaycastHit hitLeft;
	
	[HideInInspector()]
	public GameObject right, left;
	[HideInInspector()]
	public GameObject lastRight, lastLeft;
	
	GameObject HitToGameObject(RaycastHit hit){
		return hit.transform.gameObject;
	}	

	void Start(){
		right = left = lastLeft = lastRight = null;
	}

	void Update () {
		right = left = null;

		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hitLeft)){
				left = HitToGameObject(hitLeft);
				lastLeft = left;
				RayCastLeft();
			}
		}

		/*if(Input.GetMouseButtonDown(1)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hitRight)){
				right = HitToGameObject(hitRight);
				lastRight = right;
				RayCastRight();
			}
		}*/
	}
}
