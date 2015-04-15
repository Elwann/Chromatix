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

	private Rigidbody2D rigidbody2d;
	private int bounced = 0;
	private int groundLayer;

	void Awake(){
		groundLayer = LayerMask.NameToLayer("Ground");
		rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		float rotation = Vector2.Angle(rigidbody2d.velocity, -Vector2.up);
		if(rigidbody2d.velocity.x < 0){
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
		rigidbody2d.velocity = direction.normalized * speed;
	}

	public void Launch(Vector2 dir, PlayerPoints points){
		this.points = points;
		Launch(dir);
	}

	public void Explode(){
		float rotation = Vector2.Angle(rigidbody2d.velocity, -Vector2.up);
		if(rigidbody2d.velocity.x < 0){ rotation = -rotation; }
		GameObject go = Instantiate(GameManager.Instance.explosion, transform.position, Quaternion.AngleAxis(30, Vector3.forward)) as GameObject;
		SpriteRenderer sprite = go.GetComponentInChildren<SpriteRenderer>();

		switch(color){
			case BulletColor.Red: sprite.color = new Color(255f,0f,0f); break;
			case BulletColor.Green: sprite.color = new Color(0f,255f,0f); break;
			case BulletColor.Blue: sprite.color = new Color(0f,0f,255f); break;
		}
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
