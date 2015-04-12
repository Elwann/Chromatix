using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

	public Animator animator;

	private bool dead = false;
	private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start ()
	{
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	public void TakeDamage(){
		dead = true;
		//Time.timeScale = 0.1f;
		
		float dir = transform.localScale.x;
		
		rigidbody2D.gravityScale = 0.3f;
		rigidbody2D.fixedAngle = false;
		//rigidbody2D.angularVelocity = 30f*dir;
		rigidbody2D.AddForce(new Vector2(0.1f*dir, 0.3f));
		//rigidbody2D.AddTorque(30f * dir);
		
		//rigidbody2D.velocity = Vector2.zero;
		
		animator.SetFloat("Speed", 0);
		animator.SetBool("Dead", true);
		GameManager.Instance.shake.ShakeCamera(1f, 7f, new Vector3());
	}

	public bool IsDead(){
		return dead;
	}
}
