using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<char> _enemyName = new();
    
    private TextMeshProUGUI _enemyNameText;

    private BoxCollider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _enemyNameText = GetComponent<TextMeshProUGUI>();
        _enemyName.AddRange(_enemyNameText.text.ToCharArray());
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
}
