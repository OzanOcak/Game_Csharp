using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {


	//public float repeatDamagePeriod=2f;
	//public AudioClip[] ouchSounds;
	//public float hurtForce=10f;
	//public float damageAmount=10f;
	SpriteRenderer healthBar;
	//private float lastHitTime;
	private Vector3 healthScale;
	private Player player;

	void Start () 
	{
		healthBar=transform.GetComponent<SpriteRenderer>();
		healthScale=healthBar.transform.localScale;
		player=GameObject.Find ("Player").GetComponent<Player>();
	}
	void Update()
	{
		if(player.health>=0)
		UpdateHealthBar();
	}
	
	public void UpdateHealthBar()
	{
		healthBar.material.color=Color.Lerp (Color.green,Color.red,1-player.health*0.01f);
		healthBar.transform.localScale=new Vector3(healthScale.x*player.health*0.01f,transform.localScale.y,transform.localScale.z);
	}
}
