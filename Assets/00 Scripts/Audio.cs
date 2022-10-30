using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Audio : MonoBehaviour
{
    public AudioClip btn;
    
    public void SfxPlay()
    {
        GetComponent<AudioSource>().PlayOneShot(btn);
    }
    public void SfxStop()
    {
           //방금 클릭한 버튼의 객체 받아옴
        if(EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn)
        {
            GetComponent<AudioSource>().volume = 1f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0f;
        }
    }
}
