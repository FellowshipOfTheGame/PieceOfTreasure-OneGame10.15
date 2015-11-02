using UnityEngine;
using System.Collections;

public class Bat_BHV : PlayerChasing_BHV {

    protected override void Update() {
        base.Update();
        bool loopFlag = true;
        if (!isChasingPlayer) {
            while (loopFlag) {
                int randomNumber = Random.Range(1, 4);
                Direction randomDirection = (Direction)(randomNumber * 2);
                if (true/*gridMapReference.CanMoveTo(randomDirection)*/) {
                    MoveDirection(randomDirection);
                    loopFlag = false;
                }
            }
        }
    }

}
