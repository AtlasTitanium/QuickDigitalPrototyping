using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameText : MonoBehaviour
{
    private Text gametext;
    private float timetogo = 5;
    private bool holding = false;
    private bool notholding = false;
    private bool clicker = false;
    private bool start = false;

    private int finalScore;
    private int highScore;

    private void Start(){
        gametext = GetComponentInChildren<Text>();
        gametext.text = "Press F to start paying respects";
    }
    
    private void Update(){
        if(start){
            gametext.text = "Wait for: " + timetogo.ToString("F2") + " seconds";
            timetogo -= Time.deltaTime;
            if(timetogo <= 0){
                gametext.text = "";
                start = false;
            }
        }

        if(holding){
            gametext.text = "Hold F!\n" + timetogo.ToString("F2") + " seconds";
            timetogo -= Time.deltaTime;
            if(timetogo <= 0){
                gametext.text = "";
                holding = false;
            }
        }
        if(notholding){
            gametext.text = "Stop Holding F!\n" + timetogo.ToString("F2") + " seconds";
            timetogo -= Time.deltaTime;
            if(timetogo <= 0){
                gametext.text = "";
                notholding = false;
            }
        }
        if(clicker){
            gametext.text = "Press F!\n" + timetogo.ToString("F2") + " seconds";
            timetogo -= Time.deltaTime;
            if(timetogo <= 0){
                gametext.text = "";
                clicker = false;
            }
        }
    }

    public void HoldFor(float _seconds){
        timetogo = _seconds;
        holding = true;
    }

    public void DontHoldFor(float _seconds){
        timetogo = _seconds;
        notholding = true;
    }

    public void Clicker(float _seconds){
        timetogo = _seconds;
        clicker = true;
    }

    public void InfoDisplay(string what){
        notholding = false;
        holding = false;
        gametext.text = what;
    }

    public void Starting(){
        start = true;
        timetogo = 3;
    }

    public void Loss(int _finalScore,int _highScore){
        clicker = false;
        notholding = false;
        holding = false;
        start = false;
        finalScore = _finalScore;
        highScore = _highScore;
        gametext.text = "Not fast enough\nYou Respected " + finalScore + " people\nMost people respected ever: " + highScore;
        StartCoroutine(waitforagain());
    }

    public IEnumerator waitforagain(){
        yield return new WaitForSeconds(4);
        gametext.text = "Not fast enough\nYou Respected " + finalScore + " people\n\nMost people respected ever: " + highScore + "\n\nPress F to start again";
    }
}
