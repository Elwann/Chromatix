using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {

	public CameraShake cameraShake;
	public GameObject controlsUI;

	// Use this for initialization
	void Start () {
		controlsUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LogoShake(float intensity){
		cameraShake.ShakeCamera(intensity, 7f, new Vector3());
	}

	public void Play(){
		Application.LoadLevel("main");
	}

	public void Quit(){
		Application.Quit();
	}

	public void OpenControls(){
		controlsUI.SetActive (true);
	}

	public void CloseControls(){
		controlsUI.SetActive (false);
	}
}
