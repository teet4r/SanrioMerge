using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    void Awake()
    {
        leftWall.transform.localPosition = new Vector2(-Screen.width / 100 / 2f - 128 / 900f, 0f);
        rightWall.transform.localPosition = new Vector2(Screen.width / 100 / 2f + 128 / 900f, 0f);
    }

    public GameObject leftWall;
    public GameObject rightWall;
}
