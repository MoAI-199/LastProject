using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer2 : MonoBehaviour
{
    private Parent _pearent;

    void Start()
    {
        _pearent = GetComponent<Parent>();
    }

    void Update()
    {
        Vector2 position = transform.position;
        float speed = _pearent.getSpeed();
        if (Input.GetKey("left"))
        {
            position.x -= speed;
        }
        if (Input.GetKey("right"))
        {
            position.x += speed;
        }
        if (Input.GetKey("up"))
        {
            position.y += speed;
        }
        if (Input.GetKey("down"))
        {
            position.y -= speed;
        }

        transform.position = position;
    }
}
