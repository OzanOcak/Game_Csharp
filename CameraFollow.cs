using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform player;
	public float smoothTime;
	public Vector2 velocity;


	public float back=0.2f;
	public float up=0.48f;
	
	public bool isFollowing{get;set;}
	private Controller2D controller;

	private float shakeIntensity;
	private float shakeOff;
	private float shakeTime;
	
	void Awake()
	{
		player = GameObject.Find("Player").transform;
		controller=GameObject.Find ("Player").GetComponent<Controller2D>();
	}

	private void Start()
	{
		isFollowing=true; ;
	}
	void Update()
	{

		if(velocity.x<-10.5f){
			isFollowing=false;
		}else if(velocity.x>-10.5f){
			isFollowing=true;
			if (velocity.y<-10){
				isFollowing=false;
				if(controller.state.isCollidingBelow==true)
					isFollowing=true;
			}
		}
	}
	
	void FixedUpdate () 
	{
		var newPositionX=Mathf.SmoothDamp(transform.position.x+back,player.transform.position.x,ref velocity.x,smoothTime);
		var newPositionY= Mathf.SmoothDamp(transform.position.y+up,player.transform.position.y,ref velocity.y,smoothTime);

		if (isFollowing)
		{
			
			transform.position= new Vector3(newPositionX,newPositionY,transform.position.z);
		}

	}

	void LateUpdate()
	{
		Vector3 shakeFactorPosition=new Vector3(0,0,0);

		if(shakeTime>0)
		{
			shakeFactorPosition=Random.insideUnitSphere*shakeIntensity*shakeTime;
			shakeTime-=shakeOff*Time.deltaTime;
		}
		transform.position+=shakeFactorPosition;
	}
	public void Shake(Vector3 shakeParameters)
	{
		shakeIntensity=shakeParameters.x;
		shakeTime=shakeParameters.y;
		shakeOff=shakeParameters.z;
	}
}

