using UnityEngine;
using System.Collections;

public class MiniManIA : Follower {

	public GameObject selectionSprite;
	[HideInInspector()]
	public bool selected;

	new void Start(){
		base.Start ();
	}
	
	void OnEnable(){
		selectionSprite.SetActive (false);
		selected = false;
	}
	
	public void Selected(){
		selected = true;
		StopAllCoroutines();
	}
	
	public void ToggleTargetSprite(bool active){
		selectionSprite.SetActive(active);
		if(!active){
			StartCoroutine("FollowLeader");
			agent.stoppingDistance = 2;
		}
		else{
			StopCoroutine("FollowLeader");
			agent.stoppingDistance = 0;
		}
	}
	
	public IEnumerator FollowLeader(){
		if(selected && !CharController.Instance.actualHuman.Equals(gameObject))
			if(Vector3.Distance(CharController.Instance.actualHuman.transform.position,transform.position) > 1.5f)
				destination = CharController.Instance.actualHuman.transform.position;	
		yield return new WaitForSeconds(0.5f);
		StartCoroutine("FollowLeader");
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
			
			ObjectPool.instance.PoolGameObject(collider.gameObject);
		}
	}
	

}
