using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    Player player;
    private InputHandler input;
    private float halfScreenX = Screen.width / 2, halfScreenY = Screen.height / 2;
    private int angleCoeff = 30;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Start()
    {
        input = InputHandler.Instance;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {

    }
}
