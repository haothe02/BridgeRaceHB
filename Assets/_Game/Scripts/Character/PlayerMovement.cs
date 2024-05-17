using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : CharacterManagement
{
    [SerializeField] private VariableJoystick _joystick;
    //[SerializeField] private AnimatorController _animatorController;
    [SerializeField] private Transform _rayPos;
    [SerializeField] private LayerMask _brigdeLayer;

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _movementSpeed;

    private RaycastHit _hit;
    private Rigidbody _rigidbody;
    private Vector3 _moveVector;


    private void Awake()
    {
        _canMove = true;
        _goBrigde = false;
        _rigidbody = GetComponent<Rigidbody>();
        transform.position = _startPoint.position;
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);

            _animatorController.PlayRun();
        }
        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            _animatorController.PlayIdle();
        }
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Vertical * _movementSpeed * Time.deltaTime;
        _moveVector.z = -_joystick.Horizontal * _movementSpeed * Time.deltaTime;
        if (_canMove)
        {
            _rigidbody.MovePosition(_rigidbody.position + _moveVector);
            //CollectBrick();
        }
        if (_goBrigde)
        {
            MoveOnBrigde();
        }

    }
    private void MoveOnBrigde()
    {
        Physics.Raycast(_rayPos.position, -Vector3.up, out _hit, _brigdeLayer);
        MeshRenderer _brigdeRenderer = _hit.collider.GetComponent<MeshRenderer>();
        //Debug.DrawRay(_newPos, -Vector3.up, Color.red, 1000);
        if (transform.forward.x > 0)
        {
            if (_brickList.Count > 0 && !_brigdeRenderer.enabled)
            {
                RemoveBrick();
                _brigdeRenderer.enabled = true;
            }
            else if (_brickList.Count == 0 && !_brigdeRenderer.enabled)
            {
                _canMove = false;
                _animatorController.PlayIdle();
            }
        }
        else
        {
            _canMove = true;
        }
    }
    //private void CollectBrick()
    //{
    //    for (int i = 0; i < _brickList.Count; i++)
    //    {
    //        Material _brick = _brickList[i].GetComponent<Material>();
    //        if(_brick.color == GetCharacterColor())
    //        {
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Blue"))
        {
            _other.gameObject.SetActive(false);
            StartCoroutine(ActivateBrick(_other.gameObject, 3f));
            AddBrick();
        }
        if (_other.CompareTag("Brigde"))
        {
            _goBrigde = true;
        } else
        {
            _goBrigde = false;
        }
    }

    
}
    
