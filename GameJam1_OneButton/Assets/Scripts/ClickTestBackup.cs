using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTestBackup : MonoBehaviour
{
    public MoveRooms moveRooms;
    public GameText gameText;
    public Sprite[] FButtonSprite;
    public Image FButton;
    public Image[] FiveFButtons;
    public Image[] ThreeFButtons;
    public int buttonPressSpeed = 10;

    //Privates
    private bool buttonColorChange = true;
    private bool clickerColorChange = false;
    private bool holdColorChange = false;
    private bool gamestart = false;
    private int hold = 2;
    private int i = 0;
    private bool begin = false;
    private float spriteIndex = 0;
    private int fiveButtonIndex = -1;
    private int threeButtonIndex = 0;

    //final player score;
    private int finalScore=0;
    private int highScore=0;

    public void Update(){
        //if the game is busy, you can't change game
        if(buttonColorChange){
            if(Input.GetKey(KeyCode.F)){
                spriteIndex += Time.deltaTime * buttonPressSpeed;
                if(spriteIndex >= FButtonSprite.Length-1){
                    spriteIndex = FButtonSprite.Length-1;
                }
                FButton.sprite = FButtonSprite[Mathf.RoundToInt(spriteIndex)];
            }

            if(!Input.GetKey(KeyCode.F)){
                spriteIndex -= Time.deltaTime * buttonPressSpeed;
                if(spriteIndex <= 0){
                    spriteIndex = 0;
                }
                FButton.sprite = FButtonSprite[Mathf.RoundToInt(spriteIndex)];
            }
        }

        if(clickerColorChange){
            if(Input.GetKeyDown(KeyCode.F)){
                fiveButtonIndex++;
                if(fiveButtonIndex >= FiveFButtons.Length){
                    Loss();
                }
                FiveFButtons[fiveButtonIndex].sprite = FButtonSprite[4];
            }
        }

        if(holdColorChange){
            if(Input.GetKey(KeyCode.F)){
                spriteIndex += Time.deltaTime * buttonPressSpeed;
                if(spriteIndex >= FButtonSprite.Length-1){
                    spriteIndex = FButtonSprite.Length-1;
                }
                ThreeFButtons[threeButtonIndex].sprite = FButtonSprite[Mathf.RoundToInt(spriteIndex)];
            }

            if(!Input.GetKey(KeyCode.F)){
                spriteIndex -= Time.deltaTime * buttonPressSpeed;
                if(spriteIndex <= 0){
                    spriteIndex = 0;
                }
                ThreeFButtons[threeButtonIndex].sprite = FButtonSprite[Mathf.RoundToInt(spriteIndex)];
            }
        }


        if(gamestart){
            


            // if(hold == 0){
            //     // if(Input.GetKey(KeyCode.F)){
            //     //     Loss();
            //     // }
            // }
            // if(hold == 1) {
            //     // if(!Input.GetKey(KeyCode.F)){
            //     //     Loss();
            //     // } 
            // }
            // if(hold == 3) {
            //     if(Input.GetKeyDown(KeyCode.F)){
            //         i++;
            //         if(i >= 6){
            //             Loss();
            //         }
            //     } 
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
        gameText.HoldFor(3);
        yield return new WaitForSeconds(3);
        if(FButton.sprite != FButtonSprite[4]){
            Loss();
        }
        StartCoroutine(ButtonFlicker(true,FButton,1));
        gameText.DontHoldFor(1f);
        yield return new WaitForSeconds(1f);
        if(Input.GetKey(KeyCode.F)){
            Loss();
        }

        yield return new WaitForSeconds(0.5f);
        finalScore += 1;
        NextRoom();
        gamestart = false;

        // gameText.InfoDisplay("Wait 2 seconds for your turn\nHold F for 3 seconds");
        // hold = 2;
        // yield return new WaitForSeconds(2);
        // gameText.HoldFor(3);
        // hold = 2;
        // yield return new WaitForSeconds(1f); //wait time
        // hold = 1;
        // yield return new WaitForSeconds(2);
        // gameText.DontHoldFor(1f);
        // hold = 2;
        // yield return new WaitForSeconds(1f); //wait time
        // hold = 0;


        // yield return new WaitForSeconds(0.5f); //wait time
        // finalScore += 1;
        // NextRoom();
        // gamestart = false;
    }
    
    //Game 2 rules
    public IEnumerator CountDownTwo(){
        SwitchButtons();
        fiveButtonIndex = -1;
        gameText.Clicker(3);
        yield return new WaitForSeconds(3);
        SwitchButtons();

        for(int i = 0; i < 5; i++){
            FiveFButtons[i].sprite = FButtonSprite[0];
        }
        finalScore += 5;
        NextRoom();
        gamestart = false;

        // clickerColorChange = true;
        // buttonColorChange = false;
        // gameText.InfoDisplay("Pay Quick Respects\nPress F 5 times in 3 seconds");
        // hold = 2;
        // yield return new WaitForSeconds(2f); //wait time
        // gameText.Clicker(3);
        // i = 0;
        // hold = 3;
        // yield return new WaitForSeconds(3);
        // if(i < 5){
        //     Loss();
        // }
        // hold = 2;

        // yield return new WaitForSeconds(0.5f); //wait time
        // clickerColorChange = false;
        // buttonColorChange = true;

        // finalScore += 5;
        // NextRoom();
        // gamestart = false;
    }

    //Game 3 rules
    public IEnumerator CountDownThree(){
        threeButtonIndex = 0;
        SwitchButtons2();

        gameText.HoldFor(2f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ButtonFlicker(false,ThreeFButtons[threeButtonIndex],1));
        gameText.DontHoldFor(1f);
        yield return new WaitForSeconds(1f); 
        if(Input.GetKey(KeyCode.F)){
            Loss();
        }
        yield return new WaitForSeconds(0.5f);

        ThreeFButtons[threeButtonIndex].sprite = FButtonSprite[4];
        threeButtonIndex++;
        gameText.HoldFor(2f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ButtonFlicker(false,ThreeFButtons[threeButtonIndex],1));
        gameText.DontHoldFor(1f);
        yield return new WaitForSeconds(1f); 
        if(Input.GetKey(KeyCode.F)){
            Loss();
        }
        yield return new WaitForSeconds(0.5f);
        
        ThreeFButtons[threeButtonIndex].sprite = FButtonSprite[4];
        threeButtonIndex++;
        gameText.HoldFor(2f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ButtonFlicker(false,ThreeFButtons[threeButtonIndex],1));
        gameText.DontHoldFor(1f);
        yield return new WaitForSeconds(1f); 
        if(Input.GetKey(KeyCode.F)){
            Loss();
        }

        ThreeFButtons[threeButtonIndex].sprite = FButtonSprite[4];
        yield return new WaitForSeconds(0.5f);
        SwitchButtons2();
        finalScore += 3;
        NextRoom();
        gamestart = false;

        // gameText.InfoDisplay("Pay Deep and Quick respect 3 times\nHold F for 2 second 3 times");
        // threeButtonIndex = 0;
        // hold =2;
        // yield return new WaitForSeconds(2);
        // gameText.HoldFor(2f);
        // hold = 2;
        // yield return new WaitForSeconds(1f);
        // hold = 1;
        // yield return new WaitForSeconds(1f);
        // gameText.DontHoldFor(1f);
        // hold = 2;
        // yield return new WaitForSeconds(1f); 
        // hold = 0;

        // threeButtonIndex++;
        // yield return new WaitForSeconds(0.5f);
        // gameText.HoldFor(2f);
        // hold = 2;
        // yield return new WaitForSeconds(1f);
        // hold = 1;
        // yield return new WaitForSeconds(1f);
        // gameText.DontHoldFor(1f);
        // hold = 2;
        // yield return new WaitForSeconds(1f); 
        // hold = 0;

        // threeButtonIndex++;
        // yield return new WaitForSeconds(0.5f);
        // gameText.HoldFor(2f);
        // hold = 2;
        // yield return new WaitForSeconds(1f);
        // hold = 1;
        // yield return new WaitForSeconds(1f);
        // gameText.DontHoldFor(1f);
        // hold = 2;
        // yield return new WaitForSeconds(1f); 
        // hold = 0;
    
        // yield return new WaitForSeconds(0.5f);
        // hold = 2;

        // yield return new WaitForSeconds(0.5f);
        // finalScore += 3;
        // NextRoom();
        // gamestart = false;
    }

    public void Loss(){
        StopAllCoroutines();
        StartCoroutine(LossE());
    }

    public IEnumerator LossE(){
        FButton.enabled = false;
        if(finalScore >= highScore){
            highScore = finalScore;
        }
        gameText.Loss(finalScore,highScore);
        gamestart = false;
        yield return new WaitForSeconds(4);
        begin = false;
    }

    public void NextRoom(){
        FButton.enabled = false;
        gameText.InfoDisplay("Nice Respecting");
        moveRooms.MoveRoomsBack();
        StartCoroutine(RandomChoice2());
    }

    //RandomGameStarter
    public IEnumerator RandomChoice(){
        gameText.Starting();
        FButton.enabled = false;
        spriteIndex = 0;
        yield return new WaitForSeconds(3);
        FButton.enabled = true;
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
        FButton.enabled = false;
        //gameText.Starting();
        yield return new WaitForSeconds(3);
        FButton.enabled = true;
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

    //button flicker
    public IEnumerator ButtonFlicker(bool one, Image curBut, int howLong){
        //button flickers 10 times within the given time
        if(one){
            buttonColorChange = false;
        } else{
            holdColorChange = false;
        }
        for(int i = 0; i < 5; i++){
            curBut.sprite = FButtonSprite[0];
            yield return new WaitForSeconds(howLong * 0.1f);
            curBut.sprite = FButtonSprite[4];
            yield return new WaitForSeconds(howLong * 0.1f);
        }
        if(one){
            buttonColorChange = true;
        } else{
            holdColorChange = true;
        }
    } 

    private void SwitchButtons(){
        for(int i = 0; i < FiveFButtons.Length; i++){
            FiveFButtons[i].enabled = !FiveFButtons[i].enabled;
        }
        FButton.enabled = !FButton.enabled;

        clickerColorChange = !clickerColorChange;
        buttonColorChange = !buttonColorChange;
    }

    private void SwitchButtons2(){
        for(int i = 0; i < ThreeFButtons.Length; i++){
            ThreeFButtons[i].enabled = !ThreeFButtons[i].enabled;
        }
        FButton.enabled = !FButton.enabled;

        holdColorChange = !holdColorChange;
        buttonColorChange = !buttonColorChange;
    }
}
