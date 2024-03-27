using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<char> playerName = new();

    public List<char> playerCurrentName = new();
    
    // public List<Transform> bulletPrefabs = new();
    public Transform bulletPrefab;
    
    public Transform bulletSpawnPoint;
    
    private TextMeshProUGUI _playerNameText;
    
    public float reloadTime = 0.5f;
    
    private  int playerDamage = 1;
    private BoxCollider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _playerNameText = GetComponent<TextMeshProUGUI>();
        playerName.AddRange(_playerNameText.text.ToCharArray());
        playerCurrentName.AddRange(_playerNameText.text.ToCharArray());
        Vector2 size = _collider.size;
        size = new Vector2( 0.37f * playerName.Count, size.y);
        _collider.size = size;
        
        // Player damage scale inversely with the length of the player name
        if (playerName.Count < 5)
            playerDamage = 4;
        else if (playerName.Count < 7)
            playerDamage = 3;
        else if (playerName.Count < 9)
            playerDamage = 2;
        else
            playerDamage = 1;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    
    
    void Shoot()
    {
        CancelInvoke(nameof(Reload));
            
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.parent.rotation);
        bullet.SetParent(this.transform.parent);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetLetter(playerCurrentName[^1]);
        bulletScript.SetDamage(playerDamage);
            
        RemoveLetter();
        
        if(playerCurrentName.Count > 0)
            Invoke(nameof(Reload), reloadTime * 1.5f);
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public void RemoveLetter()
    {
        // Removes last element of the playerCurrentName list
        playerCurrentName.RemoveAt(playerCurrentName.Count - 1);
        _playerNameText.text = new string(playerCurrentName.ToArray());
        Vector2 size = _collider.size;
        size = new Vector2(size.x - 0.37f, size.y);
        _collider.size = size;
    }

    void Reload()
    {
        if(playerCurrentName.Count >= playerName.Count) return;
        
        // Adds the last element of the playerName list to the playerCurrentName list
        playerCurrentName.Add(playerName[playerCurrentName.Count]);
        _playerNameText.text = new string(playerCurrentName.ToArray());
        Vector2 size = _collider.size;
        size = new Vector2(size.x + 0.37f, size.y);
        _collider.size = size;
        Invoke(nameof(Reload), reloadTime);
    }
}
