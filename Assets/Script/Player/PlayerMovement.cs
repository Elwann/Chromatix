using UnityEngine;
using System.Collections;

[AddComponentMenu("Chromatix/Player/PlayerMovement")]
public class PlayerMovement : MonoBehaviour {

	public Vector2 velocity;

	public Animator animator;
	//public Transform model;
	private PlayerInput controller;
	private Rigidbody2D rigidbody2d;
	private PlayerDamage playerDamage;
	//public PlayerGroundDetection playerDetection;
	
	public float maxSpeed = 20f;
	public float normalAccel = 1f;
	public float gravity = -9.81f;
	int groundLayer;
	//public float turnMul = 2f;
	
	public float initialJumpSpeed = 50f;

	void Awake()
	{
		groundLayer = 1 << LayerMask.NameToLayer("Ground");
		//groundLayer = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Player"));
		controller = gameObject.GetComponent<PlayerInput>();
		rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
		playerDamage = gameObject.GetComponent<PlayerDamage>();
	}
	
	void Update()
	{
		if (!playerDamage.IsDead ()) {

			if (IsGrounded ()) {
				animator.SetBool ("Air", false);
				GroundMove ();
			} else {
				animator.SetBool ("Air", true);
				AirMove ();
			}

			velocity.y += gravity;
			velocity.x = Mathf.Clamp (velocity.x, -maxSpeed, maxSpeed);
			animator.SetFloat ("Speed", Mathf.Abs (velocity.x / maxSpeed));
		} 
	}

	void FixedUpdate()
	{
		if(!playerDamage.IsDead()){
			rigidbody2d.velocity = new Vector2(velocity.x, velocity.y);
		} else {
			rigidbody2d.velocity = Vector2.zero;
		}
	}

	void GroundMove()
	{
		if (!playerDamage.IsDead ()) {
			velocity.y = 0;

			float h = controller.Horizontal ();
			if (h != 0) {
				transform.localScale = new Vector3 (Mathf.Sign (h), 1f, 1f);
				velocity += new Vector2 (h, 0f) * normalAccel;
			} else {
				velocity = Vector2.zero;
			}

			// Start jump from ground
			if (controller.IsJumpPressed ()) {
				//Debug.Log(controller.IsJumpDown());
				velocity.y = initialJumpSpeed;
				return;
			} else {
				velocity.y = 0;
			}
		}
	}

	bool IsGrounded()
	{
		Vector2 start = new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f); 
		//Vector2 start = new Vector2(collider2D.bounds.min.x, collider2D.bounds.min.y+0.05f);
		Vector2 stop = new Vector2(transform.position.x + 0.5f, transform.position.y - 0.6f); 
		//Vector2 stop = new Vector2(collider2D.bounds.max.x, collider2D.bounds.min.y-0.05f);
		Debug.DrawRay(start, stop - start, Color.green);

		return Physics2D.OverlapArea(stop, start, groundLayer);
	}

	void AirMove()
	{
		if (!playerDamage.IsDead ()) {
			if ((velocity.y > 0) && (!controller.IsJumpPressed ()))
				velocity.y = 0;

			float h = controller.Horizontal ();
			if (h != 0) {
				transform.localScale = new Vector3 (Mathf.Sign (h), 1f, 1f);
			}

			velocity += new Vector2 (h, 0f) * normalAccel;
		}
	}
}
