using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {
	public PlayerPoints points;
	public Animator animator;

	public bool dead = false;
	private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start ()
	{
		points = gameObject.GetComponent<PlayerPoints>();
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		//Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
	}

	public void TakeDamage(ProjectileMovement bullet){
		if(!dead) GameManager.Instance.Spawn(points.tableId);
		
		dead = true;
		//Time.timeScale = 0.1f;
		
		float dir = transform.localScale.x;
		
		//rigidbody2D.gravityScale = 1f;
		//rigidbody2D.fixedAngle = false;
		//rigidbody2D.angularVelocity = 30f*dir;
		//rigidbody2D.AddForce(new Vector2(0.1f*dir, 0.3f));
		//rigidbody2D.AddTorque(30f * dir);
		
		//boxCollider.enabled = false;
		//rigidbody2D.velocity = Vector2.zero;


		animator.SetFloat("Speed", 0);
		animator.SetBool("Dead", true);

		if(bullet.points.tableId == points.tableId){
			GameManager.Instance.RemovePoint(points.tableId);
		} else {
			GameManager.Instance.AddPoint(bullet.points.tableId);
		}

		GameManager.Instance.shake.ShakeCamera(1f, 7f, new Vector3());
	}

	public bool IsDead(){
		return dead;
	}
}
