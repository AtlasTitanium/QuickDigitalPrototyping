using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGeneration : MonoBehaviour
{

    public GameObject roadPrefab;
    public GameObject currentRoad;

    public GameObject houseParent;
    public GameObject[] housePrefabs;
    private List<GameObject> currenHouses = new List<GameObject>();

    public GameObject[] npcPrefabs;
    public float distanceOfHouses;
    public float distanceBetweenHouses;
    public float houseHights;
    public int amountOfHouses;
    public int amountOfNPCs;
    public GameObject npcParent;

    
    private Vector3 lastPlayerPos;
    private Vector3 leftHouseLocations;
    private Vector3 rightHouseLocations;
    private float houseZPos = 0;
    private bool isMidway = false;
    private GameObject newRoad;
    private List<GameObject> newHouses = new List<GameObject>();

    private void Start(){
        lastPlayerPos = transform.position;

        //create houses
        houseZPos = transform.position.z;
        for(int i = 0; i < 10; i++){
            leftHouseLocations = new Vector3(-distanceOfHouses, houseHights, houseZPos);
            rightHouseLocations = new Vector3(distanceOfHouses, houseHights, houseZPos);

            GameObject leftHouse = Instantiate(housePrefabs[Random.Range(0,housePrefabs.Length)], leftHouseLocations, Quaternion.identity);
            GameObject rightHouse = Instantiate(housePrefabs[Random.Range(0,housePrefabs.Length)], rightHouseLocations, Quaternion.Euler(0,180,0));

            leftHouse.transform.parent = houseParent.transform;
            rightHouse.transform.parent = houseParent.transform;

            currenHouses.Add(leftHouse);
            currenHouses.Add(rightHouse);

            houseZPos += distanceBetweenHouses;
        }

        //create npc's
        for(int i = 0; i < amountOfNPCs; i++){
            Vector3 pos = new Vector3(Random.Range(-14,14), 2, Random.Range(transform.position.z + 25, 375));
            GameObject npc = Instantiate(npcPrefabs[Random.Range(0,npcPrefabs.Length)], pos, Quaternion.Euler(0,Random.Range(0,360),0));
            npc.transform.parent = npcParent.transform;
        }
    }

    private void Update(){
        if(transform.position.z >= lastPlayerPos.z + 200 && !isMidway){
            CreateRoad();
        }
        if(transform.position.z >= lastPlayerPos.z + 400 && isMidway){
            DestroyOldRoad();
        }
    }
    
    private void CreateRoad(){
        //create the new road at the next chunk
        Vector3 nextRoadLocation = new Vector3(currentRoad.transform.position.x, currentRoad.transform.position.y, currentRoad.transform.position.z + 400);
        newRoad = Instantiate(roadPrefab, nextRoadLocation, Quaternion.identity);

        //create houses
        houseZPos = nextRoadLocation.z;
        for(int i = 0; i < currenHouses.Count/2; i++){
            leftHouseLocations = new Vector3(-distanceOfHouses, houseHights, houseZPos);
            rightHouseLocations = new Vector3(distanceOfHouses, houseHights, houseZPos);

            GameObject leftHouse = Instantiate(housePrefabs[Random.Range(0,housePrefabs.Length)], leftHouseLocations, Quaternion.identity);
            GameObject rightHouse = Instantiate(housePrefabs[Random.Range(0,housePrefabs.Length)], rightHouseLocations, Quaternion.Euler(0,180,0));

            leftHouse.transform.parent = houseParent.transform;
            rightHouse.transform.parent = houseParent.transform;

            newHouses.Add(leftHouse);
            newHouses.Add(rightHouse);

            houseZPos += distanceBetweenHouses;
        }

        //increase the amount of kids per level with 10
        amountOfNPCs += 10;
        //create npc's
        for(int i = 0; i < amountOfNPCs; i++){
            Vector3 pos = new Vector3(Random.Range(-14,14), 2, Random.Range(nextRoadLocation.z + 25, nextRoadLocation.z + 375));
            GameObject npc = Instantiate(npcPrefabs[Random.Range(0,npcPrefabs.Length)], pos, Quaternion.Euler(0,Random.Range(0,360),0));
            npc.transform.parent = npcParent.transform;
        }

        isMidway = true;
    }

    private void DestroyOldRoad(){
        //destroy the old road
        Destroy(currentRoad);
        currentRoad = newRoad;
        lastPlayerPos.z = transform.position.z;

        //destroy old houses and set new as old
        for(int i = 0; i < currenHouses.Count; i++){
            Destroy(currenHouses[i]);
            currenHouses.RemoveAt(i);
            i--;
        }

        for(int i = 0; i < newHouses.Count; i++){
            currenHouses.Add(newHouses[i]);
            newHouses.RemoveAt(i);
            i--;
        }

        isMidway = false;
    }
}
