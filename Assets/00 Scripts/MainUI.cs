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

    bool isPlayingBgm = true;

    public void StartGame() //메인에서 게임시작 연결
    {
        SceneManager.LoadScene(0);
    }
    public void GameExit() //메인에서 게임종료
    {
        Application.Quit();
    }
    public void StopBGM()
    {
        isPlayingBgm = !isPlayingBgm; //else 조건 보충

        if (isPlayingBgm)
            bgmPlayer.Play();
        else
            bgmPlayer.Stop();
    }

}
