using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlock : MonoBehaviour
{
    public Action<Collision2D> doorBlockCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        doorBlockCollision(collision);
    }
}
