using UnityEngine;
using System.Collections;

public class Stages : MonoBehaviour {
	private GameManager score;
	private BoxCollider2D box;
	private scoreTransformer scoreTrans;
	private Transform player;

	public AudioClip stageupSound;

	void Start()
	{
		score=GameObject.Find("Canvas").GetComponent<GameManager>();
		box=GetComponent<BoxCollider2D>();
		scoreTrans=GameObject.Find("score100(Clone)").GetComponent<scoreTransformer>();
		player=GameObject.Find ("Player").transform;

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag!="Player")
			return;
		score.score+=300;
		for(int i=0;i<3;i++)
		scoreTrans.AddScore100(player.position);

		box.enabled=false;
		SoundManager.instance.PlaySingle(stageupSound);

		StartCoroutine (EnableCollider());

	}
	IEnumerator EnableCollider()
	{
		yield return new WaitForSeconds(30f);
		box.enabled=true;

	}
	

}
