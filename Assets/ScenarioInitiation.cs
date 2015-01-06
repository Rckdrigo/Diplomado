using UnityEngine;
using System.Collections;

public class ScenarioInitiation : MonoBehaviour {

	public Transform zombieSpawnPoints;
	public Transform humanSpawnPoints;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < zombieSpawnPoints.childCount; i++){
			GameObject temp = ObjectPool.instance.GetGameObjectOfType("Zombi",true);
			temp.transform.position = zombieSpawnPoints.GetChild(i).position;
			temp.GetComponent<NavMeshAgent>().enabled = true;
			temp.GetComponent<ZombieBehaviour>().enabled = true;
		}

		for(int i = 0; i < humanSpawnPoints.childCount; i++){
			GameObject temp = ObjectPool.instance.GetGameObjectOfType("Human",true);
			temp.transform.position = humanSpawnPoints.GetChild(i).position;
			temp.GetComponent<NavMeshAgent>().enabled = true;
			temp.GetComponent<MiniManIA>().enabled = true;
		}
	}

}
