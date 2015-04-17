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
	public GameObject explosion;
	public float colorChangeTime;
	private BulletColor[] colors = new BulletColor[]{BulletColor.Red, BulletColor.Green, BulletColor.Blue};
	private int[] currentColor;
	private float lastColorChanged = 0f;

	[Header("Interface")]
	public float focusWidth = 200f;
	public RectTransform focusUI;
	public RectTransform focusUILeft;
	public RectTransform focusUIRight;
	public Text[] scores;
	public Text toNextColor;
	private string[] numbers = new string[]{ "0", "I", "II",  "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };
	public GameObject menu;

	[Header("Raccourcis")]
	public CameraShake shake;

	private static GameManager instance;
	
	public static GameManager Instance{
		get{
			if(instance == null){
				instance = GameObject.FindObjectOfType<GameManager>();
				//DontDestroyOnLoad(instance.gameObject);
			}
			
			return instance;
		}
	}
	
	void Awake(){
		if(instance == null){
			instance = this;
			//DontDestroyOnLoad(this);
		} else {
			if(this != instance) Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		StartLevel ();

		// On affiche le menu si le jeu est en pause
		if(IsPaused()){
			menu.SetActive(true);
		} else {
			menu.SetActive(false);
		}
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


		//Pause game
		// TODO: Controller Input
		//if(Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown("Start")) || (Input.GetButtonDown("Start2"))){
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!IsPaused()){
				Pause ();
			} else {
				Resume();
			}
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
		++points[id];
		scores[id].text = NumberToRoman(points[id]);
	}

	public void RemovePoint(int id){
		if(points[id] > 0)
			--points[id];
		scores[id].text = NumberToRoman(points[id]);
	}

	public void ResetGame(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void FocusUI(float x){
		//Screen.width;
		focusUILeft.sizeDelta = new Vector2(x - focusWidth / 2f, focusUILeft.sizeDelta.y);
		focusUIRight.sizeDelta = new Vector2((Camera.main.pixelWidth - x) - focusWidth / 2f, focusUILeft.sizeDelta.y);

		focusUI.gameObject.SetActive(true);
	}

	public void UnFocusUI(){
		focusUI.gameObject.SetActive(false);
	}

	public void Pause(){
		Time.timeScale = 0f;
		menu.SetActive(true);
	}

	public void Resume(){
		Time.timeScale = 1f;
		menu.SetActive(false);
	}

	public bool IsPaused(){
		return (Time.timeScale == 0f);
	}

	public void Quit(){
		Application.Quit();
	}
}
