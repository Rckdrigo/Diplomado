using UnityEngine;
using System.Collections;

public class InitialCostumeSelector : MonoBehaviour {

	public Texture[] costumes;

	// Use this for initialization
	void Start () {
		renderer.material.mainTexture = costumes[Random.Range(0,costumes.Length-1)];
	}
	
}
