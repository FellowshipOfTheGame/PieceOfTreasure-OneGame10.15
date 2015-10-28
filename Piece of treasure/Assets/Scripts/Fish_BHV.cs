using UnityEngine;
using System.Collections;

public class Fish_BHV : GridNavigator_BHV {

    void Update() {
        bool loopFlag = true;
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
