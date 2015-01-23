using UnityEngine;
using System.Collections;

using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(LifeManager))]
[RequireComponent(typeof(Rigidbody))]
public class ZombieBehaviour : Follower {

	public int damageDeal = 50;
	[HideInInspector()]
	public bool onFire;
	public bool panic;
	LifeManager life;

	public List<GameObject> objectives;

	new void Start(){
		base.Start();
		objectives = new List<GameObject>();
		CharController.Instance.Lose += Lose;
		rigidbody.isKinematic = true;
		collider.isTrigger = true;
		life  = GetComponent<LifeManager>();
		life.NoHealth += Panic;
	}

	new void Update(){
		base.Update();

		if(objectives.Count >0){
			if(objectives[0].GetComponent<HumanDeath>().dead){
				objectives.Remove(objectives[0]);
			}
			if(Vector3.Distance(transform.position,objectives[0].transform.position) < 3.5f)	
				animator.SetTrigger("Bite");

			if(life.life>0)
				destination = objectives[0].transform.position;

		}
	}

	void Follow(){
		destination = objectives[0].transform.position;
		StartCoroutine("ChangeObjective");
	}

	IEnumerator ChangeObjective(){
		yield return new WaitForSeconds(3);
		if(objectives.Count>0){
			float min = Vector3.Distance(objectives[0].transform.position,transform.position);
			GameObject newObjective = objectives[0];
			for(int i = 1 ; i< objectives.Count ; i++){
				if(Vector3.Distance(objectives[i].transform.position,transform.position) < min){
					min = Vector3.Distance(objectives[i].transform.position,transform.position);
					newObjective = objectives[i];
				}
 		
				destination = newObjective.transform.position;
			}
		StartCoroutine("ChangeObjective");
		}
	}

	public void ResetPosition(){
		transform.position = initialPosition;
	}

	IEnumerator FireDamage(){
		onFire = true;
		life.RecievedDamage(35);
		yield return new WaitForSeconds(0.5f);
		onFire = false;
	}

	void OnTriggerEnter(Collider collider){
		if(!panic){
			if(collider.CompareTag("MiniMan") && !collider.GetComponent<HumanDeath>().dead && collider.GetComponent<MiniManIA>().selected){
				if(animator.GetCurrentAnimatorStateInfo(0).IsName("rollGround"))
					animator.SetTrigger("Resurrect");
				objectives.Add(collider.gameObject);
			}		
		}
	}
	
	void OnTriggerStay(Collider collider){
		if(!panic ){
			if(!objectives.Contains(collider.gameObject)){
				if(collider.CompareTag("MiniMan") && !collider.GetComponent<HumanDeath>().dead && collider.GetComponent<MiniManIA>().selected){
					if(animator.GetCurrentAnimatorStateInfo(0).IsName("rollGround"))
						animator.SetTrigger("Resurrect");
					objectives.Add(collider.gameObject);
				}	
			}

			if(collider.CompareTag("Fire")){
				if(!onFire && Vector3.Distance(collider.transform.position,transform.position) < 2f)
					StartCoroutine("FireDamage");
				
			}
		}
	}

	void Panic(){
		StopCoroutine("ChangeObjective");
		objectives.Clear();
		panic = true;
		animator.SetTrigger("Panic");
		destination = initialPosition;
		Invoke("Die",5);
	}


	void Die(){
		animator.SetTrigger("Die");
		initialPosition = transform.position;
		destination = initialPosition;
	}

	void Dead(){
		life.life = life.maxlife;
		objectives.Clear();
		panic = false;
	}
	
	void Lose(){
		objectives.Clear();
		animator.SetTrigger("Die");
	}
	
	void Bitting(){
		if(objectives.Count >0){
			if(Vector3.Distance(transform.position,objectives[0].transform.position) < 3.5f && !objectives[0].GetComponent<HumanDeath>().dead){
				CharController.Instance.DamageHuman(objectives[0],damageDeal);
				life.RecoverHealth(20);
			}
		}
	}

}
