using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxOnOff : ButtonOnOff
{
    public override void OnClickEvent()
    {
        isOn = !isOn;
        image.sprite = isOn ? OnSprite : OffSprite;
        soundManager.sfxAudio.SetMute(!isOn);
    }
}
