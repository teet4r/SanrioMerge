using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //크래스 모든 멤버 변수에게 [SerializeField]을 한꺼번에 적용시키는 것과 같음
public class Sound
{
    public string name; // 곡 이름
    public AudioClip clip; // 오디오 클립
}

public class SoundManager : MonoBehaviour
{
    #region singleton
    static public SoundManager instance;

    private void Awake()
    {
        if (instance == null) //soundManager 최초 할당
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //씬이 바뀔 때 자신이 파괴되는 것을 방지
        }
        else
            Destroy(this.gameObject);
    }
    #endregion singleton

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    public AudioSource bgmSource;
    public AudioSource[] sfxSources;
    public string[] playSoundName;

    void Start()
    {
        
    }

    void PlaySE(string _name)
    {
        for(int i=0; i<effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name)
            {
                for(int j=0; j<sfxSources.Length; j++)
                {
                    if()
                }
            }
        }
    }
}
