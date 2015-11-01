using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Linq;


[ExecuteInEditMode]
public class Tile : MonoBehaviour {

	enum Direction {
		N = 0,
		NE,
		E,
		SE,
		S,
		SW,
		W,
		NW
	}

	//Sprites que, os 4 juntos, formam a unidade da imagem do tile
	public TileSpriteFourth nwSprite;
	public TileSpriteFourth neSprite;
	public TileSpriteFourth seSprite;
	public TileSpriteFourth swSprite;

	public bool passable;

	private int x;
	private int y;

	public TileTerrain terrain;

	private MapLayer mapLayer;

	public int layerNumber = -1; //make it a public get method

	private static string loadedSpriteSheet;
	private static Sprite[] sprites;


	//teste
	//public int index;


	// Use this for initialization
	void Start () {
		x = (int)transform.localPosition.x;
		y = (int)transform.localPosition.y;
		mapLayer = transform.parent.gameObject.GetComponent<MapLayer>();
		//hideChildren ();
	}
	
	// Update is called once per frame
	void Update () {
		//Corrige possiveis erros de referencia do layer aos tiles
		if (transform.parent.gameObject.GetComponent<MapLayer> ().getTiles () == null) {
			transform.parent.gameObject.GetComponent<MapLayer> ().reaquireLayerInfo();
		}

		if (Application.isPlaying)
			return;

		//Para garantir que os tiles nao serao movidos
		transform.localPosition = new Vector2 ((float)x, (float)y);


//		//Teste de seleçao
//		if (Selection.Contains (gameObject)) {
//			//GetComponent<SpriteRenderer>().color =  new Color(122/255.0f, 186/255.0f, 225/255.0f, 155/255.0f);
//		}
//		else {
//			GetComponent<SpriteRenderer>().color =  new Color(1, 1, 1, 1);
//		}


		if(Selection.Contains(this.gameObject) || hasSelectedNeighbor()){
			applyAutoTile ();
		}


	}

	void hideChildren(){
		Debug.Log ("Esconde Fourths");
		nwSprite.gameObject.hideFlags = HideFlags.HideInHierarchy;
		neSprite.gameObject.hideFlags = HideFlags.HideInHierarchy;
		seSprite.gameObject.hideFlags = HideFlags.HideInHierarchy;
		swSprite.gameObject.hideFlags = HideFlags.HideInHierarchy;
	}


