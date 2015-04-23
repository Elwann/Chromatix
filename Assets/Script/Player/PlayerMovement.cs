using UnityEngine;
using System.Collections;

[AddComponentMenu("Chromatix/Player/PlayerMovement")]
public class PlayerMovement : MonoBehaviour {
	public class GroundState
	{
		private GameObject player;
		private float  width;
		private float height;
		private float length;

		private int groundLayer;
		
		//GroundState constructor.  Sets offsets for raycasting.
		public GroundState(GameObject playerRef)
		{
			groundLayer = 1 << LayerMask.NameToLayer("Ground");

			player = playerRef;

			Collider2D collider2d = player.GetComponent<Collider2D>();
			width = collider2d.bounds.extents.x + 0.1f;
			height = collider2d.bounds.extents.y + 0.2f;

			length = 0.05f;
		}
		
		//Returns whether or not player is touching wall.
		public bool isWall()
		{
			bool left = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length, groundLayer);
			bool right = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length, groundLayer);

			//Debug.DrawRay (new Vector2(player.transform.position.x - width, player.transform.position.y),  -Vector2.right, Color.yellow);
			
			if(left || right)
				return true;
			else
				return false;
		}
		
		//Returns whether or not player is touching ground.
		public bool isGround()
		{
			bool bottom1 = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y - height), -Vector2.up, length, groundLayer);
			bool bottom2 = Physics2D.Raycast(new Vector2(player.transform.position.x + (width - 0.2f), player.transform.position.y - height), -Vector2.up, length, groundLayer);
			bool bottom3 = Physics2D.Raycast(new Vector2(player.transform.position.x - (width - 0.2f), player.transform.position.y - height), -Vector2.up, length, groundLayer);

			//Debug.DrawRay (new Vector2(player.transform.position.x, player.transform.position.y - height), -Vector2.up, Color.yellow);
			//Debug.Log (bottom1);

			if(bottom1 || bottom2 || bottom3)
				return true;
			else
				return false;
		}
		
		//Returns whether or not player is touching wall or ground.
		public bool isTouching()
		{
			if(isGround() || isWall())
				return true;
			else
				return false;
		}
		
		//Returns direction of wall.
		public int wallDirection()
		{
			bool left = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length, groundLayer);
			bool right = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length, groundLayer);
			
			if(left)
				return -1;
			else if(right)
				return 1;
			else
				return 0;
		}
	}

	public Animator animator;
	private PlayerInput controller;
	private Rigidbody2D rigidbody2d;
	private PlayerDamage playerDamage;

	public float    speed = 14f;
	public float    accel = 6f;
	public float airAccel = 3f;
	public float     jump = 14f;

	private Vector2 input;
	private GroundState groundState;

	void Awake()
	{
		controller = gameObject.GetComponent<PlayerInput>();
		rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
		playerDamage = gameObject.GetComponent<PlayerDamage>();
		groundState = new GroundState(transform.gameObject);
	}
	
	void Update()
	{
		if (!playerDamage.IsDead ())
		{
			input.x = controller.Horizontal();
			
			if(controller.IsJumpDown())
				input.y = 1;
			
			//Reverse player if going different direction
			if(input.x != 0){
				transform.localScale = new Vector3 (Mathf.Sign (input.x), 1f, 1f);
			}

			if(groundState.isTouching()){
				animator.SetBool ("Air", false);
			} else {
				animator.SetBool ("Air", true);
			}

			animator.SetFloat ("Speed", Mathf.Abs (rigidbody2d.velocity.x / speed));
		}
		else
		{
			input.x = 0f;
			input.y = 0f;
		}
	}

	void FixedUpdate()
	{
		rigidbody2d.AddForce(new Vector2(((input.x * speed) - rigidbody2d.velocity.x) * (groundState.isGround() ? accel : airAccel), 0)); //Move player.
		rigidbody2d.velocity = new Vector2((input.x == 0 && groundState.isGround()) ? 0 : rigidbody2d.velocity.x, (input.y == 1 && groundState.isTouching()) ? jump : rigidbody2d.velocity.y);//Stop player if input.x is 0 (and grounded) and jump if input.y is 1

		if(groundState.isWall() && !groundState.isGround() && input.y == 1)
			rigidbody2d.velocity = new Vector2(-groundState.wallDirection() * speed * 0.75f, rigidbody2d.velocity.y); //Add force negative to wall direction (with speed reduction)

		if (!groundState.isTouching() && !controller.IsJumpPressed () && rigidbody2d.velocity.y > 0f) {
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0f);
		}
		
		input.y = 0;
	}
}