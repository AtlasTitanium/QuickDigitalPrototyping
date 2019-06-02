using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SafetyLevel{Easy, Medium, Hard}
public class PlatformGenerator : MonoBehaviour
{
    public SafetyLevel safety;
    public Text scoreText;
    public Transform parentEnviroment;
    public GameObject[] platformPrefabs;
    public int[] platformChances;
    public GameObject groundPrefab;
    public GameObject lamps;
    public GameObject currentLava;
    public int levelWidthAndLenght;
    public int platformHeight;
    public float platformWidhtAndLenght;
    public int lampHeight;
    public int checkpointHeight;


    private int currentPlatformPos;
    private GameObject player;
    private int score = 0;

    void Start(){
        player = GameObject.Find("Player");
        currentPlatformPos = platformHeight;
        CreatePlatform();
    }
    public void CreatePlatform(){
        //chance generator
        int platformIndex = 0;
        int chance = Random.Range(0,100);
        for(int i = 0; i < platformPrefabs.Length; i++){
            if(chance <= platformChances[i]){
                platformIndex = i;
            }
        }

        if(currentPlatformPos%checkpointHeight == 0){
            //Creating a checkpoint
            Vector3 platformPos2 = new Vector3(0,player.transform.position.y-3,0);
            GameObject platform2 = Instantiate(groundPrefab, platformPos2, Quaternion.identity, parentEnviroment);
            Vector3 platformSize2 = new Vector3(levelWidthAndLenght, 1, levelWidthAndLenght);
            platform2.transform.localScale = platformSize2;
            platformWidhtAndLenght -= 0.5f;
            if(platformWidhtAndLenght <= 1){
                platformWidhtAndLenght = 1;
            }
        }

        //Creating a platform
        Vector3 platformPos = new Vector3(Random.RandomRange(-levelWidthAndLenght/2, levelWidthAndLenght/2),currentPlatformPos,Random.RandomRange(-levelWidthAndLenght/2, levelWidthAndLenght/2));
        GameObject platform = Instantiate(platformPrefabs[platformIndex], platformPos, Quaternion.identity, parentEnviroment);
        Vector3 platformSize = new Vector3(platformWidhtAndLenght, 1, platformWidhtAndLenght);
        platform.GetComponent<PlatFormInit>().platformGenerator = this;
        platform.GetComponent<PlatFormInit>().lava = currentLava;
        platform.transform.localScale = platformSize;

        currentPlatformPos += platformHeight;
        if(currentPlatformPos%lampHeight == 0){
            Instantiate(lamps, new Vector3(0,currentPlatformPos,0), Quaternion.identity);
        }

        switch(safety){
            case SafetyLevel.Easy:
                score += 1;
            break;

            case SafetyLevel.Medium:
                score += 2;
            break;

            case SafetyLevel.Hard:
                score += 4;
            break;
        }

        scoreText.text = "Score: " + score;
    }
}
