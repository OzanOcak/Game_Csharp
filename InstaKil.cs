using UnityEngine;
using System.Collections;

public class InstaKil : MonoBehaviour 
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		var player=other.GetComponent<Player>();
		if(player==null) return;
		print("dead");
		//LevelManager.Instance.KillPlayer();
	}

}
