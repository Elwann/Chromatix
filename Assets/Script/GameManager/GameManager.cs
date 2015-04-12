using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public CameraShake shake;
	public List<PlayerPoints> players = new List<PlayerPoints>();

	private static GameManager instance;
	
	public static GameManager Instance{
		get{
			if(instance == null){
				instance = GameObject.FindObjectOfType<GameManager>();
				DontDestroyOnLoad(instance.gameObject);
			}
			
			return instance;
		}
	}
	
	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		} else {
			if(this != instance) Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
