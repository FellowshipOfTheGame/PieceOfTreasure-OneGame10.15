using UnityEngine;
using System.Collections;

public class GridEntity_BHV : MonoBehaviour {

    public enum Direction {
        UP = 2,
        RIGHT = 4,
        LEFT = 6,
        DOWN = 8
    }

    public Vector2 ToVector2(Direction direction) {
        return new Vector2(((((int)direction) % 6) / 2) - 1, 1 - ((int)(((int)direction) / 4)));
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
    private Direction lookDirection;
    public Direction LookDirection {
        get {
            return lookDirection;
        }
        set {
            lookDirection = value;
            AnimateTurnDirection(value);
        }
    }

    public float locationDepth = 0.5f;
    public bool isCollider;

    protected Animator animatorReference;


    protected virtual void Start() {
        gridMapReference = GameObject.FindObjectOfType<GameMapController>();
		LookDirection = inicialLookDirection;
        animatorReference = GetComponent<Animator>();
//		  gridPosition = initialGridPosition;
		gridPosition = new Vector2((int)transform.position.x, (int)transform.position.y); //troquei a linha de cima por esta
        transform.position = new Vector3(gridPosition.x,gridPosition.y,-locationDepth);
    }

    protected virtual void AnimateTurnDirection(Direction dir) {
        if (animatorReference != null) {
            animatorReference.SetInteger("Direction", (int)dir);
        }
    }

}