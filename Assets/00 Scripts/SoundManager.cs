using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null) //soundManager 최초 할당
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //씬이 바뀔 때 자신이 파괴되는 것을 방지
        }
        else
            Destroy(gameObject); // 이 오브젝트가 존재하는 씬으로 올 때 instance에 또 할당이 되기 때문인데
                                 // 이미 instance에 할당이 되어있다면 사운드매니저는 활성화 되어있다는 뜻이므로
                                 // instance는 null이 아님. 따라서 새로 생성된 해당 오브젝트를 제거해주어야 함

        audioSource = GetComponent<AudioSource>();

        // 인스펙터에서 넣어준 audioClip들을 bgm 및 sfx dictionary에 넣어줌으로써
        // O(1)시간에 원하는 클립을 재생시킬 수 있도록 함.
        // O(1)시간은 자료를 탐색할 때 가장 빠른 속도를 보여주는 지표(시간복잡도).
        for (int i = 0; i < bgmClips.Length; i++)
            bgmDictionary.Add(bgmClips[i].name, bgmClips[i]);
        for (int i = 0; i < sfxClips.Length; i++)
            sfxDictionary.Add(sfxClips[i].name, sfxClips[i]);
    }

    /// <summary>
    /// 원하는 bgm이름을 입력하면 재생시켜주는 함수
    /// 두번째인자는 루프 인자, 생략할 시 자동으로 loop됨
    /// </summary>
    /// <param name="bgmName"></param>
    /// <param name="loop"></param>
    public void PlayBgm(string bgmName, bool loop = true)
    {
        // bgmDictionary에 실행하려는 bgmName이 등록돼있지 않다면 아무것도 하지않고 함수 종료
        if (!bgmDictionary.ContainsKey(bgmName))
            return;

        audioSource.loop = loop; // 루프할지 말지 인자로 결정
        audioSource.clip = bgmDictionary[bgmName];
        audioSource.Play();
    }

    /// <summary>
    /// 실행중인 bgm멈춤.
    /// 이미 실행중인 bgm이 없으면 아무것도 실행하지 않고 종료
    /// </summary>
    public void StopBgm()
    {
        if (!audioSource.isPlaying)
            return;

        audioSource.Stop();
    }

    /// <summary>
    /// 원하는 sfx이름을 입력하면 재생시켜주는 함수
    /// </summary>
    public void PlaySfx(string sfxName)
    {
        // sfxDictionary에 실행하려는 sfxName이 등록돼있지 않다면 아무것도 하지않고 함수 종료
        if (!sfxDictionary.ContainsKey(sfxName))
            return;

        // sfx는 일회성이므로 PlayOneShot()함수로 실행
        audioSource.PlayOneShot(sfxDictionary[sfxName]);
    }

    [SerializeField]
    AudioClip[] bgmClips;
    [SerializeField]
    AudioClip[] sfxClips;

    AudioSource audioSource;
    Dictionary<string, AudioClip> bgmDictionary = new Dictionary<string, AudioClip>(); // key와 value를 가진 dictionary구조
    Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>(); // key와 value를 가진 dictionary구조
}
