using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartEasy(){
        SceneManager.LoadScene("EasyGame");
    }
    public void StartMedium(){
        SceneManager.LoadScene("MediumGame");
    }
    public void StartHard(){
        SceneManager.LoadScene("HardGame");
    }
}
