using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    protected Map map;

    void Awake()
    {
        map = Map.GetDefaultMap();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
