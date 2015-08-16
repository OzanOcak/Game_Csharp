using UnityEngine;
using System.Collections;

public class ControllerState2D  
{
	public bool isCollidingAbove{get;set;}
	public bool isCollidingBelow{get;set;}
	public bool isCollidingRight{get;set;}
	public bool isCollidingLeft{get;set;}
	public bool isClimbingSlope{get;set;}
	public bool isDescendingSlope{get;set;}

	public bool handleCollision{get{return isCollidingAbove || isCollidingBelow || isCollidingLeft || isCollidingRight;}set{}}

	public bool isGrounded{get{return isCollidingBelow;}}
	public bool isCrouching{get;set;}
	public bool isAttacking{get;set;}

	public float slopeAngle,slopeAngleOld;

	public void Reset()
	{
		isCollidingAbove=
			isCollidingBelow=
			isCollidingLeft=
			isCollidingRight=
			isClimbingSlope=
			isDescendingSlope=false;

		slopeAngleOld=slopeAngle;
		slopeAngle=0;
	}
}


