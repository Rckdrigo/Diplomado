using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(LifeManager))]
[RequireComponent(typeof(Rigidbody))]
public class SkeletonBehaviour : Follower {

	protected bool following;
	GameObject currentObjective;
	public int damageDeal = 10;
	[HideInInspector()]
	public bool onFire;
	LifeManager life;

	new void Start(){
		rigidbody.isKinematic = true;
		base.Start();
		collider.isTrigger = true;
		following = false;
		currentObjective = null;
		life  = GetComponent<LifeManager>();
		life.NoHealth += Die;
	}
	
	new void Update(){
		base.Update();
		animator.SetFloat("Speed",agent.velocity.magnitude);
		if(following)
			destination = currentObjective.transform.position;
	}
	
	void Follow(){
		following = true;
		agent.SetDestination(currentObjective.transform.position);
	}
	
	IEnumerator FireDamage(){
		onFire = true;
		life.RecievedDamage(10);
		yield return new WaitForSeconds(0.5f);
		onFire = false;
	}
	
	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag("MiniMan") && !following && collider.GetComponent<MiniManIA>().selected){
			following = true;
			wander = false;
			agent.enabled = true;
			currentObjective = collider.gameObject;
		}	
	}
	
	void OnTriggerStay(Collider collider){
		if(following && collider.CompareTag("MiniMan")){
			if(currentObjective.activeSelf == true){
				if(Vector3.Distance(transform.position,collider.transform.position) < Vector3.Distance(transform.position,currentObjective.transform.position))
					currentObjective = collider.gameObject;
				
				if(Vector3.Distance(transform.position,currentObjective.transform.position) < 3.5f)
					animator.SetTrigger("Attack");	
			}else
				currentObjective = collider.gameObject;
		}
		
		
		if(following && collider.CompareTag("Fire")){
			if(!onFire && Vector3.Distance(collider.transform.position,transform.position) < 2f)
				StartCoroutine("FireDamage");
			
		}
	}
	
	void Hit(){
		if(currentObjective != null){
			if(Vector3.Distance(transform.position,currentObjective.transform.position) < 3f)
				CharController.Instance.DamageHuman(currentObjective,damageDeal);
			
		}
	}
	
	void Die(){
		animator.SetTrigger("Die");
		currentObjective = null;
		agent.enabled = false;
		following = false;
	}
}
