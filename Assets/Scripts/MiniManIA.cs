using UnityEngine;
using System.Collections;

public class MiniManIA : Follower {

	public bool initial;
	[HideInInspector()]
	public bool selected;

	public Renderer rimShader;
	
	new void Start(){
		base.Start ();
		GameState.Instance.Restart += Restart;
		CharController.Instance.Lose += Lose;

		rimShader.GetComponent<Renderer>().material.SetColor("_RimColor",Color.white);
		destination = initialPosition;
		if(initial){
			ActualSelectedState(true);
			Selected();
		}
	}

	public void Selected(){
		selected = true;
		StopAllCoroutines();
	}
	
	public void ActualSelectedState(bool active){
		if(!active){
			rimShader.GetComponent<Renderer>().material.SetColor("_RimColor",Color.blue);
			StartCoroutine("FollowLeader");
		}
		else{
			rimShader.GetComponent<Renderer>().material.SetColor("_RimColor",Color.red);
			StopCoroutine("FollowLeader");
		}
	}
	
	public IEnumerator FollowLeader(){
		if(selected && !CharController.Instance.actualHuman.Equals(gameObject))
			if(Vector3.Distance(CharController.Instance.actualHuman.transform.position,transform.position) > 3f)
				destination = CharController.Instance.actualHuman.transform.position;	
		yield return new WaitForSeconds(0.5f);
		StartCoroutine("FollowLeader");
	}
	
	public void ResetPosition(){
		transform.position = initialPosition;
	}

	void Restart(){		
		ResetPosition();
		GetComponent<Collider>().enabled = true;
		rimShader.GetComponent<Renderer>().material.SetColor("_RimColor",Color.white);
		destination = transform.position;
		
		GetComponent<MiniManIA>().StopCoroutine("FollowLeader");
		if(initial){
			ActualSelectedState(true);
			Selected();
		}
		else{
			selected = false;
		}
	}

	void Lose(){
		animator.SetTrigger("Reset");
	}

	void Hit(){}
	
	new void Update(){
		base.Update();
		animator.SetFloat("Speed",agent.velocity.magnitude);
	}
	
	void OnTriggerEnter(Collider collider){
		if(collider.CompareTag("MedPack") && CharController.Instance.actualHuman == gameObject){
			foreach(GameObject o in CharController.Instance.selectedHumans)
				o.GetComponent<LifeManager>().RecoverHealth(50);
			collider.GetComponent<ItemSoundManager>().ActivateBiteSound();
			ObjectPool.instance.PoolGameObject(collider.gameObject);
		}
	}
	

}
