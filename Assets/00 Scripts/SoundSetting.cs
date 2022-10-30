using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundSetting : MonoBehaviour
{
    bool BgmOn=true;
    bool SfxOn=true;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
