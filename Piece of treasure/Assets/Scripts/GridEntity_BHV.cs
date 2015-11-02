using UnityEngine;
using System.Collections;

public class GridEntity_BHV : MonoBehaviour {

    public enum Direction {
        UP = 2,
        RIGHT = 4,
        LEFT = 6,
        DOWN = 8
    }

    protected GameMapController gridMapReference;

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


    protected virtual void Start() {
        gridMapReference = GameObject.FindObjectOfType<GameMapController>();
		lookDirection = inicialLookDirection;
//		  gridPosition = initialGridPosition;
		gridPosition = new Vector2((int)transform.position.x, (int)transform.position.y); //troquei a linha de cima por esta
        transform.position = new Vector3(gridPosition.x,gridPosition.y,-locationDepth);

    }


}