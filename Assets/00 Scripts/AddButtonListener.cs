using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButtonListener : MonoBehaviour
{
    void OnEnable()
    {
        soundManager = SoundManager.instance;

        AddClickSound();
    }

    void AddClickSound()
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].onClick.AddListener(delegate { soundManager.sfxAudio.Play(SfxAudio.Sfx.BUTTON); });
    }

    [SerializeField]
    Button[] buttons;

    SoundManager soundManager;
}
