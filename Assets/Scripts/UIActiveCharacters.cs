using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIActiveCharacters : MonoBehaviour {

	public static int maxChars;

	void Start(){
		maxChars = CharController.Instance.humans.Count;
		GameState.Instance.Restart+=Restart;
	}

	void Restart(){
		maxChars = CharController.Instance.humans.Count;
	}

	// Update is called once per frame
	void Update () {
		GetComponent<Text>().text = CharController.Instance.selectedHumans.Count +"/"+maxChars;
	}
}
