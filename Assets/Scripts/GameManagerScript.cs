using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    float m_CameraScale;
    public float CameraScale = 1;

    void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {
        if (CameraScale != m_CameraScale)
        {
            Camera.main.orthographicSize = (Screen.height / 32f * CameraScale);
            m_CameraScale = CameraScale;
        }
    }
}
