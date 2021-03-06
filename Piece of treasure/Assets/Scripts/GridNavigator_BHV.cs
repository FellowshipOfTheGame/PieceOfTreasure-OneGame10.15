﻿using UnityEngine;
using System.Collections;

public class GridNavigator_BHV : GridEntity_BHV {
    private Vector2 gridDestPosition;
    public float navigationSpeed = 1f; //tiles per second

    protected bool isCurrentlyMoving;
    private float movementProgression;

    public float stepDelay; //seconds
    private float stepDelayCounter;

    public bool flying;
    public bool swimming;
    public bool walking;

	protected override void Start () {
		base.Start ();
        animatorReference = GetComponent<Animator>();
        isCurrentlyMoving = false;
	}

    protected virtual void Update() {
        if (isCurrentlyMoving) {
            movementProgression += navigationSpeed / 60;
            if (movementProgression >= 1) {
                movementProgression = 1;
                isCurrentlyMoving = false;
                stepDelayCounter = stepDelay;
                gridPosition = gridDestPosition;
                //Stop Walk Animation
                AnimateStopWalking();
                /*
                GridEntity_BHV gridCollider = gridMapReference.CheckCollision(this);
                if(gridCollider != null){
                    ColisionResult(gridCollider);
                }
                */
            }
            //lerp position
            Vector3 origin = new Vector3(gridPosition.x,gridPosition.y,transform.position.z);
            Vector3 destination = new Vector3(gridDestPosition.x, gridDestPosition.y, transform.position.z);
            transform.position = Vector3.Lerp(origin, destination, movementProgression);
        }
    }

    protected void ColisionResult(GridEntity_BHV gridCollider) {
        Direction dir;
        dir = (Direction)(10 - ((int)LookDirection)); 
        MoveDirection(dir);
    }
	
    public bool MoveDirection(Direction direction){
        if (!isCurrentlyMoving) { //Doesn't accept movement commands while moving
            if (stepDelayCounter <= 0) {
                Vector2 destination = gridPosition + ToVector2(direction);
                if (gridMapReference.CanMove(this, destination)) { //Checking map collision
                    //Sets the movement
                    isCurrentlyMoving = true;
                    movementProgression = 0;
                    gridDestPosition = destination;
                    LookDirection = direction;
                    //Start Walking Animation
                    AnimateStartWalking();
                    return true;
                }
            }
            else {
                stepDelayCounter -= Time.deltaTime;
            }
        }
        return false;
    }

    protected virtual void AnimateStartWalking() {
        if (animatorReference != null) {
            animatorReference.SetBool("Walking", true);
        }
    }
    protected virtual void AnimateStopWalking() {
        if (animatorReference != null) {
            animatorReference.SetBool("Walking", false);
        }
    }
}
