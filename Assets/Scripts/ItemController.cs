 using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;
using System.Collections.Generic;

public class ItemController : Singleton<ItemController> {

	[HideInInspector()]
	public List<GameObject> items;
	
	// Use this for initialization
	void Start () {
		items = new List<GameObject>();
		CharController.Instance.Lose += PoolAllItems;
	}
	
	void PoolAllItems(){
		foreach(GameObject item in items){
			ObjectPool.instance.PoolGameObject(item);
		}
	}
}
