using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;

public class GameState : Singleton<GameState>{

	Animator anim;
	[HideInInspector()]
	public bool paused;

	public delegate void GameEvents();
	public event GameEvents Restart;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		CharController.Instance.Lose += Lose;
		RayCastListener.Instance.RayCastRight += StartGame;
	}

	void Lose(){
		anim.SetTrigger("Lose");
	}

	public void StartGame(){
		anim.SetTrigger("Start");
	}

	public void Quit(){
		Application.Quit();
	}

	public void Continue(){
		Time.timeScale = 1;
		anim.SetTrigger("Continue");
	}

	public void RestartGame(){	
		Restart();
		paused = false;
		anim.SetTrigger("Restart");
		Time.timeScale = 1;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}

	public void Pause(){
		anim.SetTrigger("Pause");
		Time.timeScale = 0;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape) 
		    && (anim.GetCurrentAnimatorStateInfo(0).IsName("HUD") 
        	|| anim.GetCurrentAnimatorStateInfo(0).IsName("PauseMenu"))){
			if(paused)
				Continue();
			else
				Pause ();
		}

	}

}

