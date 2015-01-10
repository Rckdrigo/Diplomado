using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIActiveCharacters : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text>().text = "X "+CharController.Instance.selectedHumans.Count;
	}
}
