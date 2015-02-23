using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {
	public int damage = 10;
	
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

		yield return new WaitForSeconds(CharController.Instance.selectedHumans.Count*0.3f+1);
		if(!GameState.Instance.paused)
			for(int i = 0; i < CharController.Instance.selectedHumans.Count; i++)
				CharController.Instance.selectedHumans[i].GetComponent<LifeManager>().RecievedDamage(damage);
		StartCoroutine(PoisonDamage());
	}
	
}
