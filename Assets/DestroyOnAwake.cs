using UnityEngine;
using System.Collections;

public class DestroyOnAwake : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Destroy(gameObject);
	}
	
}
