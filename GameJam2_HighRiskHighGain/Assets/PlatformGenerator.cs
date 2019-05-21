using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public int levelHeight;
    public int levelWidthAndLenght;
    public int platformHeight;
    private int currentPlatformPos;
    void Start(){
        currentPlatformPos = platformHeight;
        for(int i = 0; i < levelHeight/platformHeight; i++){
            Vector3 platformPos = new Vector3(Random.RandomRange(-levelWidthAndLenght/2, levelWidthAndLenght/2),currentPlatformPos,Random.RandomRange(-levelWidthAndLenght/2, levelWidthAndLenght/2));
            Instantiate(platformPrefab, platformPos, Quaternion.identity);
            currentPlatformPos += platformHeight;
        }
    }
}
