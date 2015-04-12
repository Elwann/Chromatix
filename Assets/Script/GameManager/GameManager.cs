using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	[Header("Players")]
	public GameObject[] players;
	public int[] points;
	public Transform[] spawnPoint;

	[Header("Bullets")]
	public float colorChangeTime;
	private BulletColor[] colors = new BulletColor[]{BulletColor.Red, BulletColor.Green, BulletColor.Blue};
	private int[] currentColor;
	private float lastColorChanged = 0f;

	[Header("Interface")]
	public Text[] scores;
	public Text toNextColor;
	private string[] numbers = new string[]{ "0", "I", "II",  "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };

	[Header("Raccourcis")]
	public CameraShake shake;

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

		//Time to next color
		float colTime = GetTimeToNextColor();
		// Affiche color
		toNextColor.text = NumberToRoman(colTime);
		// Change color
		if(colTime <= 0f){
			ChangeColor();
		}
	}

	public string NumberToRoman(float n){
		return NumberToRoman(Mathf.RoundToInt(n));
	}

	public string NumberToRoman(int n){
		return numbers[n];
	}

	public float GetTimeToNextColor(){
		return Mathf.Ceil(colorChangeTime - (Time.time - lastColorChanged));
	}

	public string GetRomanTimeToNextColor(){
		return NumberToRoman(GetTimeToNextColor());
	}

	public void ChangeColor(){
		for(int i = 0; i < currentColor.Length; i++) {
			currentColor[i] = (currentColor[i]+1) % (colors.Length);
		}

		lastColorChanged = Time.time;
	}

	public void StartLevel(){
		toNextColor.text = NumberToRoman(0);
		lastColorChanged = Time.time;
		points = new int[players.Length];
		currentColor = new int[players.Length];
		for(int i = 0; i < players.Length; i++) {
			points[i] = 0;
			scores[i].text = NumberToRoman(0);
			currentColor[i] = Random.Range(0,players.Length-1);
			Spawn(i);
		}
	}

	public void Spawn(int id){
		Transform point = spawnPoint [Random.Range (0, spawnPoint.Length - 1)];
		GameObject go = Instantiate (players[id], point.position, point.rotation) as GameObject;
		go.transform.localScale = point.localScale;
		go.GetComponent<PlayerPoints>().tableId = id;
	}

	public BulletColor GetCurrentColor(int id){
		return colors[currentColor[id]];
	}

	public void AddPoint(int id){
		points[id]++;
		scores[id].text = NumberToRoman(points[id]);
	}

	public void RemovePoint(int id){
		points[id]++;
		scores[id].text = NumberToRoman(points[id]);
	}
}
