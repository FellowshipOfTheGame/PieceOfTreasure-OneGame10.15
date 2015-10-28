using UnityEngine;
using System.Collections;

public class GridEntity_BHV : MonoBehaviour {

    public enum Direction {
        UP = 2,
        RIGHT = 4,
        LEFT = 6,
        DOWN = 8
    }

    //protected GameMapController gridMapReference;

    public Vector2 initialGridPosition = new Vector2(0, 0);
    protected Vector2 gridPosition;
    public Vector2 GridPosition {
        get {
            return gridPosition;
        }
    }

    public Direction inicialLookDirection;
    protected Direction lookDirection;

    public float locationDepth = 0.5f;
    public bool isCollider;

    void Start() {
        //gridMapReference = GameObject.FindObjectOfType<GameMapController>();
        gridPosition = initialGridPosition;
        lookDirection = inicialLookDirection;
        transform.position = new Vector3(gridPosition.x,gridPosition.y,locationDepth);
    }

}