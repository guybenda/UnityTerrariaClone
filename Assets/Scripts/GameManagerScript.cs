using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    void Awake()
    {
        Camera.main.orthographicSize = Screen.height / 32f;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
