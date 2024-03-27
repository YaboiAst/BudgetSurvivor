using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<char> _enemyName = new();
    
    private TextMeshProUGUI _enemyNameText;

    private BoxCollider2D _collider;

    private Rigidbody2D _rb;
    private Transform _target;
    [SerializeField] private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _enemyNameText = GetComponent<TextMeshProUGUI>();
        _enemyName.AddRange(_enemyNameText.text.ToCharArray());

        _target = GameObject.Find("Jogador").transform;
    }

    private void FixedUpdate() {
        if(_target == null){
            Debug.Log("No Target Found");
        }

        Vector3 moveDir = Vector3.right * (_target.position.x - transform.position.x) + Vector3.up * (_target.position.y - transform.position.y);
        transform.Translate(moveDir.normalized * (speed * Time.fixedDeltaTime));
    }

    public void TakeDamage(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            RemoveLetter();
        }
    }
    
    void RemoveLetter()
    {
        if (_enemyName.Count > 0)
        {
            var size = _collider.size;
            size = new Vector2(size.x - 0.33f, size.y);
            _collider.size = size;
            _enemyName.RemoveAt(_enemyName.Count - 1);
            _enemyNameText.text = new string(_enemyName.ToArray());
            if (_enemyName.Count == 0)
                Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            PlayerCombat playerCombat = other.gameObject.GetComponent<PlayerCombat>();
            playerCombat.RemoveLetter();
            playerCombat.Invencibility();
        }
    }
}
