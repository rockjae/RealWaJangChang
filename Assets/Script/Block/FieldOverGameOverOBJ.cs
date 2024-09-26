using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldOverGameOverOBJ : MonoBehaviour
{
    public Action GameOverCollision;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("player"))
        {
            Debug.Log("rockjae0 player out");
            GameOverCollision();
        }
    }
}
