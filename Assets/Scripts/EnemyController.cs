using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : Singleton<EnemyController> {

	[HideInInspector()]
	public List<GameObject> enemies;

	// Use this for initialization
	void Start () {
		enemies = new List<GameObject>();
		CharController.Instance.Lose += PoolAllCharacters;
	}
	
	void PoolAllCharacters(){
		foreach(GameObject enemy in enemies){
			enemy.GetComponent<LifeManager>().life = enemy.GetComponent<LifeManager>().maxlife;
			enemy.GetComponent<Animator>().SetTrigger("Die");
			if(enemy.name.Equals("Zombi")){
				enemy.GetComponent<ZombieBehaviour>().enabled = false;
				enemy.GetComponent<NavMeshAgent>().enabled = false;
				ObjectPool.instance.PoolGameObject(enemy);
			}
		}
	}
}
