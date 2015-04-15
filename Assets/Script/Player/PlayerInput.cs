using UnityEngine;
using System.Collections;

[AddComponentMenu("Chromatix/Player/PlayerInput")]
public class PlayerInput : MonoBehaviour
{
	public string horizontal = "Horizontal";
	public string vertical = "Vertical";

	public string fire = "Fire";
	public string jump = "Jump";

	private PlayerDamage playerDamage;

	void Start()
	{
		playerDamage = gameObject.GetComponent<PlayerDamage>();
	}

	public bool IsFirePressed()
	{
		return (playerDamage != null && !playerDamage.IsDead() && Input.GetButton(fire));
	}
	
	public float Horizontal()
	{
		return (playerDamage != null && !playerDamage.IsDead())? Input.GetAxis(horizontal) : 0;
	}
	
	public float Vertical()
	{
		return (playerDamage != null && !playerDamage.IsDead())? Input.GetAxis(vertical) : 0;
	}
	
	public bool IsJumpPressed()
	{
		return (playerDamage != null && !playerDamage.IsDead() && Input.GetButton(jump));
	}

	public bool IsJumpDown()
	{
		return (playerDamage != null && !playerDamage.IsDead() && Input.GetButtonDown(jump));
	}
}
