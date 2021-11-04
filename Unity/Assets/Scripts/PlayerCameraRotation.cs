using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity = 500f;
    [SerializeField] private Transform playerBody;
    private Vector2 _mouseInput;
    private float _xRotation;

    private const float MaxYDegree = 20f;
    private const float MinYDegree = -45f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * (sensitivity * Time.deltaTime);
        _xRotation -= _mouseInput.y;

        _xRotation = Mathf.Clamp(_xRotation, MinYDegree, MaxYDegree);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * _mouseInput.x); 
    }
}
