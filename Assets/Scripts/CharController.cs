using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;
using System.Collections.Generic;

public class CharController : Singleton<CharController> {

	public GameObject initalHuman;
	[HideInInspector()]
	public GameObject actualHuman;	
	[HideInInspector()]
	public List<GameObject> selectedHumans;
	[HideInInspector()]
	public List<GameObject> humans;
	
	public delegate void DeathListener();
	public event DeathListener Lose;
	
	bool lose;
	
	void OnEnable(){
		RayCastListener.Instance.RayCastTouch += SelectHuman;
		RayCastListener.Instance.RayCastTouch += SetSelectedHumanDestination;
	}
	
	void Start(){
		actualHuman = initalHuman;
		initalHuman.GetComponent<MiniManIA>().ToggleTargetSprite(true);
		initalHuman.GetComponent<MiniManIA>().Selected();
		selectedHumans = new List<GameObject>();
		humans = new List<GameObject>();
		selectedHumans.Add(initalHuman);
	}

	public void DamageHuman(GameObject human, int damageAmount){
		human.GetComponent<LifeManager>().RecievedDamage(damageAmount);
	}

	public void HumanKilled(GameObject human){
		human.GetComponent<MiniManIA>().StopCoroutine("FollowLeader");
		if(human == actualHuman){
			ActualHumanKilled();
		}
		else{
			selectedHumans.Remove(human);
			human.GetComponent<Animator>().SetTrigger("Die");
		}
	}
	
	void ActualHumanKilled(){
		selectedHumans.Remove(actualHuman);
		actualHuman.GetComponent<Animator>().SetTrigger("Die");
		actualHuman.GetComponent<MiniManIA>().ToggleTargetSprite(false);
		if(selectedHumans.Count>0){
			actualHuman = selectedHumans[Random.Range(0,selectedHumans.Count)];
			actualHuman.GetComponent<MiniManIA>().ToggleTargetSprite(true);
		}
		else{
			lose = true;
			PoolAllCharacters();
			Lose();
		}
	}
	
	void PoolAllCharacters(){
		foreach(GameObject human in humans){
			human.GetComponent<LifeManager>().life = human.GetComponent<LifeManager>().maxlife;
			if(!human.GetComponent<MiniManIA>().initial){
				human.GetComponent<MiniManIA>().enabled = false;
				human.GetComponent<NavMeshAgent>().enabled = false;
				ObjectPool.instance.PoolGameObject(human);
			}
			//else
				//human.GetComponent<MiniManIA>().ResetPosition();
		}
	}
	

	void SelectHuman(){
		if (RayCastListener.Instance.touch.CompareTag ("MiniMan") && !lose) {
			actualHuman = RayCastListener.Instance.touch;
			actualHuman.GetComponent<MiniManIA>().ToggleTargetSprite(true);
			
			foreach(GameObject human in selectedHumans){
				if(!human.Equals(actualHuman))
					human.GetComponent<MiniManIA>().ToggleTargetSprite(false);
			}
			
			if(!selectedHumans.Contains(actualHuman)){
				actualHuman.GetComponent<MiniManIA>().Selected();
				selectedHumans.Add(actualHuman);
			}
			
			
		}
	}

	void SetSelectedHumanDestination(){
		if(!RayCastListener.Instance.touch.CompareTag("MiniMan") && !lose)
				actualHuman.GetComponent<MiniManIA> ().destination = RayCastListener.Instance.hitTouch.point;

	}
}
