using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class ZombieBehaviour : Follower {

	private GameObject player;
	protected bool following;

	new void Start(){
		base.Start();
		player = GameObject.FindWithTag("Player");
		collider.isTrigger = true;
		following = false;
	}

	new void Update(){
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk") && following)
			agent.SetDestination(player.transform.position);
		

		if(Vector3.Distance(transform.position,player.transform.position) < 4)
			animator.SetTrigger("Bite");
	}

	new void OnTriggerEnter(Collider collider){
		if(collider.CompareTag("Player") && !following){
			following = true;
			animator.SetTrigger("Resurrect");
		}

		//if(following && collider.CompareTag("MiniMan")){
		//	objective = collider.gameObject.transform;
		//}

	}


}
