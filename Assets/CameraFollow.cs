using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		Vector3 position = Vector3.zero;
		
		foreach(GameObject human in CharController.Instance.selectedHumans){
			position += human.transform.position;
		}
		position /= CharController.Instance.selectedHumans.Count;
		position.y = transform.position.y;
		
		transform.position = Vector3.Lerp(transform.position,position,Time.time);
	}
}
