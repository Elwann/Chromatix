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

	public void LogoShake(){
		cameraShake.ShakeCamera(4f, 7f, new Vector3());
	}
}
