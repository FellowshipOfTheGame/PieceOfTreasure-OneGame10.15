using UnityEngine;
using System.Collections;

public class Player_BHV : GridNavigator_BHV {

    public float movementThreshold = 0.8f;
	
	protected override void Update () {
		base.Update ();
        /*
         * Player Movement Handling:
         */
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float inputIntensity = 0f;
        Direction inputDirection = Direction.RIGHT;

        //Define input direction and input intensity from vertical and horizontal input axis
        if (Mathf.Abs(horizontalInput) >= Mathf.Abs(verticalInput)) {
            if (horizontalInput > 0) {
                inputDirection = Direction.RIGHT;
                inputIntensity = Mathf.Abs(horizontalInput);
            }
            else if (horizontalInput < 0) {
                inputDirection = Direction.LEFT;
                inputIntensity = Mathf.Abs(horizontalInput);
            }
        }
        else {
            if (verticalInput > 0) {
                inputDirection = Direction.UP;
                inputIntensity = Mathf.Abs(verticalInput);
            }
            else if (verticalInput < 0) {
                inputDirection = Direction.DOWN;
                inputIntensity = Mathf.Abs(verticalInput);
            }
        }



        //Moves to input Direction
        if (inputIntensity >= movementThreshold) {
            MoveDirection(inputDirection);
        }

        //Looks to input direction
        if ((inputIntensity > 0) && (!isCurrentlyMoving)) {
            LookDirection = inputDirection;
        }


        /*
         * Any other stuff a player does:
         */

	}
}