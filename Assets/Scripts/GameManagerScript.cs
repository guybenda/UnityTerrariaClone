using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public float scale = 1f;

    void Awake()
    {
        Camera.main.orthographicSize = (Screen.height / 32f * scale);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
