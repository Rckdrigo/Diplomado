﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class ZombieBehaviour : Follower {

	protected bool following;
	GameObject currentObjective;

	new void Start(){
		base.Start();
		collider.isTrigger = true;
		following = false;
		currentObjective = null;
	}

	new void Update(){
		base.Update();
		if(currentObjective == null && following){
			animator.SetTrigger("Die");
		}
		
		if(following)
			destination = currentObjective.transform.position;
	}
	
	void Follow(){
		following = true;
		agent.SetDestination(currentObjective.transform.position);
	}
	
	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag("MiniMan") && !following && collider.GetComponent<MiniManIA>().selected){
			following = true;
			animator.SetTrigger("Resurrect");
			currentObjective = collider.gameObject;
		}
	}
	
	void OnTriggerStay(Collider collider){
		if(following && collider.CompareTag("MiniMan")){
			if(currentObjective != null){
				if(Vector3.Distance(transform.position,collider.transform.position) < Vector3.Distance(transform.position,currentObjective.transform.position))
					currentObjective = collider.gameObject;
					
				if(Vector3.Distance(transform.position,currentObjective.transform.position) < 3.5f)
					animator.SetTrigger("Bite");	
			}else
				currentObjective = collider.gameObject;
		}
	}

	void Dead(){
		following = false;
	}

	void Bitting(){
		if(currentObjective != null){
			if(Vector3.Distance(transform.position,currentObjective.transform.position) < 3f){
				if(currentObjective == CharController.Instance.actualHuman)
					CharController.Instance.ActualHumanKilled();
				else
					CharController.Instance.HumanKilled(currentObjective);
			}
		}
	}

}
