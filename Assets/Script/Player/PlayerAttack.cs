using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	private PlayerInput controller;

	public Transform gun;
	public Transform spawn;
	public ProjectileMovement bullet;

	public float fireRate = 1f;
	public float rafaleRate = 10f;
	public float rafaleNumber = 3f;

	private float currentRafaleNumber = 0f;
	private float lastFire = 0f;
	private float lastRafale = 0f;
	private float rotation = 0f;

	// Use this for initialization
	void Start ()
	{
		lastFire = Time.time;
		controller = gameObject.GetComponent<PlayerInput>();
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

		// Applique la direction a l'arme
		gun.transform.localEulerAngles = new Vector3(0f, 0f, rotation);

		// Gestion des tir
		if(controller.IsFirePressed()){
			if(Time.time - lastFire >= fireRate){
				//Debug.Log("TIRER");
				Fire(direction);
			}
		}
	}

	void Fire(Vector2 direction){
		ProjectileMovement b = (ProjectileMovement)Instantiate(bullet, spawn.position, gun.rotation);
		if(transform.localScale.x > 0f){
			b.transform.localEulerAngles = new Vector3(0f, 0f, rotation);
		} else {
			b.transform.localEulerAngles = new Vector3(0f, 0f, -rotation);
		}
		b.direction = direction;
		lastFire = Time.time;
	}
}
