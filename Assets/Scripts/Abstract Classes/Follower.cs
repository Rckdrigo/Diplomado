using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class Follower : MonoBehaviour {

	protected NavMeshAgent agent;
	protected Animator animator;
	
	public bool wander;

	[HideInInspector()]
	public Vector3 destination;


	protected IEnumerator Wander(float time){
		destination = transform.position + Random.onUnitSphere * 3;
		destination.y = 0;
		agent.SetDestination(destination);
		yield return new WaitForSeconds(time);
		StartCoroutine(Wander(time));
	}

	protected void Start(){
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		if(!wander)
			destination = transform.position;
		else
			StartCoroutine(Wander(3f));
	}

	protected void Update(){
		agent.SetDestination (destination);
	}

}
