using UnityEngine;
using System.Collections;

public class Skeleton_BHV : PlayerChasing_BHV {

    protected override void Update() {
        if (!isChasingPlayer) {
            bool canMove = true/*gridMapReference.CanMoveTo(this,lookDirection)*/;
            if (canMove) {
                MoveDirection(LookDirection);
            }
            else {
                LookDirection = (Direction)  (10 - (int)LookDirection);
            }
        }
    }

}