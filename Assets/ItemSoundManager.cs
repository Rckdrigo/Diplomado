using UnityEngine;
using System.Collections;

public class ItemSoundManager : MonoBehaviour {

	void Start () {
		GameState.Instance.Restart+=Restart;
	}
	
	void Restart(){
		GetComponent<AudioSource>().Stop();
	}

	public void ActivateBiteSound(){
		print(GetComponent<AudioSource>().clip);
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
		
		print ("CRUNCH: " + GetComponent<AudioSource>().clip + " " + GetComponent<AudioSource>().isPlaying);
	}
}
