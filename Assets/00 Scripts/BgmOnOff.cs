using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmOnOff : ButtonOnOff
{
    public override void OnClickEvent()
    {
        isOn = !isOn;
        image.sprite = isOn ? OnSprite : OffSprite;
        soundManager.bgmAudio.mute = !isOn;
    }
}
