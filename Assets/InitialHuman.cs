using UnityEngine;
using System.Collections;

public class InitialHuman : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ObjectPool.instance.PoolGameObjectActive(gameObject);
	}

}
