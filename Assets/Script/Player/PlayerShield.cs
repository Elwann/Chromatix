using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour {

	public PlayerDefense playerDefense;
	private int bulletLayer;
	
	void Awake()
	{
		bulletLayer = LayerMask.NameToLayer("Bullet");
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.gameObject.layer == bulletLayer){
			ProjectileMovement bullet = collision.collider.gameObject.GetComponent<ProjectileMovement>();
			playerDefense.GetHit(bullet);
		}
	}
}
