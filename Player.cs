using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour 
{
	public AudioClip jump1;
	public AudioClip jump2;
	public AudioClip gameoverSound;
	public AudioClip damageSound;

	public float jumpHeight=6;
	public float timeToJumpApex=.4f;
	public float moveSpeed=6;
	public bool isDead{get;set;}

	private Animator anim;
	float accelerationTimeAir=.2f;
	float accelerationTimeGround=.1f;

	public float gravity{get;set;}
//	float jumpVelocity;
	private Vector3 velocity;
	float velocityXSmoothing;

	public Controller2D controller{get;set;}
	public bool isFacingRight=true;
	public int health{get;set;}
	public int maxHealth=100;

	private float horizontalMove;
	private float verticalMove;
	

	private float normalizedSpeed;
	private CameraFollow camera;

	public bool enemyHit=false;
	private Remover re; 
	//private Enemy enemy; //je ne le utilise pas
	private int waterDamage=1;
	private GameManager score;
	private GameManager etape;
	private HealthBar bar;

	void Awake()
	{
		controller=GetComponent<Controller2D>();
		camera=GameObject.Find("MainCamera").GetComponent<CameraFollow>();
		health=maxHealth;
		//enemy=GameObject.Find ("enemy01(Clone)").GetComponent<Enemy>();

		gravity= -(2*jumpHeight)/Mathf.Pow(timeToJumpApex,2);
//		jumpVelocity=Mathf.Abs(gravity)*timeToJumpApex;
		re=GameObject.Find ("Reloader").GetComponent<Remover>();
		score=GameObject.Find("Canvas").GetComponent<GameManager>();
		etape=GameObject.Find ("Canvas").GetComponent<GameManager>();

		bar=GameObject.Find("Canvas").transform.FindChild("UIHealthBar").transform.FindChild("HealthBar").GetComponent<HealthBar>();
	}

	void Start()
	{
		anim=GetComponent<Animator>();
		isFacingRight=transform.localScale.x>0;

		controller.state.isAttacking=false;
		controller.state.isCrouching=false;



	}
	void Update()
	{
		    UpdateAnimator();
		if(!isDead){
		    HorizontalAxis();
		    VerticalAxis();
			StartCoroutine (Jump());
			StartCoroutine (OnCrouch());
			StartCoroutine (OnAttack());

		}else 
			controller.SetHorizontalForce(0);
	}

	public void RespawnAt(Transform spawnPoint)
	{
		if(!isFacingRight) Flip ();
		isDead=false;
		GetComponent<Collider2D>().enabled=true;
		controller.state.handleCollision=true;
		transform.position=spawnPoint.position;
		health=maxHealth;
	}
	public void Kill()
	{
		controller.state.handleCollision=false;
		GetComponent<Collider2D>().enabled=false;
		isDead=true;
	}
	public void FinishLevel(){}

	public void TakeDamage(int damage)//int damage,GameObject obj
	{
		//AudioSource.PlayClipAtPoint();
		    SoundManager.instance.PlaySingle (damageSound);
			StartCoroutine(Flicker());
		
		health-=damage;
		if(health<=0)
		{
			controller.enabled=false;
			isDead=true;
			//controller.SetHorizontalForce(-20);
			SoundManager.instance.PlaySingle (gameoverSound);
			SoundManager.instance.musicSource.Stop ();
			StartCoroutine (re.Gameover());
		}
	}
    IEnumerator Flicker()
	{
//		if(inWater) 
//			return;
		Color tempColor=new Color32 (255,20,20,255);
		for(var n =0; n<10;n++)
		{
			GetComponent<Renderer>().material.color=Color.white;
			yield return new WaitForSeconds(.05f);
			GetComponent<Renderer>().material.color= tempColor;
			yield return new WaitForSeconds(.05f);
		}
		GetComponent<Renderer>().material.color=Color.white;
		Physics2D.IgnoreLayerCollision(8,10,true);
		yield return new WaitForSeconds(.7f);
		Physics2D.IgnoreLayerCollision(8,10,false);

	}
	private void HorizontalAxis()
	{
		horizontalMove=CrossPlatformInputManager.GetAxis("Horizontal");
		//horizontalMove=Input.GetAxis("Horizontal");
		if(horizontalMove>0.1){
			normalizedSpeed=horizontalMove;
			if (!isFacingRight) Flip();
		}

		else if(horizontalMove<-0.1){
			normalizedSpeed=horizontalMove;
			if(isFacingRight) Flip ();
		}
		else
			normalizedSpeed=0;

		float targetVelocity=horizontalMove*moveSpeed;
		velocity.x=Mathf.SmoothDamp (velocity.x,targetVelocity,ref velocityXSmoothing,(controller.state.isCollidingBelow)?accelerationTimeGround:accelerationTimeAir);
		controller.SetHorizontalForce(velocity.x);
	}
	private void VerticalAxis()
	{
		verticalMove=CrossPlatformInputManager.GetAxis("Vertical");
		//verticalMove=Input.GetAxis("Vertical");

		if(controller.state.isCollidingAbove || controller.state.isCollidingBelow)
			velocity.y=0;

		velocity.y+=gravity*Time.deltaTime;
		controller.SetVerticalForce(velocity.y);
	}

	public void Flip()
	{
		transform.localScale=new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
		isFacingRight=transform.localScale.x>0;
	}
	private void UpdateAnimator()
	{
		anim.SetBool("isGrounded",controller.state.isGrounded);
		anim.SetFloat("speed",Mathf.Abs (controller.velocity.x));
		anim.SetFloat("vSpeed",controller.velocity.y);
		anim.SetBool("isCrouching",controller.state.isCrouching);
		anim.SetBool("isAttacking",controller.state.isAttacking);
	}

	IEnumerator OnCrouch()
	{
		if(controller.state.isGrounded && Input.GetKeyDown(KeyCode.DownArrow)){
			controller.state.isCrouching=true;
			camera.up=0.01f;
			camera.smoothTime=2;
		    yield return new WaitForSeconds(.5f);
			camera.up=0.1f;
			camera.smoothTime=.5f;
			controller.state.isCrouching=false;
		}
	}

	IEnumerator OnAttack()
	{
		if(controller.state.isGrounded && Input.GetKeyDown(KeyCode.X)){
		    yield return new WaitForSeconds(0f);
			controller.state.isAttacking=true;
			Vector3 shakeParameters=new Vector3(0.3f,0.3f,0.3f);
			camera.SendMessage("Shake",shakeParameters);
			controller.state.isAttacking=false;
		}

	}

	IEnumerator  Jump()
	{
		if(/*Input.GetKeyDown(KeyCode.UpArrow)  && */controller.state.isGrounded && CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			velocity.y= 4*jumpHeight;
			controller.SetVerticalForce(velocity.y);
			controller.state.isCollidingBelow=false;
			camera.up=0.01f;
			SoundManager.instance.RandomizeSfx (jump1,jump2);
			yield return new WaitForSeconds(1f);
			camera.up=0.1f;

		}
	}

	public void GiveHealth(int healthPoint,GameObject obj)
	{
		Mathf.Min (health+healthPoint,maxHealth);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Water")
		{
		TakeDamage(waterDamage);
		accelerationTimeAir=1f;
		moveSpeed=2f;
		}
		if(other.gameObject.tag=="fini"){
			etape.stage++;

			if(((etape.stage)%10)==0)
			{
				health=maxHealth;
				bar.UpdateHealthBar();
			}

		}

	}
	//private void OnTriggerStay2D(Collider2D other){}
	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag != "Water")
			return;
		accelerationTimeAir=.2f;
		moveSpeed=6f;
	}


}
