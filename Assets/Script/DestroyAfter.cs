using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

	public float destroyTime = 1f;

	// Use this for initialization
	void Start () {
		Invoke("Disapear", destroyTime);
	}

	void Disapear(){
		Destroy (gameObject);
	}
}
