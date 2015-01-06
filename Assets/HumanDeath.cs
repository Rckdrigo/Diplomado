using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LifeManager))]
public class HumanDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<LifeManager>().NoHealth += Die;
	}
	
	// Update is called once per frame
	void Die () {
		CharController.Instance.HumanKilled(gameObject);
	}
}
