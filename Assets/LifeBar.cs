using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeBar : MonoBehaviour {

	public Image bar;
	LifeManager life;

	// Use this for initialization
	void Start () {
		life = GetComponent<LifeManager>();
	}
	
	// Update is called once per frame
	void Update () {
		bar.fillAmount = (float)life.life / (float)life.maxlife;
		bar.color = Color.Lerp(Color.red,Color.green,bar.fillAmount);
	}
}
