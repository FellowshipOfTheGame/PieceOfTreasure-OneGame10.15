using UnityEngine;
using System.Collections;

public class PlayerChasing_BHV : GridNavigator_BHV {

    public float chasingTime = 5f;
    public float chaseDelay = 2f;
    protected bool isChasingPlayer = false;
    private float stateTimer = 0f;
    protected Player_BHV playerTarget;

    protected override void Update() {
        base.Update();
        if (stateTimer > 0) {
            stateTimer -= Time.deltaTime;
            if (stateTimer <= 0) {
                if(isChasingPlayer){
                    StopChasing();
                }
            }
        }
        else {
            if(!isChasingPlayer){
                //If isnt chasing and timer is over, then try to find a visible player to chase.
                Player_BHV player = playerInSight();
                if(player != null){
                    StartChasing(player);
                }
            }
        }
        if (isChasingPlayer) {
            Chase();
        }
    }

    private void Chase() {
        Direction dir;
        Vector2 v = playerTarget.GridPosition - this.gridPosition;
        if (v.x > 0) { dir = Direction.RIGHT; }
        else if (v.x < 0) { dir = Direction.LEFT; }
        else if(v.y > 0) { dir = Direction.UP; }
        else if (v.y < 0) { dir = Direction.DOWN; }
        else { return; }
        MoveDirection(dir);
    }

    private void StopChasing() {
        isChasingPlayer = false;
        stateTimer = chaseDelay;
        playerTarget = null;
    }

    private void StartChasing(Player_BHV player) {
        isChasingPlayer = true;
        stateTimer = chasingTime;
        playerTarget = player;
    }

    public Player_BHV playerInSight() {
        Player_BHV[] playerList = FindObjectsOfType<Player_BHV>();
        foreach (Player_BHV p in playerList) {
            /*
            if(gridMapReference.CanThisSeeThat(this, p)){
                return p;
            }
            */
            Vector2 targetPosition = p.GridPosition;
            switch (lookDirection) {
                case Direction.DOWN:
                    if ((targetPosition.x == this.gridPosition.x) && (targetPosition.y < this.gridPosition.y)) {
                        return p;
                    }
                    break;
                case Direction.UP:
                    if ((targetPosition.x == this.gridPosition.x) && (targetPosition.y > this.gridPosition.y)) {
                        return p;
                    }
                    break;
                case Direction.RIGHT:
                    if ((targetPosition.x > this.gridPosition.x) && (targetPosition.y == this.gridPosition.y)) {
                        return p;
                    }
                    break;
                case Direction.LEFT:
                    if ((targetPosition.x < this.gridPosition.x) && (targetPosition.y == this.gridPosition.y)) {
                        return p;
                    }
                    break;
            }
        }
        return null;
    }

}
