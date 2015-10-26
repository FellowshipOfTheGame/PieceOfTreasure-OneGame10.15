using UnityEngine;
using System.Collections;

public class Skeleton_BHV : PlayerChasing_BHV {

    void Update() {
        if (!isChasingPlayer) {
            bool canMove = true/*gridMapReference.CanMoveTo(this,lookDirection)*/;
            if (canMove) {
                MoveDirection(lookDirection);
            }
            else {
                lookDirection = (Direction)  (10 - (int)lookDirection);
            }
        }
    }

}
