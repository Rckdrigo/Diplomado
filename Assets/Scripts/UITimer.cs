using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UITimer : MonoBehaviour {

	[HideInInspector()]
	public static float difTime;

	void Start(){
		difTime = Time.time;
		GameState.Instance.Restart += Restart;
	}

	void Restart(){
		difTime = Time.time;
	}

	void Update () {
		TimeSpan t = TimeSpan.FromSeconds(Time.time - difTime);
		GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);
		
		if(Input.GetKeyDown(KeyCode.P))
			Time.timeScale = (Time.timeScale == 1) ? 0 : 1;	
	}
}
