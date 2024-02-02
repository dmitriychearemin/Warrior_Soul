using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conrol_FPS : MonoBehaviour
{
    private int maxFPS = 60;

    private void Start()
    {
        Application.targetFrameRate = maxFPS;
    }

    public void SetFPS(int fps)
    {
        maxFPS = fps;
        Application.targetFrameRate = maxFPS;
    }
}
