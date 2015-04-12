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
		if(collision.gameObject.layer == bulletLayer){
			Destroy(collision.gameObject);
			playerDamage.TakeDamage();
		}
	}
}
