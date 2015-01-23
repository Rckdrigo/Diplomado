using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	[Range(1f,10f)]
	public float camSmooth = 1f;

	// Update is called once per frame
	void Update () {
		Vector3 position = Vector3.zero;
		if(CharController.Instance.selectedHumans.Count > 0){
			foreach(GameObject human in CharController.Instance.selectedHumans){
				position += human.transform.position;
			}
			position /= CharController.Instance.selectedHumans.Count;
			position.y = transform.position.y;
			
			transform.position = Vector3.Lerp(transform.position,position,Time.deltaTime * camSmooth);
		}
	}
}
