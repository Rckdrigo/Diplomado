using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;

public class RayCastListener : Singleton<RayCastListener> {

	public delegate void RayCastEventListener();
	public event RayCastEventListener RayCastRight;
	public event RayCastEventListener RayCastLeft;
	public event RayCastEventListener RayCastTouch;

	[HideInInspector()]
	public RaycastHit hitRight;
	[HideInInspector()]
	public RaycastHit hitLeft;
	
	[HideInInspector()]
	public RaycastHit hitTouch;
	
	[HideInInspector()]
	public GameObject right, left, touch;
	[HideInInspector()]
	public GameObject lastRight, lastLeft, lastTouch;
	
	GameObject HitToGameObject(RaycastHit hit){
		return hit.transform.gameObject;
	}	

	void Start(){
		right = left = lastLeft = lastRight = touch = null;
	}

	void Update () {
		//if(!GameState.Instance.paused){
			right = left = null;

			if(Input.GetMouseButtonDown(0)){
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray, out hitLeft)){
					left = HitToGameObject(hitLeft);
					lastLeft = left;
					RayCastLeft();
				}
			}
			
			if(Input.touchCount>0){
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if(Physics.Raycast(ray, out hitTouch)){
					touch = HitToGameObject(hitTouch);
					lastTouch = touch;
					RayCastTouch();
				}
			}

			if(Input.GetMouseButtonDown(1)){
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray, out hitRight)){
					right = HitToGameObject(hitRight);
					lastRight = right;
					RayCastRight();
				}
			}
		//}
	}
}
