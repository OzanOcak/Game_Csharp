using UnityEngine;
using System.Collections;

public class GoldScript : MonoBehaviour {
	
	private float maxY;
	private float minY;
	private int direction = 1;

	public bool inPlay=true;
	private bool releaseCreate=false;
	private GameManager score;
	private scoreTransformer scoreTransform;

	public AudioClip gold1;
	public AudioClip gold2;

	void Start () {
		score=GameObject.Find("Canvas").GetComponent<GameManager>();
		scoreTransform=GameObject.Find("score100(Clone)").GetComponent<scoreTransformer>();

		maxY = this.transform.position.y + .5f;
		minY = maxY - 1.0f;

	}

	void Update () {
		
		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y +(direction * 0.05f));
		if (this.transform.position.y > maxY)
			direction = -1;
		if (this.transform.position.y < minY)
			direction = 1;
		if(!inPlay && !releaseCreate)
			Respawn();
		
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		
		if (coll.gameObject.tag == "Player") 
		{
			SoundManager.instance.RandomizeSfx (gold1,gold2);
			Vector2 goldPos=transform.position;
			//string type="s100";
			scoreTransform.AddScore100(goldPos);
			inPlay=false;
			this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 30.0f);
			score.score+=100;

		}
	}
	void Respawn()
	{
		releaseCreate=true;
		Invoke("PlaceGold",(float)Random.Range(1,5));
	}


	void PlaceGold()
	{
		inPlay=true;
		releaseCreate=false;

		if(scoreTransform.level!=null){
			GameObject tmpTile=GameObject.Find ("MainCamera").GetComponent<LevelCreator>().tilePos;
			this.transform.position=new Vector2(tmpTile.transform.position.x,tmpTile.transform.position.y+3.5f);
		}

		maxY = this.transform.position.y + .5f;
		minY = maxY - 1.0f;
	}
}