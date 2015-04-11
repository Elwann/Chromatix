using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

	private bool dead = false;
	private int bulletLayer;

	private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start ()
	{
		bulletLayer = LayerMask.NameToLayer("Bullet");
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.layer == bulletLayer){
			dead = true;
			rigidbody2D.gravityScale = 1;
			rigidbody2D.fixedAngle = false;
			rigidbody2D.angularVelocity = 30f;
			rigidbody2D.AddForceAtPosition(new Vector2(Random.value * 20f - 10f, Random.value * 50f), new Vector2(transform.position.x + 0f, transform.position.y + 0.5f));
		}
	}

	public bool IsDead(){
		return dead;
	}
}
