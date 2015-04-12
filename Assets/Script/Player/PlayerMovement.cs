using UnityEngine;
using System.Collections;

[AddComponentMenu("Chromatix/Player/PlayerMovement")]
public class PlayerMovement : MonoBehaviour {

	public Vector2 velocity;

	public Animator animator;
	//public Transform model;
	private PlayerInput controller;
	private Rigidbody2D rigidbody2D;
	private BoxCollider2D collider2D;
	private PlayerDamage playerDamage;
	//public PlayerGroundDetection playerDetection;
	
	public float maxSpeed = 20f;
	public float normalAccel = 1f;
	public float gravity = -9.81f;
	int groundLayer;
	//public float turnMul = 2f;
	
	public float initialJumpSpeed = 50f;
	bool groundedLastFrame = false;

	bool pressed = false;


	void Awake()
	{
		groundLayer = 1 << LayerMask.NameToLayer("Ground");
		collider2D = gameObject.GetComponent<BoxCollider2D>();
		controller = gameObject.GetComponent<PlayerInput>();
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		playerDamage = gameObject.GetComponent<PlayerDamage>();
	}
	
	void Update()
	{
		if (!playerDamage.IsDead ()) {
			if (IsGrounded ()) {
				GroundMove ();
			} else {
				AirMove ();
			}

			velocity.y += gravity;
			velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

			animator.SetFloat ("Speed", Mathf.Abs (velocity.x / maxSpeed));
		}
	}

	void FixedUpdate()
	{
		if(!playerDamage.IsDead()){
			rigidbody2D.velocity = new Vector2(velocity.x, velocity.y);
		}
	}

	void GroundMove()
	{
		velocity.y = 0;

		float h = controller.Horizontal();
		if(h != 0){
			transform.localScale = new Vector3(Mathf.Sign(h), 1f ,1f);
			velocity += new Vector2(h, 0f) * normalAccel;
		} else {
			velocity = Vector2.zero;
		}

		// Start jump from ground
		if (controller.IsJumpPressed()) {
			//Debug.Log(controller.IsJumpDown());
			velocity.y = initialJumpSpeed;
			return;
		} else {
			velocity.y = 0;
		}
	}

	bool IsGrounded()
	{
		Vector2 start = new Vector2(collider2D.bounds.min.x, collider2D.bounds.min.y+0.05f);
		Vector2 stop = new Vector2(collider2D.bounds.max.x, collider2D.bounds.min.y-0.05f);
		return Physics2D.OverlapArea(stop, start, groundLayer);

		/*if(Mathf.Abs ( rigidbody2D.velocity.y ) < 0.1f) {	// Checking floats for exact equality is bad. Also good for platforms that are moving down.
			
			// Since it's possible for our velocity to be exactly zero at the apex of our jump,
			// we actually require two zero velocity frames in a row.
			
			if(groundedLastFrame)
				return true;
			
			groundedLastFrame = true;
		} else {
			groundedLastFrame = false;
		}
		
		return false;*/
	}

	void AirMove()
	{
		if ((velocity.y > 0) && (!controller.IsJumpPressed()))
			velocity.y = 0;

		float h = controller.Horizontal();
		transform.localScale = new Vector3(Mathf.Sign(h), 1f ,1f);
		velocity += new Vector2(h, 0f) * normalAccel;
	}

	/*
	
	float MaxHorizontalSpeed() {
		if (playerInput.IsBoostPressed()) return boostMaxHorizSpeed;
		return normMaxHorizSpeed;
	}
	
	float walkAccel() {
		if (playerInput.IsBoostPressed()) return boostAccel;
		return normalAccel;
	}
	
	bool walk() {
		float sign = 0;
		
		if (playerInput.IsRightPressed()) sign = 1;
		else if (playerInput.IsLeftPressed()) sign = -1;
		else return false;
		
		float currSign = Mathf.Sign(velocity.x);
		float v = walkAccel();
		if ((currSign != 0) && (currSign != sign))
			v *= turnMul;
		velocity.x += v * sign * Time.deltaTime * 60;
		return true;
	}
	
	void airControls() {
		// Abort jump if user lets go of button 
		if ((velocity.y > 0) && (!playerInput.IsJumpPressed()))
			velocity.y = 0;
		
		// Air walk
		walk();
	}
	
	void groundControls() {
		velocity.y = 0;
		
		// Start jump from ground
		if (playerInput.IsJumpPressed()) {
			velocity.y = initialJumpSpeed;
			return;
		} else {
			velocity.y = 0;
		}
		
		// Run on ground 
		if (!walk())
			velocity.x = 0;
	}*/
}
