using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	public Texture2D[] mapList;
	Texture2D currentMap;
	
	public GameObject grassSection;
	public GameObject roadSection;
	public GameObject[] buildings;
	public Rect MapBounds;
	public float levelScale = 1.0f;
	Color BuildingColor = new Color(1,0,0,1);
	Color RoadColor = new Color(0,0,0,1);
	Color ToxicColor = new Color(0,1,0,1);

	void Start() {
		currentMap = mapList[Random.Range(0, mapList.Length)];
		BuildMap();
	}

	void BuildMap() {
		for(int i = 0; i < currentMap.width; ++i){
			for(int j = 0; j < currentMap.height; ++j){
				Color currentColor = currentMap.GetPixel(i, j);

				GameObject objToSpawn;

				if(currentColor == RoadColor) {
					objToSpawn = roadSection;
				} else if(currentColor == BuildingColor) {
					objToSpawn = grassSection;
					GameObject buildingObj = buildings[Random.Range(0, buildings.Length)];
					GameObject building = Instantiate(buildingObj, new Vector3(grassSection.transform.localScale.x * i, grassSection.transform.localScale.y * j, 0), Quaternion.identity) as GameObject;
					building.transform.parent = transform;
				} else {
					objToSpawn = grassSection;
				}

				// Add the tile to the map object.
				GameObject obj = Instantiate(objToSpawn, new Vector3(objToSpawn.transform.localScale.x * i, objToSpawn.transform.localScale.y * j, 0), Quaternion.identity) as GameObject;
				obj.transform.parent = transform;
			}
		}
		MapBounds = new Rect(0,0,grassSection.transform.localScale.x * currentMap.width *  levelScale, grassSection.transform.localScale.y * currentMap.height *  levelScale);
		transform.localScale = new Vector3 (levelScale, levelScale, 0);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
