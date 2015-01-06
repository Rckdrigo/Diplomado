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
	
	void OnEnable(){
		RayCastListener.Instance.RayCastLeft += SelectHuman;
		RayCastListener.Instance.RayCastRight += SetSelectedHumanDestination;
	}
	
	void Start(){
		initalHuman.GetComponent<MiniManIA>().ToggleTargetSprite(true);
		initalHuman.GetComponent<MiniManIA>().Selected();
		actualHuman = initalHuman;
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
		ObjectPool.instance.PoolGameObject(human);
	}
	
	void ActualHumanKilled(){
		selectedHumans.Remove(actualHuman);
		ObjectPool.instance.PoolGameObject(actualHuman);
		if(selectedHumans.Count>0){
			actualHuman = selectedHumans[Random.Range(0,selectedHumans.Count)];
			actualHuman.GetComponent<MiniManIA>().ToggleTargetSprite(true);
		}
		else
			print ("Lose");
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
		actualHuman.GetComponent<MiniManIA> ().destination = RayCastListener.Instance.hitRight.point;
	}
}
