using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour {

	public PlayerDamage playerDamage;
	private int bulletLayer;

	void Awake()
	{
		bulletLayer = LayerMask.NameToLayer("Bullet");
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(!playerDamage.IsDead()){
			if(collision.gameObject.layer == bulletLayer){
				playerDamage.TakeDamage(collision.gameObject.GetComponent<ProjectileMovement>() as ProjectileMovement);
				Destroy(collision.gameObject);
			}
		}
	}
}
