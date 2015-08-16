using UnityEngine;
using System.Collections;

public abstract class Enemi : MonoBehaviour {
	
	public float moveSpeed=2;
	public int HP=2;
	public Sprite normalEnemy;
	public Sprite deadEnemy;
	public Sprite damageEnemy;
	//public GameObject hundredPointsUI;
	public bool groundHit=false;
	public Vector3 direction;
	
	protected float gravity=-4f;
	protected SpriteRenderer renderer;
	protected Controller2D controller;
	protected Transform groundCheck;
	protected bool dead=false;
	//private bool notHitted=true;
	protected Vector3 velocity;
	protected Camera camera;
	protected Player player;
	protected GameManager score;
	protected scoreTransformer scoreTrans;
	//private LevelCreator level;
	//private HealthBar hBar;
	public AudioClip hurt1;
	public AudioClip hurt2;

	protected void Awake(){
		score=GameObject.Find ("Canvas").GetComponent<GameManager>();
	}
	
	protected void Start()
	{
		renderer=transform.Find("body").GetComponent<SpriteRenderer>();
		controller=transform.GetComponent<Controller2D>();
		direction=new Vector2(1,0);
		groundCheck=transform.Find("groundCheck").transform;
		camera=GameObject.Find("MainCamera").GetComponent<Camera>();
		player=GameObject.Find("Player").GetComponent<Player>();
		scoreTrans=GameObject.Find("score100(Clone)").GetComponent<scoreTransformer>();

		//level= GameObject.Find ("MainCamera").GetComponent<LevelCreator>();
		//hBar=GameObject.Find ("MainCamera").transform.GetComponentInChildren("HealthBar").transform.GetComponent<HealthBar>();
		
	}

	protected virtual void Update()
	{
		if (((direction.x<0 && controller.state.isCollidingLeft)||(direction.x>0 && controller.state.isCollidingRight))|| !groundHit )
		{
			direction = - direction;
			transform.localScale=new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
		}
		controller.SetHorizontalForce(direction.x*moveSpeed);// use it as trick later put in back fixedUpdate
	}
	
	
	protected virtual void FixedUpdate () 
	{
		
		groundHit=Physics2D.Linecast(transform.position,groundCheck.position,1<<LayerMask.NameToLayer("Ground"));
		Debug.DrawLine(transform.position,groundCheck.position,Color.blue);
		
		
		VerticalMovement();
	}

	
	protected virtual void VerticalMovement()
	{
		
		if(controller.state.isCollidingAbove || controller.state.isCollidingBelow)
			velocity.y=0;
		
		velocity.y+=gravity*Time.deltaTime;
		controller.SetVerticalForce(velocity.y);
		
	}
//	void OnTriggerEnter2D(Collider2D other)
//	{
//
//	}
	protected virtual IEnumerator DeadTranstion()
	{
		if(controller.velocity.x != 0) 
		{
			moveSpeed=0f;
			SoundManager.instance.RandomizeSfx (hurt1,hurt2);
		//	Collider2D col=GetComponent<Collider2D>();
			renderer.enabled=true;
			renderer.sprite=deadEnemy;
			Vector3 shakeParameters=new Vector3(0.5f,0.5f,0.5f);
			camera.SendMessage("Shake",shakeParameters);
			Vector2 enemyPos=transform.position;
			//string type="s100";
			scoreTrans.AddScore100(enemyPos);
			score.score+=100;
			yield return new WaitForSeconds(10f);
			moveSpeed=4f;
			//renderer.sprite=enemy;
			velocity.y=0;
			renderer.sprite=normalEnemy;
		}	
	}
}

