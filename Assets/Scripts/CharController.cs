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
	
	public delegate void DeathListener();
	public event DeathListener Lose;
	
	void OnEnable(){
		RayCastListener.Instance.RayCastLeft += SelectHuman;
		RayCastListener.Instance.RayCastRight += SetSelectedHumanDestination;
	}
	
	void Start(){
		actualHuman = initalHuman;
		initalHuman.GetComponent<MiniManIA>().ToggleTargetSprite(true);
		initalHuman.GetComponent<MiniManIA>().Selected();
		selectedHumans = new List<GameObject>();
		selectedHumans.Add(initalHuman);
	}

	public void DamageHuman(GameObject human, int damageAmount){
		human.GetComponent<LifeManager>().RecievedDamage(damageAmount);
	}

	public void HumanKilled(GameObject human){
		if(human == actualHuman){
			ActualHumanKilled();
			return;
		}
		selectedHumans.Remove(human);
		actualHuman.GetComponent<Animator>().SetTrigger("Die");
		//ObjectPool.instance.PoolGameObject(human);
	}
	
	void ActualHumanKilled(){
		selectedHumans.Remove(actualHuman);
		if(selectedHumans.Count>0){
			ObjectPool.instance.PoolGameObject(actualHuman);
			actualHuman = selectedHumans[Random.Range(0,selectedHumans.Count)];
			actualHuman.GetComponent<MiniManIA>().ToggleTargetSprite(true);
		}
		else{
			actualHuman.GetComponent<Animator>().SetTrigger("Die");
			actualHuman.GetComponent<MiniManIA>().ToggleTargetSprite(false);
			Lose();
		}
	}

	void SelectHuman(){
		if (RayCastListener.Instance.left.CompareTag ("MiniMan")) {
			actualHuman = RayCastListener.Instance.left;
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
		if(!RayCastListener.Instance.right.CompareTag("MiniMan"))
				actualHuman.GetComponent<MiniManIA> ().destination = RayCastListener.Instance.hitRight.point;

	}
}
