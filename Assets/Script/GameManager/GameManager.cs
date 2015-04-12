using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public CameraShake shake;
	public GameObject[] players;
	public Dictionary<string, int> names;
	public int[] points;
	public Transform[] spawnPoint;

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
		StartLevel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartLevel(){
		names = new Dictionary<string, int>();
		points = new int[players.Length];
		for(int i = 0; i < players.Length; i++) {
			names.Add(players[i].name, i);
			points[i] = 0;
			Spawn(players[i].name);
		}
	}

	public void Spawn(string player){
		Transform point = spawnPoint [Random.Range (0, spawnPoint.Length - 1)];
		GameObject go = Instantiate (players[names[player]], point.position, point.rotation) as GameObject;
		go.transform.localScale = point.localScale;
	}

	public void AddPoint(string player){
		points[names[player]]++;
	}

	public void RemovePoint(string player){
		points[names[player]]++;
	}
}
