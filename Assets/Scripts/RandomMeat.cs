using UnityEngine;
using System.Collections;

public class RandomMeat : MonoBehaviour {

	[System.Serializable]
	public struct Model{
		public Mesh mesh;
		public Texture texture;
	}
	
	public Model[] meat;
	
	// Use this for initialization
	void OnEnable () {
		int i = Random.Range(0,meat.Length-1);
		GetComponent<MeshFilter>().mesh = meat[i].mesh;
		transform.rotation = Quaternion.Euler(Random.onUnitSphere);
		renderer.material.mainTexture = meat[i].texture;
	}
	
}
