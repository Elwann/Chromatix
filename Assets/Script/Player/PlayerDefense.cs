using UnityEngine;
using System.Collections;

public class PlayerDefense : MonoBehaviour {

	private PlayerInput controller;
	//private PlayerDamage damage;
	//private PlayerMovement movement;

	public Transform shield;
	public Animator shieldAnimator;
	public SpriteRenderer shieldRenderer;
	public BoxCollider2D boxCollider;

	// Lifes
	private bool red = true;
	private bool green = true;
	private bool blue = true;

	// Directions
	private Vector2 direction = new Vector2();
	private float rotation = 0f;
	private float rotationOffset = 90f;

	// Use this for initialization
	void Start ()
	{
		controller = gameObject.GetComponent<PlayerInput>();
		//movement = gameObject.GetComponent<PlayerMovement>();
		//damage = gameObject.GetComponent<PlayerDamage>();
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

	public void GetHit(ProjectileMovement bullet)
	{
		switch(bullet.color){
			case BulletColor.Red:
				shieldRenderer.color = new Color(0f, shieldRenderer.color.g, shieldRenderer.color.b);
				red = GereColorHit(bullet, red);
				break;

			case BulletColor.Green:
				shieldRenderer.color = new Color(shieldRenderer.color.a, 0f, shieldRenderer.color.b);
				green = GereColorHit(bullet, green);
				break;

			case BulletColor.Blue:
				shieldRenderer.color = new Color(shieldRenderer.color.a, shieldRenderer.color.g, 0f);
				blue = GereColorHit(bullet, blue);
				break;
		}
		
		if(!red && !green && !blue){
			boxCollider.enabled = false;
			shieldAnimator.SetBool("Destroyed", true);
		}
	}

	bool GereColorHit(ProjectileMovement bullet, bool col){
		if(col){
			bullet.Explode();
		} else {
			bullet.Launch(direction);
		}

		return false;
	}
}
