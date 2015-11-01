using UnityEngine;
using System.Collections;
using UnityEditor;

public class MapEditorWindow : EditorWindow {

	string mapName = "Map Name";
	string createMapButton = "Create New Map";

	GameMapController activeMap = null;

	int selectedLayer = 0;
	string[] optionsLayer;

	Object[] terrains;


	int mapWidth = 12;
	int mapHeight = 9;
	[Range(1, 3)]
	int layerCount = 3;


//	bool hideChildren = true;




	[MenuItem("Window/Map Editor")]
	static void Init(){
		MapEditorWindow window = (MapEditorWindow)EditorWindow.GetWindow (typeof(MapEditorWindow));
		window.Show ();
	}

	void OnGUI(){

		activeMap = (GameMapController) FindObjectOfType (typeof(GameMapController));
		if (activeMap) {
			displayMapEditorWindow();
		} 
		else {
			displayNewMapWindow ();
		}

	}

	//Criaçao de mapa - Janela
	void displayNewMapWindow(){
		GUILayout.Label ("New Map Settings", EditorStyles.boldLabel);{
			mapName = EditorGUILayout.TextField ("Map Name", mapName);
			mapWidth = EditorGUILayout.IntSlider("Width", mapWidth, 0, 31);
			mapHeight = EditorGUILayout.IntSlider("Height", mapHeight, 0, 23);
			layerCount = Mathf.Clamp(EditorGUILayout.IntField("Layers (1-3)", layerCount, GUILayout.MaxWidth(200)), 3, 3);
		}
		
		if(GUILayout.Button(createMapButton, GUILayout.MaxWidth(200))){
			createMap(mapName, mapWidth, mapHeight, layerCount);
		}
	}

	//Cria o mapa
	void createMap(string name, int w, int h, int l){
		GameObject map;
		map = (GameObject) Instantiate (Resources.Load ("Prefabs/GameMap") as GameObject, Vector2.zero , Quaternion.identity);
		map.GetComponent<GameMapController> ().createMap (name, w, h, l);
		selectedLayer = 0;
	}

	//Ediçao do mapa - Janela
	void displayMapEditorWindow(){
		GUILayout.Label ("Map Editor", EditorStyles.boldLabel);
		optionsLayer = new string[activeMap.mapLayers.Length];
		for (int i = 0; i < activeMap.mapLayers.Length; i++) {
			optionsLayer[i] = activeMap.mapLayers[i].name;
		}
		selectedLayer = EditorGUILayout.Popup("Active Layer", selectedLayer, optionsLayer);
		activeMap.chooseActiveLayer (selectedLayer);
		activeMap.showGrid = EditorGUILayout.Toggle ("Show Grid", activeMap.showGrid);
		activeMap.grid.SetActive (activeMap.showGrid);
//		hideChildren = EditorGUILayout.Toggle ("Hide Layers in Hierarchy", hideChildren);
//		activeMap.hideChildren(hideChildren);

		terrains = Resources.LoadAll ("Prefabs/Terrains");

		//Texture2D[] thumbs = Resources.LoadAll<Texture2D> ("Sprites/Terrain");
		Texture2D[] thumbs = new Texture2D[terrains.Length];
		for (int i = 0; i < terrains.Length; i++) {
			thumbs[i] =  ( (GameObject) terrains[i] ).GetComponent<TileTerrain>().tileSet;
		}


		for (int i = 0; i < thumbs.Length; i++) {
			GUILayout.BeginHorizontal();{
				//Apertou no terreno
				if (GUILayout.Button (AssetPreview.GetMiniThumbnail (thumbs [i]), GUILayout.Height (50), GUILayout.Width (50))) {
					GameObject[] selectedTiles = Selection.gameObjects; //GetFiltered(typeof(Tile), SelectionMode.Unfiltered); // Devo filtrar esse bagulho: ta pegando seleçao d ebaixo quando troca layer
					for (int t = 0; t < selectedTiles.Length; t++){
						(selectedTiles[t].GetComponent<Tile>()).terrain = ((GameObject) terrains[i]).GetComponent<TileTerrain>(); ;
						(selectedTiles[t].GetComponent<Tile>()).forceUpdate();
					}
				}
				GUILayout.BeginVertical();{
					GUILayout.Space(20);
					GUILayout.Label (terrains[i].name, EditorStyles.boldLabel);
				}
				GUILayout.EndVertical();

			}
			GUILayout.EndHorizontal();
		}

		if (GUILayout.Button("Salvar Mapa como Prefab")){
			PrefabUtility.CreatePrefab("Assets/Resources/Prefabs/Maps/" + activeMap.name + ".prefab", activeMap.gameObject);
		}


	}



}
