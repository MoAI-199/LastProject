using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Henchman : MonoBehaviour
{

    [SerializeField]
    private Transform Player;

    private float speed = 0.0625f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Transform PlayerTransform = Player.transform;
        //this.transform.position = PlayerTransform.position;
        this.transform.position = Vector2.MoveTowards( transform.position, Player.position, speed );
    }
}
