using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIScore : MonoBehaviour {
	
	int maxChars;
	float timeRecord;
	public Text scoreText;
	
	// Use this for initialization
	void Start () {
		if(!PlayerPrefs.HasKey("RecordChars")){
			PlayerPrefs.SetInt("RecordChars",1);
			PlayerPrefs.SetFloat("RecordTime",0f);
		}
		
		maxChars = PlayerPrefs.GetInt("RecordChars");
		
		CharController.Instance.Lose += SaveStats;
	}

	void Update () {
//		print (CharController.Instance.selectedHumans.Count);
		if(CharController.Instance.selectedHumans.Count > maxChars)
			maxChars = CharController.Instance.selectedHumans.Count;

	}
	
	void SaveStats(){
		
		if(Time.time - UITimer.difTime> PlayerPrefs.GetFloat("RecordTime")) 
			PlayerPrefs.SetFloat("RecordTime",UITimer.difTime-Time.time);

		TimeSpan t = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("RecordTime"));
		scoreText.text = "Max. Kids: " + maxChars + "\n\nTime: " + string.Format("{0:D2}:{1:D2}:{2:D3}", -t.Minutes, -t.Seconds, -t.Milliseconds);
		PlayerPrefs.SetInt("RecordChars",maxChars);

	}
	
}
