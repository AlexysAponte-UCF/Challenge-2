﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHor : MonoBehaviour
{

    private Vector2 pos1 = new Vector2(16,-2);
    private Vector2 pos2 = new Vector2(10, -2);
    public float speed = 1.0f;

    void Update()
    {
        transform.position = Vector2.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
