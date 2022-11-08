using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dongle : MonoBehaviour
{
    public GameManager manager;
    public ParticleSystem effect;
    public bool isDrag; //기본값 false
    public bool isMerge;
    public bool isAttach;
    public bool isLine;
    public int level;
    
    public Rigidbody2D rigid;

    CircleCollider2D circle;
    Animator anim;
    SpriteRenderer spriteRenderer;
    SoundManager soundManager;

    float deadtime;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        soundManager = SoundManager.instance;

        anim.SetInteger("Level", level);
    }
    void OnDisable()
    {
        level = 0;
        isDrag = false;
        isMerge = false;
        isAttach = false;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.zero;

        rigid.simulated = false;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
        circle.enabled = true;
    }
    void Update()
    {
        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //스크린 좌표를 월드 좌표로
                                                                                    //x축 경계 설정
            float leftBorder = -2.9f + transform.localScale.x;
            float rightBorder = 2.9f - transform.localScale.x;

            if (mousePos.x < leftBorder)
            {
                mousePos.x = leftBorder;
            }
            else if (mousePos.x > rightBorder)
            {
                mousePos.x = rightBorder;
            }

            mousePos.y = 4.2f;
            mousePos.z = 0;
            //부드럽게 따라다니게 lerp(현재위치, 목표위치, 따라가는 강도
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);
        }
        
    }
    public void Drag()
    {
        isDrag = true;
    }
    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(AttachRoutine());
    }
    IEnumerator AttachRoutine()
    {
        if(isAttach)
        {
            yield break;
        }
        isAttach = true;
        //manager.SfxPlay(GameManager.Sfx.Attach);
        soundManager.PlaySfx(SoundManager.Sfx.ATTACH);

        yield return new WaitForSeconds(0.2f);

        isAttach = false;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Dongle"))
        {
            Dongle other = collision.gameObject.GetComponent<Dongle>();

            if(level == other.level && !isMerge && !other.isMerge && level < 8)
            {
                //나와 상대 위치 가져오기
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;
                //1. 내가 아래
                //2. 동일한 높이, 내가 오른쪽
                if (meY < otherY || (meY == otherY && meX > otherX))
                {
                    //상대 숨기기
                    other.Hide(transform.position);
                    //나 레벨업
                    LevelUp();
                }
            }
        }
    }
    public void Hide(Vector3 targetPos)
    {
        isMerge = true;

        rigid.simulated = false;
        circle.enabled = false; //콜라이더 비활성화 enabled

        if(targetPos == Vector3.up*100)
        {
            EffectPlay();
        }

        StartCoroutine(HideRoutine(targetPos));
    }

    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;

        while (frameCount < 20)
        {
            frameCount++;
            if (targetPos != Vector3.up * 100)
                transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
            else
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.2f);
            yield return null;
        }

        manager.score += (int)Mathf.Pow(2, level);

        isMerge = false;
        gameObject.SetActive(false);
    }
    void LevelUp()
    {
        isMerge = true;

        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;

        StartCoroutine(LevelUpRoutine());
    }
    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        EffectPlay();
        //manager.SfxPlay(GameManager.Sfx.LevelUp);
        soundManager.PlaySfx(SoundManager.Sfx.LEVELUP);

        anim.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel);

        isMerge = false;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            deadtime += Time.deltaTime;
            
            if(deadtime > 2)
            {
                //spriteRenderer.color = Color.red;
                StartCoroutine(FadeColor());

            }
            if(deadtime > 4)
            {
                manager.GameOver();
            }
        }    
    }
    IEnumerator FadeColor()
    { 

        float fadeCount = 1f;
        while (fadeCount >= 0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = new Color(1, fadeCount, fadeCount, 1);
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Finish")
        {
            deadtime = 0;
            spriteRenderer.color = Color.white;
        }
    }

    void EffectPlay()
    {
        effect.transform.position = transform.position;
        effect.transform.localScale = transform.localScale;
        effect.Play();
    }
}
