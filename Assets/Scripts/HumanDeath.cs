using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LifeManager))]
public class HumanDeath : MonoBehaviour {
	public bool dead;
	// Use this for initialization
	void Start () {
		GetComponent<LifeManager>().NoHealth += Die;
		GameState.Instance.Restart+=Restart;
	}

	void Restart(){
		dead = false;
	}
	
	// Update is called once per frame
	void Die () {
		dead = true;
		GetComponent<AudioSource>().Stop();
		UIActiveCharacters.maxChars--;
		GetComponent<Animator>().SetTrigger("Die");
		GetComponent<MiniManIA>().StopCoroutine("FollowLeader");
		GetComponent<MiniManIA>().selected = false;
		GetComponent<MiniManIA>().rimShader.material.SetColor("_RimColor",Color.black);
		GetComponent<Collider>().enabled= false;

		CharController.Instance.HumanKilled(gameObject);
	}
}
