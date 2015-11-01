using UnityEngine;
using System.Collections;
using System;
using UnityEditor;


[ExecuteInEditMode]
public class GameMapController : MonoBehaviour {

	//Camera tem 31x23 tiles
	[SerializeField]
	public int width;
	[SerializeField]
	public int height;
//	[SerializeField]
//	private layerCount; // pra quando puder escolher o numero de layers (a lista mapLayers deve sumir)
	public GameObject tilePrefab;
	public MapLayer[] mapLayers;
	public GameObject grid;
	public GameObject gridFrame;

	public bool showGrid = true;


	// Use this for initialization
	void Start () {
		PrefabUtility.DisconnectPrefabInstance (gameObject);
		//Proteje a grid de ediçoes
		for (int i = 0; i < grid.transform.childCount; i++){
			grid.transform.GetChild(i).gameObject.hideFlags = HideFlags.HideInHierarchy;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void lockLayer(MapLayer mapLayer){
		GameObject child;
		for (int i = 0; i < mapLayer.gameObject.transform.childCount; i++){
			child = mapLayer.gameObject.transform.GetChild(i).gameObject;
			child.hideFlags = HideFlags.HideInHierarchy;
		}
	}

	void unlockLayer(MapLayer mapLayer){
		GameObject child;
		for (int i = 0; i < mapLayer.gameObject.transform.childCount; i++){
			child = mapLayer.gameObject.transform.GetChild(i).gameObject;
			child.hideFlags = 0;
			child.transform.hideFlags = HideFlags.HideInHierarchy; //Provisorio para teste
		}
	}

	public void hideChildren(bool hide){
		if(hide){
			for(int l = 0; l < mapLayers.Length; l++){
				mapLayers[l].transform.hideFlags = HideFlags.HideInHierarchy;
			}
			grid.transform.hideFlags = HideFlags.HideInHierarchy;
		}
		else {
			for(int l = 0; l < mapLayers.Length; l++){
				mapLayers[l].transform.hideFlags = HideFlags.None;
			}
			grid.transform.hideFlags = HideFlags.None;
		}
	}

	public void setWidth(int w){
		width = w;
	}

	public void setHeight(int h){
		height = h;
	}

	public void setLayerCount(int l){

	}

	public void createMap(string name, int w, int h, int lc){
		gameObject.name = name;
		width = w;
		height = h;
		//layerCount = lc;

		//Cria os layers do mapa
		for (int l = 0; l < mapLayers.Length; l++){
			//if(mapLayers[l].transform.childCount == 0)
			mapLayers[l].createLayer(width, height, tilePrefab, l);
		}
		createGrid ();
	}

	public void createGrid(){
		//Cria a grid acima dos layers
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				//Instancia tiles(?)
				GameObject obj;
				//Instancia grid frames
				obj = (GameObject) Instantiate (gridFrame, new Vector2(i, j), Quaternion.identity);
				obj.transform.parent = grid.transform;
				obj.hideFlags = HideFlags.HideInHierarchy;
			}
		}
	}

	public void chooseActiveLayer(int l){
		for (int i = 0; i < mapLayers.Length; i++) {
			if (i == l){
				unlockLayer(mapLayers[i]);
			}
			else{
				lockLayer(mapLayers[i]);
			}
		}
	}


	//-------------------------------------------------------------------------------------//
	// Metodos para resposta ao Grid Navigator

	public bool CanMove(GridNavigator_BHV navigator, Vector2 targetPos){ //refazer o metodo
		Tile originTile = LogicTile(navigator.GridPosition);
		Tile targetTile = LogicTile(targetPos);
		if (!IsWalkable (targetTile)) {
			return false;
		} else {
			//Is walkable - make borders?
			return !doTilesCreateBorder(originTile, targetTile);
		}
	}


	Tile LogicTile (Vector2 position){
		Tile logicTile = null;
		for (int l = 0; l < mapLayers.Length; l++) {
			Tile tile = mapLayers [l].getTile ((int)position.x, (int)position.y);
			if(tile != null) {
				if (tile.terrain != null){
					if (tile.terrain.getTileSet () != null) {
						//The Tile's terrain tileset exists - there is a terrain
						logicTile = tile;
						logicTile.layerNumber = l;
					}
				}
			}
		}
		return logicTile;
	}

	bool IsWalkable(Tile tile){
		//Tile tile = LogicTile (tilePos);
		if (tile == null)
			return false;
		return tile.terrain.IsWalkable ();
	}

	bool doTilesCreateBorder(Tile tileOrig, Tile tileDest){
		if (!tileOrig.terrain.HasClosedBorders () && !tileDest.terrain.HasClosedBorders ()) {
			// ambos tem bordas abertas - nao criam borda
			return false;
		} else if (tileOrig.terrain == tileDest.terrain && tileOrig.layerNumber == tileDest.layerNumber) {
			// tem bordas fechadas mas sao do mesmo tipo e no mesmo layer - nao cria borda
			return false;
		} else {
			return true;
		}
	}
}

