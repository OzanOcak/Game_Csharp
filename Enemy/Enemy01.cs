using UnityEngine;
using System.Collections;

public class Enemy01 : Enemi {

	private int damage=10;

	void  Start () 
	{
		base.Start();
	}
	protected override void  Update () 
	{
		base.Update();
	}
	protected override void  FixedUpdate () 
	{
		base.FixedUpdate();
	}
	protected override void VerticalMovement()
	{
		base.VerticalMovement();
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.tag=="Player")
		{
			if(player.transform.position.y>=transform.position.y+1.2f)
			{
				StartCoroutine(DeadTranstion());
				
			}else {
				player.TakeDamage(damage);

			}
		}
	}
	protected virtual IEnumerator DeadTransition()
	{
		yield return StartCoroutine(base.DeadTranstion());
	}
}
