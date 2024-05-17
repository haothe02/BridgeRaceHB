using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CharacterManagement
{
    private enum State
    {
        _Picking,
        _GoingBrigde
    }

    [SerializeField] private NavMeshAgent _enemyNavAgent;
    [SerializeField] private List<GameObject> _enemyBrick;
    [SerializeField] private Transform _lastPos;

    private Renderer _brickRen;
    private Vector3 _brickPos;
    private State _state;
    private float _closeBrick;
    private float _minDistance = float.MaxValue;

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = _startPoint.position;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        _state = State._Picking;
    }
    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        
    }
    private void EnemyMove()
    {
        //if (_brickList.Count >= 5)
        //{
        //    _state = State._GoingBrigde;
        //    ChangeState();
        //}
        if (_brickList.Count == 0)
        {
            _state = State._Picking;
            ChangeState();
        }
    }
    private void FindBrick()
    {
        for (int i = 0; i < _enemyBrick.Count; i++)
        {
            _brickRen = _enemyBrick[i].GetComponent<Renderer>();
            _closeBrick = Vector3.Distance(_brickRen.transform.position, transform.position);
            if (_brickRen.material.color == GetCharacterColor() && _brickRen.enabled && _closeBrick < _minDistance)
            {
                _minDistance = _closeBrick;
                _brickPos = _brickRen.transform.position;
                _enemyNavAgent.SetDestination(_brickPos);
                _animatorController.PlayRun();
            }
        }
    }
    //private static void Shuffle (List<GameObject> list)
    //{
    //    Random rng = new Random();
    //    int n = list.Count;
    //    while (n > 1)
    //    {
    //        n--;
    //        int k = rng.Next(n + 1);
    //        GameObject value = list[k];
    //        list[k] = list[n];
    //        list[n] = value;
    //    }
    //}
    private void ChangeState()
    {
        switch (_state)
        {
            default:
            case State._Picking:
                FindBrick();
                break;
            case State._GoingBrigde:
                _enemyNavAgent.SetDestination(_lastPos.position);
                _animatorController.PlayRun();
                break;
        }
    }
    private void OnTriggerEnter(Collider _other)
    {
        _brickRen = _other.GetComponent<Renderer>();
        if (_brickRen.material.color == GetCharacterColor())
        {
            AddBrick();
            _other.gameObject.SetActive(false);
            StartCoroutine(ActivateBrick(_other.gameObject, 3f));
        }
        if (_other.CompareTag("Brigde"))
        {
            RemoveBrick();
        }
    }
}
