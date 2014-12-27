using UnityEngine;
using System.Collections;

public class MiniManIA : Follower {

	public GameObject selectionSprite;
	[HideInInspector()]
	public bool selected;

	new void Start(){
		base.Start ();
		selectionSprite.SetActive (false);
		selected = false;
	}
	
	public void Selected(){
		selected = true;
		StopAllCoroutines();
	}
	
	public void ToggleTargetSprite(bool active){
		selectionSprite.SetActive(active);
	}
	
	new void Update(){
		base.Update();
		animator.SetFloat("Speed",agent.velocity.magnitude);
		
		if(selected && !CharController.Instance.actualHuman.Equals(gameObject)){
			if(Vector3.Distance(CharController.Instance.actualHuman.transform.position,transform.position) > 2.5f)
				destination = CharController.Instance.actualHuman.transform.position;	
			else
				destination = transform.position;
		}
	}

}
