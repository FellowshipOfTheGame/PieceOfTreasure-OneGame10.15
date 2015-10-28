using UnityEngine;
using System.Collections;

public class Player_BHV : GridNavigator_BHV {

    public float movementThreshold = 0.8f;
	
	void Update () {
        /*
         * Player Movement Handling:
         */
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        //Looks to input direction
        if (Mathf.Abs(horizontalInput) >= Mathf.Abs(verticalInput)) {
            if (horizontalInput > 0) {
                lookDirection = Direction.RIGHT;
            }
            else if (horizontalInput < 0) {
                lookDirection = Direction.LEFT;
            }
        }
        else {
            if (verticalInput > 0) {
                lookDirection = Direction.UP;
            }
            else if (verticalInput < 0) {
                lookDirection = Direction.DOWN;
            }
        }


        //Moves to input Direction
        if (horizontalInput >= movementThreshold) {
            MoveDirection(Direction.RIGHT);
        }
        else if (horizontalInput <= -movementThreshold) {
            MoveDirection(Direction.LEFT);
        }

        if (verticalInput >= movementThreshold) {
            MoveDirection(Direction.UP);
        }
        else if (verticalInput <= -movementThreshold) {
            MoveDirection(Direction.DOWN);
        }

        /*
         * Any other stuff a player does:
         */

	}
}