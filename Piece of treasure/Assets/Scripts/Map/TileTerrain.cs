using UnityEngine;
using System.Collections;

public class TileTerrain : MonoBehaviour {

	public enum TerrainType{
		plain,
		closedBorders,
		notPassable,
	}

	public Texture2D tileSet;

	public TerrainType terrainType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Texture2D getTileSet(){
		return tileSet;
	}

	public bool IsWalkable(){
		if (terrainType == TerrainType.plain || terrainType == TerrainType.closedBorders) {
			return true;
		}
		return false;
	}

	public bool HasClosedBorders(){
		if (terrainType == TerrainType.closedBorders){
			return true;
		}
		return false;
	}

}
