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
        if (playerCurrentName.Count > 0) // Remove when die
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.parent.rotation);
            bullet.SetParent(this.transform.parent);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetLetter(playerCurrentName[^1]);
            bulletScript.SetDamage(playerDamage);
            
            
            // Removes last element of the playerCurrentName list
            playerCurrentName.RemoveAt(playerCurrentName.Count - 1);
            _playerNameText.text = new string(playerCurrentName.ToArray());
            Vector2 size = _collider.size;
            size = new Vector2(size.x - 0.37f, size.y);
            _collider.size = size;
            Invoke(nameof(Reload), reloadTime * 1.5f);
        }
        else
        {
            Debug.Log("No more bullets");
        }
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
