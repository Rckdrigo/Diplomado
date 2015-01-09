using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UITimer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		TimeSpan t = TimeSpan.FromSeconds(Time.time);
		GetComponent<Text>().text = string.Format("Time \n{0:D2}:{1:D2}:{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);
	}
}
