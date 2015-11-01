using UnityEngine;
using System.Collections;
using System;


[ExecuteInEditMode]
public class MapLayer : MonoBehaviour {

	[SerializeField]
	private int width;
	[SerializeField]
	private int height;
	private Tile[,] tiles;


	// Use this for initialization
	void Start () {
		if (tiles == null) {
			reaquireLayerInfo();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (tiles == null) {
			reaquireLayerInfo();
		}
	}

	public void reaquireLayerInfo(){
		tiles = new Tile[width,height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				tiles [i, j] = transform.GetChild (j + i * height).GetComponent<Tile>();
			}
		}
	}
	
	public int getWidth(){
		return width;
	}

	public int getHeight(){
		return height;
	}

	public void createLayer(int w, int h, GameObject tilePrefab, int order){
		width = w;
		height = h;
		//Aloca memoria para os tiles
		tiles = new Tile[w,h];

		GameObject obj;
		for (int i = 0; i < w; i++) {
			for (int j = 0; j < h; j++) {
				obj = (GameObject) Instantiate (tilePrefab, new Vector2(i, j), Quaternion.identity);
				obj.transform.parent = transform;
				//Ajusta a ordem dos quatro componentes de sprite do tile - tambem pega o renderer do pai
				for (int c = 1; c <= 4; c++){
					obj.GetComponentsInChildren<SpriteRenderer>()[c].sortingOrder = order;
				}
				//Debug.Log(i + " " + j + " " + tiles[i,j]);
				tiles[i,j] = obj.GetComponent<Tile>();
			}
		}
	}

	public Tile[,] getTiles(){
		return tiles;
	}

	public Tile getTile(int x, int y){
		if (x >= width || y >= height || x < 0 || y < 0) {
			return null;
		}
		return getTiles()[(int)x, y];
	}

}
