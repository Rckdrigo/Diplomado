using UnityEngine;
using System.Collections;

public class MainCharController : Follower {
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {



		if(Input.GetMouseButtonDown(1)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit)){
				print(hit.transform.name);
				agent.SetDestination(hit.point);
			}
		}
	}
}
