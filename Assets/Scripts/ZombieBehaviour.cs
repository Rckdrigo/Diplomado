using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(LifeManager))]
[RequireComponent(typeof(Rigidbody))]
public class ZombieBehaviour : Follower {

	protected bool following;
	GameObject currentObjective;
	public int damageDeal = 50;
	[HideInInspector()]
	public bool onFire;
	public bool panic;
	LifeManager life;
	
	new void Start(){
		base.Start();
		rigidbody.isKinematic = true;
		collider.isTrigger = true;
		following = false;
		currentObjective = null;
		life  = GetComponent<LifeManager>();
		life.NoHealth += Panic;
	}

	new void Update(){
		base.Update();
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
		if(!panic){
			if(collider.CompareTag("MiniMan") && !following && collider.GetComponent<MiniManIA>().selected){
				following = true;
				animator.SetTrigger("Resurrect");
				currentObjective = collider.gameObject;
			}		
		}
	}
	
	void OnTriggerStay(Collider collider){
		if(!panic){
			if(following && collider.CompareTag("MiniMan")){
				if(currentObjective.activeSelf == true){
					if(Vector3.Distance(transform.position,collider.transform.position) < Vector3.Distance(transform.position,currentObjective.transform.position))
						currentObjective = collider.gameObject;
	
					if(Vector3.Distance(transform.position,currentObjective.transform.position) < 3.5f)
						animator.SetTrigger("Bite");	
				}else
					currentObjective = collider.gameObject;
			}
			
			
			if(following && collider.CompareTag("Fire")){
				if(!onFire && Vector3.Distance(collider.transform.position,transform.position) < 2f)
					StartCoroutine("FireDamage");
				
			}
		}
	}

	void Panic(){
		panic = true;
		animator.SetTrigger("Panic");
		following = false;
		destination = initialPosition;
		Invoke("Die",10);
	}


	void Die(){
		animator.SetTrigger("Die");
		initialPosition = transform.position;
		destination = initialPosition;
	}

	void Dead(){
		life.life = life.maxlife;
		currentObjective = null;
		panic = false;
	}
	
	void Bitting(){
		if(currentObjective != null){
			if(Vector3.Distance(transform.position,currentObjective.transform.position) < 3f)
				CharController.Instance.DamageHuman(currentObjective,damageDeal);

		}
	}

}
