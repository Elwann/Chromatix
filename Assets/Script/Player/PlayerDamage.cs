using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {
	public GameObject explosion;

	public PlayerHit hit;
	public Animator animator;

	public PlayerPoints points;

	public bool dead = false;
	//private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start ()
	{
		points = gameObject.GetComponent<PlayerPoints>();
		//rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		//Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
	}

	public void TakeDamage(ProjectileMovement bullet){
		hit.gameObject.SetActive(false);

		dead = true;
/*
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		if (transform.localScale.x > 0) {
			pos.x += 0.5f;
		} else {
			pos.x -= 0.5f;
		}

		GameManager.Instance.FocusUI(Camera.main.WorldToScreenPoint(pos).x);
*/
		animator.SetFloat("Speed", 0);
		animator.SetBool("Dead", true);

		if(bullet.points.tableId == points.tableId){
			GameManager.Instance.RemovePoint(points.tableId);
		} else {
			GameManager.Instance.AddPoint(bullet.points.tableId);
		}

		GameManager.Instance.shake.ShakeCamera(1f, 7f, new Vector3());

		Invoke ("Explode", 2f);
	}

	public void Explode(){
		gameObject.SetActive(false);
		GameManager.Instance.shake.ShakeCamera(4f, 7f, new Vector3());
		Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
		if (transform.localScale.x > 0) {
			pos.x += 0.5f;
		} else {
			pos.x -= 0.5f;
		}
		GameObject go = Instantiate(explosion, pos, new Quaternion()) as GameObject;
		go.transform.localScale = transform.localScale;
		Invoke("Respawn", 3f);
	}

	public void Respawn(){
		//GameManager.Instance.UnFocusUI();
		GameManager.Instance.Spawn(points.tableId);
		GameManager.Instance.shake.ShakeCamera(1f, 7f, new Vector3());
		Destroy(gameObject);
	}

	public bool IsDead(){
		return dead;
	}
}
