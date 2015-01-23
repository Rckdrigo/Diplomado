using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;
using System.Collections.Generic;

public class CharController : Singleton<CharController> {

	public GameObject initalHuman;
	[HideInInspector()]
	public GameObject actualHuman;	
	//[HideInInspector()]
	public List<GameObject> selectedHumans;
	[HideInInspector()]
	public List<GameObject> humans;
	
	public delegate void DeathListener();
	public event DeathListener Lose;
	
	bool lose;
	void Start(){
		humans = new List<GameObject>();
		actualHuman = initalHuman;
		selectedHumans = new List<GameObject>();
		selectedHumans.Add(initalHuman);
		
		RayCastListener.Instance.RayCastLeft += SelectHuman;
		RayCastListener.Instance.RayCastRight += SetSelectedHumanDestination;

		GameState.Instance.Restart += Restart;
	}
	
	void Restart(){
		lose = false;
		selectedHumans.Clear();
		selectedHumans.Add(initalHuman);

		actualHuman = initalHuman;
		initalHuman.GetComponent<MiniManIA>().enabled = true;
		initalHuman.GetComponent<NavMeshAgent>().enabled = true;
	}

	public void DamageHuman(GameObject human, int damageAmount){
		human.GetComponent<LifeManager>().RecievedDamage(damageAmount);
	}

	public void HumanKilled(GameObject human){
		selectedHumans.Remove(human);
		if(human == actualHuman)
			ActualHumanKilled();
	}
	
	void ActualHumanKilled(){
		if(selectedHumans.Count>0){
			actualHuman = selectedHumans[Random.Range(0,selectedHumans.Count)];
			actualHuman.GetComponent<MiniManIA>().ActualSelectedState(true);
		}
		else{
			lose = true;
			Lose();
		}
	}
	
	public void PoolAllCharacters(){
		foreach(GameObject human in humans){
			human.GetComponent<LifeManager>().life = human.GetComponent<LifeManager>().maxlife;
			if(human != initalHuman){
				human.GetComponent<MiniManIA>().enabled = false;
				human.GetComponent<NavMeshAgent>().enabled = false;
				ObjectPool.instance.PoolGameObject(human);
			}
		}
	}
	

	void SelectHuman(){
		if(!lose){
			if (RayCastListener.Instance.left.CompareTag ("MiniMan") && !lose) {
				actualHuman = RayCastListener.Instance.left;
				actualHuman.GetComponent<MiniManIA>().ActualSelectedState(true);
				
				foreach(GameObject human in selectedHumans){
					if(!human.Equals(actualHuman))
						human.GetComponent<MiniManIA>().ActualSelectedState(false);
				}
				
				if(!selectedHumans.Contains(actualHuman)){
					actualHuman.GetComponent<MiniManIA>().Selected();
					selectedHumans.Add(actualHuman);
				}
			}
		}
	}

	void SetSelectedHumanDestination(){
		if(!lose){
			if(!RayCastListener.Instance.right.CompareTag("MiniMan") && !lose)
					actualHuman.GetComponent<MiniManIA> ().destination = RayCastListener.Instance.hitRight.point;
		}

	}
}
