using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	private PlayerInput controller;
	private PlayerMovement movement;

	public Transform gun;
	public Transform spawn;
	public ProjectileMovement bullet;
	public BulletColor currentBulletColor = BulletColor.Red;

	public float fireRate = 1f;
	public float backFire = 1f;
	public float rafaleRate = 10f;
	public int rafaleNumber = 3;

	// Fire rates
	private int currentRafaleNumber = 0;
	private float lastFire = 0f;
	private float lastRafale = 0f;

	// Directions
	private Vector2 direction = Vector2.zero;
	private float rotation = 0f;
	private float rotationOffset = 44.5f;

	// Use this for initialization
	void Start ()
	{
		lastFire = Time.time;
		controller = gameObject.GetComponent<PlayerInput>();
		movement = gameObject.GetComponent<PlayerMovement>();

	}

	// Update is called once per frame
	void Update ()
	{
		// Calcule de la rotation de l'arme
		direction = new Vector2(controller.Horizontal(), controller.Vertical());
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

		if(Time.time - lastRafale >= rafaleRate){
			currentRafaleNumber = 0;
		}
	}

	void LateUpdate(){
		// Applique la direction a l'arme
		gun.transform.localEulerAngles = new Vector3(0f, 0f, rotation - rotationOffset);

		// Gestion des tir
		if(controller.IsFirePressed()){
			if(Time.time - lastFire >= fireRate && currentRafaleNumber < rafaleNumber){
				Fire();
			}
		}
	}

	void Fire(){
		// On tire le projectile
		ProjectileMovement b = (ProjectileMovement)Instantiate(bullet, spawn.position, gun.rotation);
		if(transform.localScale.x > 0f){
			b.transform.localEulerAngles = new Vector3(0f, 0f, rotation);
		} else {
			b.transform.localEulerAngles = new Vector3(0f, 0f, -rotation);
		}
		b.direction = direction;

		// On donne de l'effet au personnage
		GameManager.Instance.shake.ShakeCamera(0.15f, 4f, new Vector3(direction.x, direction.y, 0f));
		movement.velocity -= direction.normalized * backFire;

		//On setup les timer de tir
		if (currentRafaleNumber == 0)
			lastRafale = Time.time;
		currentRafaleNumber++;
		lastFire = Time.time;
	}
}
