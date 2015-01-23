using UnityEngine;
using System.Collections;

public class ItemSoundManager : MonoBehaviour {

	void Start () {
		GameState.Instance.Restart+=Restart;
	}
	
	void Restart(){
		audio.Stop();
	}

	public void ActivateBiteSound(){
		if(!audio.isPlaying)
			audio.Play ();
		print ("CRUNCH: " + audio.clip + " " + audio.isPlaying);
	}
}
