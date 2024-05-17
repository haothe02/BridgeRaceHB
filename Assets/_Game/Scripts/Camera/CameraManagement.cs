using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManagement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    //private Transform _camera;

    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;

    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _player.position + _offset, Time.deltaTime * _speed);
    }
}
