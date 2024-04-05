using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
    public Vector2 movDirection;
    public float movHor, movVer;

    private Rigidbody2D rb;
    public float speed = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movHor = Input.GetAxisRaw("Horizontal");
        movVer = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        movDirection = new Vector2(movHor, movVer);
        rb.AddForce(movDirection * speed * Time.fixedDeltaTime);

        if (movDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
