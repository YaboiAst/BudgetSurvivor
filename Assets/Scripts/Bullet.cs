using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _letterText;
    
    private float bulletSpeed = 5.0f;
    private Rigidbody2D _rb;
    private int damage = 1;

    private float spinSpeed = 50f;

    private void Start()
    {
        spinSpeed = Random.Range(50, 1000);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void Update()
    {
        // Makes bullet spin
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }

    public void SetLetter(char letter)
    {
        _letterText = GetComponent<TextMeshProUGUI>();    
        _rb = GetComponent<Rigidbody2D>();

        _letterText.text = letter.ToString();
        _rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        
        Destroy(this.gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
