using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagEvent : MonoBehaviour
{
    public Action<Collision2D> FlagCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FlagCollision(collision);
    }
}
