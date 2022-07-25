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
