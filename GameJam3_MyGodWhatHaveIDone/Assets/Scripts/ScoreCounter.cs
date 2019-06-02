using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    //singleton
    private static ScoreCounter _instance;

    public static ScoreCounter Instance { get { return _instance; } }

    //variables
    public Text scoreboard;
    public Material goodCubemap;
    public Material badCubemap;
    public GameObject gameOverObject;
    public Text gameoverText;
    public string[] possibleGameOvetTexts;


    private int score;
    private bool creapyEffect = false;
    private bool gameOver = false;
    private float time;
    private float fasterTime;
    private Color skyLightColor;
    private int amount;

    //Instantiate Singleton
    private void Awake()
    {
        skyLightColor = RenderSettings.ambientSkyColor;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    //functions
    public void GainScore(){
        score++;
        scoreboard.text = "You helped " + score + " kids!";
        creapyEffect = true;
        time = Time.time;
        fasterTime = Time.time;
        amount = 0;
    }

    void Update(){
        if(creapyEffect){
            float waitTime = 0.5f;
            if(time + waitTime <= Time.time){
                scoreboard.text = "YOU KILLED " + score + " KIDS!";
                RenderSettings.skybox = badCubemap;
                RenderSettings.ambientSkyColor = Color.red;

                float fastWait = 0.05f;
                if(fasterTime + fastWait <= Time.time){
                    scoreboard.color = Color.white;
                }
                if(fasterTime + fastWait + fastWait <= Time.time){
                    scoreboard.color = Color.black;
                    fasterTime = Time.time;
                }

            }
            if(time + waitTime + waitTime <= Time.time){
                scoreboard.color = Color.black;
                scoreboard.text = "You helped " + score + " kids!";
                RenderSettings.skybox = goodCubemap;
                RenderSettings.ambientSkyColor = skyLightColor;
                time = Time.time;

                creapyEffect = false;
            }
        }

        if(gameOver){
            if(Input.GetKeyDown(KeyCode.Return)){
                SceneManager.LoadScene(1);
            }
            if(Input.GetKeyDown(KeyCode.Escape)){
                SceneManager.LoadScene(0);
            }

            scoreboard.text = "YOU KILLED " + score + " KIDS!";
            RenderSettings.skybox = badCubemap;
            RenderSettings.ambientSkyColor = Color.red;

            float fastWait = 0.05f;
            if(fasterTime + fastWait <= Time.time){
                scoreboard.color = Color.white;
            }
            if(fasterTime + fastWait + fastWait <= Time.time){
                scoreboard.color = Color.black;
                fasterTime = Time.time;
            }

            float fastWait2 = 0.05f;
            if(fasterTime + fastWait2 <= Time.time){
                gameoverText.text = possibleGameOvetTexts[Random.Range(0,possibleGameOvetTexts.Length)];
                gameoverText.color = Color.white;
            }
            if(fasterTime + fastWait2 + fastWait2 <= Time.time){
                gameoverText.color = Color.black;
                fasterTime = Time.time;
            }
        }
    }

    public void GameOver(){
        Camera.main.gameObject.GetComponent<Move>().No();
        Camera.main.gameObject.GetComponent<Move>().enabled = false;
        Camera.main.gameObject.GetComponentInChildren<GunControl>().enabled = false;
        gameOver = true;
        gameOverObject.SetActive(true);
    }
}
