using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {
	public GameObject explosion;

	public PlayerHit hit;
	public Animator animator;

	public PlayerPoints points;

	public bool dead = false;

	void Start ()
	{
		points = gameObject.GetComponent<PlayerPoints>();
	}

	public void TakeDamage(ProjectileMovement bullet){
		hit.gameObject.SetActive(false);

		dead = true;

		animator.SetFloat("Speed", 0);
		animator.SetBool("Dead", true);

		if(bullet.points.tableId == points.tableId){
			GameManager.Instance.RemovePoint(points.tableId);
		} else {
			GameManager.Instance.AddPoint(bullet.points.tableId);
		}

		GameManager.Instance.shake.ShakeCamera(4f, 7f, new Vector3());

		Invoke ("Explode", 2f);
	}

	public void Explode(){
		gameObject.SetActive(false);
		GameManager.Instance.shake.ShakeCamera(8f, 7f, new Vector3());
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
		GameManager.Instance.Spawn(points.tableId);
		GameManager.Instance.shake.ShakeCamera(1f, 7f, new Vector3());
		Destroy(gameObject);
	}

	public bool IsDead(){
		return dead;
	}
}
