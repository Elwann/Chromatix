using UnityEngine;
using System.Collections;

public class PlayerDefense : MonoBehaviour {

	private PlayerInput controller;
	private PlayerMovement movement;

	public Transform shield;

	// Lifes
	private bool red = true;
	private bool green = true;
	private bool blue = true;

	// Directions
	private float rotation = 0f;
	private float rotationOffset = 90f;

	// Use this for initialization
	void Start ()
	{
		controller = gameObject.GetComponent<PlayerInput>();
		movement = gameObject.GetComponent<PlayerMovement>();
	}

	// Update is called once per frame
	void Update ()
	{
		// Calcule de la rotation de l'arme
		Vector2 direction = new Vector2(controller.Horizontal(), controller.Vertical());
		if(direction.x == 0f && direction.y == 0f){
			if(transform.localScale.x > 0f){
				direction = Vector2.right;
			} else {
				direction = -Vector2.right;
			}
			rotation = 90f;
		} else {
			rotation = Vector2.Angle(direction, -Vector2.up);
		}
	}

	void LateUpdate(){
		// Applique la direction a l'arme
		shield.transform.localEulerAngles = new Vector3(0f, 0f, rotation - rotationOffset);
	}
}