	void applyAutoTile(){
		//Carrega todos os sprites da spritesheet do tileset
		if (Selection.Contains (this.gameObject)) {
			if (terrain != null){
				string spriteSheet = AssetDatabase.GetAssetPath (terrain.getTileSet());
				if (spriteSheet != loadedSpriteSheet || sprites.Length < 1) {
					sprites = AssetDatabase.LoadAllAssetsAtPath (spriteSheet).OfType<Sprite> ().ToArray ();
					//sprites = sprites.OrderBy(s => (s.pivot.x - s.pivot.y*3) ).ToArray();
					sprites = sprites.OrderBy(s => s.name ).ToArray();
					sprites = sprites.OrderBy(s => s.name.Length ).ToArray();

					loadedSpriteSheet = spriteSheet;
					Debug.Log ("Tileset loaded:");
					Debug.Log (spriteSheet);
					Debug.Log (loadedSpriteSheet);
					Debug.Log (sprites.Length);
				}
			}
		}
		//tileSet.SetPixels; pode adquirir automaticamente os tiles, sem necessidade de recortar a textura manualmente
		//Refazer o codigo acima para ter menos impacto na performance (carregando do disco constantemente)



		SpriteRenderer sRnw = nwSprite.gameObject.GetComponent<SpriteRenderer> ();
		SpriteRenderer sRne = neSprite.gameObject.GetComponent<SpriteRenderer> ();
		SpriteRenderer sRse = seSprite.gameObject.GetComponent<SpriteRenderer> ();
		SpriteRenderer sRsw = swSprite.gameObject.GetComponent<SpriteRenderer> ();


		if (terrain == null) {
			sRnw.sprite = null;
			sRne.sprite = null;
			sRsw.sprite = null;
			sRse.sprite = null;
			return;
		} else if (terrain.getTileSet () == null) {
			sRnw.sprite = null;
			sRne.sprite = null;
			sRsw.sprite = null;
			sRse.sprite = null;
			return;
		}

		if (AssetDatabase.GetAssetPath (terrain.getTileSet()) != loadedSpriteSheet) {
			return;
		}

		//Debug.Log(Selection.Contains(this.gameObject));
		if (sprites.Length > 0) {

			//sRnw.sprite = sprites[index]; //teste

			//Compreende os dois sprites da parte superior
			if(isSameTile(Direction.N)){
				//Compreende o sprite NE
				if(isSameTile(Direction.E)){
					//Sprite Cheio
					if(isSameTile(Direction.NE)){
						sRne.sprite = sprites[10];
					}
					//Canto Interno
					else{
						sRne.sprite = sprites[4];
					}
				}
				//Lateral direita
				else {
					sRne.sprite = sprites[11];
				}

				//Compreende o sprite NW
				if(isSameTile(Direction.W)){
					//Sprite Cheio
					if(isSameTile(Direction.NW)){
						sRnw.sprite = sprites[10];
					}
					//Canto Interno
					else {
						sRnw.sprite = sprites[5];
					}
				}
				//Lateral esquerda
				else {
					sRnw.sprite = sprites[9];
				}
			}
			else {
				//Compreende o sprite NE
				//Lateral superior
				if(isSameTile(Direction.E)){
					sRne.sprite = sprites[7];
				}
				//Canto Externo
				else {
					sRne.sprite = sprites[8];
				}

				//Compreende o sprite NW
				//Lateral superior
				if(isSameTile(Direction.W)){
					sRnw.sprite = sprites[7];
				}
				//Canto Externo
				else {
					sRnw.sprite = sprites[6];
				}
			}

			//Compreende os dois sprites da parte inferior
			if(isSameTile(Direction.S)){
				//Compreende o sprite SE
				if(isSameTile(Direction.E)){
					//Sprite Cheio
					if(isSameTile(Direction.SE)){
						sRse.sprite = sprites[10];
					}
					//Canto Interno
					else{
						sRse.sprite = sprites[1];
					}
				}
				//Lateral direita
				else {
					sRse.sprite = sprites[11];
				}
				
				//Compreende o sprite SW
				if(isSameTile(Direction.W)){
					//Sprite Cheio
					if(isSameTile(Direction.SW)){
						sRsw.sprite = sprites[10];
					}
					//Canto Interno
					else {
						sRsw.sprite = sprites[2];
					}
				}
				//Lateral esquerda
				else {
					sRsw.sprite = sprites[9];
				}
			}
			else {
				//Compreende o sprite SE
				//Lateral inferior
				if(isSameTile(Direction.E)){
					sRse.sprite = sprites[13];
				}
				//Canto Externo
				else {
					sRse.sprite = sprites[14];
				}
				
				//Compreende o sprite SW
				//Lateral inferior
				if(isSameTile(Direction.W)){
					sRsw.sprite = sprites[13];
				}
				//Canto Externo
				else {
					sRsw.sprite = sprites[12];
				}
			}

		}

	}

	private Tile getTile(Direction dir){
		int i = x, j = y;
		//Separaçao entre norte, sul e centro
		if (dir == Direction.N || dir == Direction.NE || dir == Direction.NW) {
			j = y + 1;
		} else if (dir == Direction.S || dir == Direction.SE || dir == Direction.SW) {
			j = y - 1;
		}
		//Separaçao entre leste, oeste e centro
		if (dir == Direction.W || dir == Direction.SW || dir == Direction.NW) {
			i = x - 1;
		} else if (dir == Direction.E || dir == Direction.SE || dir == Direction.NE){
			i = x + 1;
		}

//		Debug.Log (mapLayer);
//
//		Debug.Log (i + " " + j);
//
//		Debug.Log(mapLayer.getTiles().Length);

		//Debug.Log (mapLayer.getTiles () [0].Length);

		//Fazer tratamento de erro de i e j
		if (i < 0 || j < 0 || i >= mapLayer.getWidth () || j >= mapLayer.getHeight ()) {
			return null;
		}
		//Debug.Log ("i = " + i + "; j = " + j);
		return mapLayer.getTiles() [i,j].GetComponent<Tile>();
	}

	private bool hasSelectedNeighbor(){
		for (Direction d = Direction.N; d <= Direction.NW; d++) {
			Tile candidate = getTile(d);
			if (candidate == null){
				//return false;
			}
			else if (Selection.Contains(candidate.gameObject) ){
				return true;
			}
		}
		return false;
	}
	

	private bool isSameTile(Direction dir){
		if (getTile (dir) == null)
			return true;
		if (terrain == getTile(dir).terrain) {
			return true;
		}
		return false;
	}
	

	public int getX(){
		return x;
	}

	public int getY(){
		return y;
	}

	public void forceUpdate(){
		Update ();
	}

	public MapLayer GetMapLayer(){
		return mapLayer;
	}
	

}
