using UnityEngine;
using System.Collections;

public class Radiation : MonoBehaviour {
	public float time = 7.5f;
	public int damage = 5;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(RadiationDamage());
	}
	
	IEnumerator RadiationDamage(){
		//if(CharController.Instance.selectedHumans.Count>0)
			foreach(GameObject o in CharController.Instance.selectedHumans)
				o.GetComponent<LifeManager>().RecievedDamage(damage);
		yield return new WaitForSeconds(time);
		
		//if(CharController.Instance.selectedHumans.Count>0)
			StartCoroutine(RadiationDamage());
		}
}
