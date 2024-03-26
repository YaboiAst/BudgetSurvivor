using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    private Vector2 _movementInput;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementInput.x = Input.GetAxis("Horizontal");
        _movementInput.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        // _rb.velocity = _movementInput * (speed * Time.fixedDeltaTime);
        transform.Translate(_movementInput * (speed * Time.fixedDeltaTime));
    }
}
