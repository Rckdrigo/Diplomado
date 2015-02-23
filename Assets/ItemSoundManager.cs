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
		print(audio.clip);
		if(!audio.isPlaying)
			audio.Play();
		
		print ("CRUNCH: " + audio.clip + " " + audio.isPlaying);
	}
}
