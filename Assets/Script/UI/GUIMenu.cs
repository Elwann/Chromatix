using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {

	public CameraShake cameraShake;

	// Use this for initialization
	void Start () {
	
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
}
