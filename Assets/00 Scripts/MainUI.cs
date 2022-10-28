using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainUI : MonoBehaviour
{
    [Header("----------[ Audio ]")]
    public AudioSource bgmPlayer;
    public AudioClip sfxButton;

    void Start()
    {
            
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
