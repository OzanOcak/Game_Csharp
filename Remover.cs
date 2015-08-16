using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag=="Player")
		{
			//Destroy (other.gameObject);
			StartCoroutine(Gameover());
		}
	}
	public IEnumerator Gameover()
	{
		yield return new WaitForSeconds(1f);
		Application.LoadLevel("GameOver");
	}

}
