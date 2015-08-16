using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour 
{
	public LayerMask collisionMask;


	const float skinWidth= .015f;
	public int horizontalRayCount=4;
	public int verticalRayCount=4;

	float horizontalRaySpacing;
	float verticalRaySpacing;
	public ControllerState2D state;

	BoxCollider2D collider;
	RaycastOrigins raycastOrigins;
	float maxSlopeAngle=70;
	float maxDescendAngle=75;
	public Vector2 velocity { get{ return _velocity; } }
	private Vector2 _velocity;

	void Awake()
	{
		state=new ControllerState2D();// je dois instantiate state :)
		collider=GetComponent<BoxCollider2D>();
		CalculateRaySpacing();
	}

	public void AddForce(Vector2 force)
	{
		_velocity+=force;
	}
	public void SetForce(Vector2 force)
	{
		_velocity=force;
	}
	public void SetVerticalForce(float y)
	{
		_velocity.y=y;
	}
	public void SetHorizontalForce(float x)
	{
		_velocity.x=x;
	}
	public void LateUpdate()
	{
		Move (velocity*Time.deltaTime);
	}
	

	public void Move(Vector3 velocity)
	{
		UpdateRaycastOrigins();
		state.Reset(); // avoid of accumulating

		if(velocity.y<0)
			DescendSlope(ref velocity);
		if(velocity.x !=0)
			HorizontalCollisions(ref velocity);
		if(velocity.y !=0)
		    VerticalCollisions(ref velocity);

		transform.Translate (velocity);
	}

	void HorizontalCollisions(ref Vector3 velocity)
	{
		float directionX=Mathf.Sign (velocity.x);
		float rayLength=Mathf.Abs (velocity.x)+skinWidth;
		
		for(int i=0;i<horizontalRayCount;i++)
		{
			Vector2 rayOrigin=(directionX==-1) ? raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
		    RaycastHit2D hit=Physics2D.Raycast (rayOrigin,Vector2.right*directionX,rayLength,collisionMask);


			Debug.DrawRay(rayOrigin,Vector2.right * directionX*rayLength,Color.red);
			if(hit)
			{
				float slopeAngle=Vector2.Angle (hit.normal,Vector2.up);
				if(i==0 && slopeAngle <= maxSlopeAngle)
					ClimbSlope(ref velocity,slopeAngle);
	
				velocity.x=(hit.distance-skinWidth)*directionX;
				rayLength=hit.distance;

				state.isCollidingLeft= directionX==-1; // if we hit sthg & direction -1 (left).....collision.left is true
				state.isCollidingRight=directionX==1;
				
			}
		}
	}

	void VerticalCollisions(ref Vector3 velocity)
	{
		float directionY=Mathf.Sign (velocity.y);
		float rayLength=Mathf.Abs (velocity.y)+skinWidth;

		for(int i=0;i<verticalRayCount;i++)
		{
			Vector2 rayOrigin=(directionY==-1) ? raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i+ velocity.x);
			RaycastHit2D hit=Physics2D.Raycast (rayOrigin,Vector2.up*directionY,rayLength,collisionMask);

			Debug.DrawRay(rayOrigin,Vector2.up*directionY*rayLength,Color.red);

			if(hit)
			{
				velocity.y=(hit.distance-skinWidth)*directionY;
				rayLength=hit.distance;

				state.isCollidingAbove=directionY==1;
				state.isCollidingBelow=directionY==-1;
			}
		}
	}
	void ClimbSlope(ref Vector3 velocity,float slopeAngle)
	{
		float moveDistance=Mathf.Abs (velocity.x);
		float climbVelocityY=Mathf.Sin(slopeAngle*Mathf.Deg2Rad)*moveDistance;

		if(velocity.y<=climbVelocityY)
		{
			velocity.y=climbVelocityY;
			velocity.x=Mathf.Cos(slopeAngle*Mathf.Deg2Rad*moveDistance*Mathf.Sign(velocity.x));
			state.isCollidingBelow=true;
			state.isClimbingSlope=true;
			state.slopeAngle=slopeAngle;
		}

	}
	void DescendSlope(ref Vector3 velocity)
	{
		float directionX=Mathf.Sign (velocity.x);
		float rayLength=Mathf.Abs (velocity.y)+skinWidth;
		Vector2 rayOrigin=(directionX==-1)? raycastOrigins.bottomRight:raycastOrigins.bottomLeft;
		RaycastHit2D hit =Physics2D.Raycast(rayOrigin,-Vector2.up,rayLength,collisionMask);
		if(hit)
		{
			float slopeAngle=Vector2.Angle(hit.normal,Vector2.up);
			if(slopeAngle != 0 && slopeAngle<=maxDescendAngle){
				if(Mathf.Sign (hit.normal.x)==directionX){
					float moveDistance=Mathf.Abs(velocity.x);
					float descendVelocityY=Mathf.Sign (slopeAngle*Mathf.Deg2Rad)*moveDistance;
					velocity.x=Mathf.Cos (slopeAngle*Mathf.Deg2Rad)*moveDistance*Mathf.Sign (velocity.x);
					velocity.y-= descendVelocityY;

					state.slopeAngle=slopeAngle;
					state.isCollidingBelow=true;
					state.isDescendingSlope=true;
				}
			}
		}
	}

	void UpdateRaycastOrigins()
	{
		Bounds bounds=collider.bounds;
		bounds.Expand(skinWidth*-2);

		raycastOrigins.bottomLeft=new Vector2(bounds.min.x,bounds.min.y);
		raycastOrigins.bottomRight=new Vector2(bounds.max.x,bounds.min.y);
		raycastOrigins.topLeft=new Vector2(bounds.min.x,bounds.max.y);
		raycastOrigins.topRight=new Vector2(bounds.max.x,bounds.max.y);
	}

	void CalculateRaySpacing()
	{
		Bounds bounds=collider.bounds;
		bounds.Expand(skinWidth*-2);

		horizontalRayCount=Mathf.Clamp(horizontalRayCount,2,int.MaxValue);
		verticalRayCount=Mathf.Clamp(verticalRayCount,2,int.MaxValue);

		horizontalRaySpacing=bounds.size.y/(horizontalRayCount-1);
		verticalRaySpacing=bounds.size.x/(verticalRayCount-1);
	}

	struct RaycastOrigins
	{
		public Vector2 topLeft,topRight;
		public Vector2 bottomLeft,bottomRight;
	}

}
