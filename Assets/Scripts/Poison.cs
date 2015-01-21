using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {
	public float time = 1f;
	public int damage = 5;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(PoisonDamage());
		CharController.Instance.Lose += Lose;
	}

	void Lose ()
	{
		StopAllCoroutines();
	}
	
	IEnumerator PoisonDamage(){
		yield return new WaitForSeconds(time);
		for(int i = 0;  i < CharController.Instance.selectedHumans.Count; i++)
			CharController.Instance.selectedHumans[i].GetComponent<LifeManager>().RecievedDamage(damage);
		StartCoroutine(PoisonDamage());
	}
	
}
