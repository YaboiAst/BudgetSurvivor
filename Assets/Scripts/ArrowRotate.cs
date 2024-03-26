using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotate : MonoBehaviour
{
    // Makes the game object attached to this script rotate towards the mouse position
    
    private Camera _mainCamera;
    private Vector3 _mousePosition;
    private Vector3 _objectPosition;
    private float _angle;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _objectPosition = transform.position;
        
        _mousePosition.z = 0;
        _objectPosition.z = 0;
        
        _angle = Mathf.Atan2(_mousePosition.y - _objectPosition.y, _mousePosition.x - _objectPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, _angle -90f);
        
    }
}
