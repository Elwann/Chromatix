using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour {
	public PlayerPoints points;

	[Header("Sprite")]
	public Sprite red;
	public Sprite green;
	public Sprite blue;

	public Transform rotationable;
	public SpriteRenderer spriteRenderer;
	
	[Header("Vitesse")]
	public BulletColor color = BulletColor.Red;
	public Vector2 direction = new Vector2();
	public float speed = 30f;
	public float spread = 1f;
	public int bounces = 3;

	private Rigidbody2D rigidbody2D;
	private int bounced = 0;
	private int groundLayer;

	void Awake(){
		groundLayer = LayerMask.NameToLayer("Ground");
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		float rotation = Vector2.Angle(rigidbody2D.velocity, -Vector2.up);
		if(rigidbody2D.velocity.x < 0){
			rotation = -rotation;
		}
		rotationable.localEulerAngles = new Vector3(0f, 0f, rotation);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		// Gestion des rebonds
		if(collision.gameObject.layer == groundLayer){
			if(bounced >= bounces){
				Explode();
			}
			bounced++;
		} 
	}

	public void Launch(Vector2 dir){
		direction = dir;
		direction += new Vector2(Random.value * spread - spread / 2, Random.value * spread - spread / 2);
		rigidbody2D.velocity = direction.normalized * speed;
	}

	public void Launch(Vector2 dir, PlayerPoints points){
		this.points = points;
		Launch(dir);
	}

	public void Explode(){
		Destroy(gameObject);
	}

	public void SetColor(BulletColor col){
		color = col;
		switch(col){
			case BulletColor.Red: spriteRenderer.sprite = red; break;
			case BulletColor.Green: spriteRenderer.sprite = green; break;
			case BulletColor.Blue: spriteRenderer.sprite = blue; break;
		}
	}
}
