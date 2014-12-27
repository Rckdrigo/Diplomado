using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Follower : MonoBehaviour {

	protected NavMeshAgent agent;
	protected Animator animator;

	protected void Start(){
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

}
