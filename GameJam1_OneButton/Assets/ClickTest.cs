using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTest : MonoBehaviour
{
    public MoveRooms moveRooms;
    public GameText gameText;
    private bool one = false;
    private bool two = false;
    private bool three = false;

    private bool gamestart = false;
    private int hold = 2;
    private int i = 0;
    private bool begin = false;

    //final player score;
    private int finalScore=0;
    private int highScore=0;

    private void Start(){
        // //Random game choice
        // StartCoroutine(RandomChoice());
    }

    public void Update(){
        //if the game is busy, you can't change game
        if(gamestart){
            if(hold == 0){
                if(Input.GetKey(KeyCode.F)){
                    Loss();
                }
            }
            if(hold == 1) {
                if(!Input.GetKey(KeyCode.F)){
                    Loss();
                } 
            }
            if(hold == 3) {
                if(Input.GetKeyDown(KeyCode.F)){
                    i++;
                    if(i >= 6){
                        Loss();
                    }
                } 
            }
        } else {
            //choose which game you want
            // if(Input.GetKey(KeyCode.Alpha1)){
            //     StartCoroutine(ClearConsole());
            //     Debug.Log("game 1 start");
            //     StartCoroutine(CountDownOne());
            //     gamestart = true;
            // }
            // if(Input.GetKey(KeyCode.Alpha2)){
            //     StartCoroutine(ClearConsole());
            //     Debug.Log("game 2 start");
            //     StartCoroutine(CountDownTwo());
            //     gamestart = true;
            // }
            // if(Input.GetKey(KeyCode.Alpha3)){
            //     StartCoroutine(ClearConsole());
            //     Debug.Log("game 3 start");
            //     StartCoroutine(CountDownThree());
            //     gamestart = true;
            // }
        }

        if(!begin){
            Debug.Log("begin active");
            if(Input.GetKeyDown(KeyCode.F)){
                //Random game choice
                StartCoroutine(RandomChoice());
                finalScore = 0;
                begin = true;
            }
        }
    }

    //Game 1 rules
    public IEnumerator CountDownOne(){
        gameText.InfoDisplay("Wait 2 seconds for your turn\nHold F for 3 seconds");
        hold = 2;
        yield return new WaitForSeconds(2);
        gameText.HoldFor(3);
        hold = 2;
        yield return new WaitForSeconds(1f); //wait time
        hold = 1;
        yield return new WaitForSeconds(2);
        gameText.DontHoldFor(1f);
        hold = 2;
        yield return new WaitForSeconds(1f); //wait time
        hold = 0;


        yield return new WaitForSeconds(0.5f); //wait time
        finalScore += 1;
        NextRoom();
        gamestart = false;
    }
    
    //Game 2 rules
    public IEnumerator CountDownTwo(){
        gameText.InfoDisplay("Pay Quick Respects\nPress F 5 times in 3 seconds");
        hold = 2;
        yield return new WaitForSeconds(2f); //wait time
        gameText.Clicker(3);
        i = 0;
        hold = 3;
        yield return new WaitForSeconds(3);
        if(i < 5){
            Loss();
        }
        hold = 2;

        yield return new WaitForSeconds(0.5f); //wait time
        finalScore += 5;
        NextRoom();
        gamestart = false;
    }

    //Game 3 rules
    public IEnumerator CountDownThree(){
        gameText.InfoDisplay("Pay Deep and Quick respect 3 times\nHold F for 2 second 3 times");
        hold =2;
        yield return new WaitForSeconds(2);
        gameText.HoldFor(2f);
        hold = 2;
        yield return new WaitForSeconds(1f);
        hold = 1;
        yield return new WaitForSeconds(1f);
        gameText.DontHoldFor(1f);
        hold = 2;
        yield return new WaitForSeconds(1f); 
        hold = 0;

        yield return new WaitForSeconds(0.5f);
        gameText.HoldFor(2f);
        hold = 2;
        yield return new WaitForSeconds(1f);
        hold = 1;
        yield return new WaitForSeconds(1f);
        gameText.DontHoldFor(1f);
        hold = 2;
        yield return new WaitForSeconds(1f); 
        hold = 0;

        yield return new WaitForSeconds(0.5f);
        gameText.HoldFor(2f);
        hold = 2;
        yield return new WaitForSeconds(1f);
        hold = 1;
        yield return new WaitForSeconds(1f);
        gameText.DontHoldFor(1f);
        hold = 2;
        yield return new WaitForSeconds(1f); 
        hold = 0;
    
        yield return new WaitForSeconds(0.5f);
        hold = 2;

        yield return new WaitForSeconds(0.5f);
        finalScore += 3;
        NextRoom();
        gamestart = false;
    }

    public void Loss(){
        StopAllCoroutines();
        StartCoroutine(LossE());
    }

    public IEnumerator LossE(){
        if(finalScore >= highScore){
            highScore = finalScore;
        }
        gameText.Loss(finalScore,highScore);
        gamestart = false;
        yield return new WaitForSeconds(4);
        begin = false;
    }

    public void NextRoom(){
        gameText.InfoDisplay("Nice Respecting");
        moveRooms.MoveRoomsBack();
        StartCoroutine(RandomChoice2());
    }

    //RandomGameStarter
    public IEnumerator RandomChoice(){
        gameText.Starting();
        yield return new WaitForSeconds(3);
        int random = Random.Range(0,3);
        //Debug.Log(random);
        if(random == 0){
            Debug.Log("game 1 start");
            StartCoroutine(CountDownOne());
            gamestart = true;
        }
        if(random == 1){
            Debug.Log("game 2 start");
            StartCoroutine(CountDownTwo());
            gamestart = true;
        }
        if(random == 2){
            Debug.Log("game 3 start");
            StartCoroutine(CountDownThree());
            gamestart = true;
        }
    }

    //RandomGameStarter
    public IEnumerator RandomChoice2(){
        //gameText.Starting();
        yield return new WaitForSeconds(3);
        int random = Random.Range(0,3);
        //Debug.Log(random);
        if(random == 0){
            Debug.Log("game 1 start");
            StartCoroutine(CountDownOne());
            gamestart = true;
        }
        if(random == 1){
            Debug.Log("game 2 start");
            StartCoroutine(CountDownTwo());
            gamestart = true;
        }
        if(random == 2){
            Debug.Log("game 3 start");
            StartCoroutine(CountDownThree());
            gamestart = true;
        }
    }
}
