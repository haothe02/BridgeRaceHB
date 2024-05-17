using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static ColorManagement;

public class FloorManagement : MonoBehaviour
{
    [SerializeField] private List<GameObject> _brickListFloor1;
    [SerializeField] private List<GameObject> _brickListFloor2;
    [SerializeField] private List<GameObject> _brickListFloor3;
    //[SerializeField] private GameObject _brickPrefab;
    //[SerializeField] private Transform _mapBrick;

    //[SerializeField] private float _gridSizeX;
    //[SerializeField] private float _gridSizeZ;
    //[SerializeField] private float _spacing;

    //private ColorSO _colorSO;
    //private int _currentFloor;

    public static FloorManagement _instance;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        SpawnBrickF1();
    }
    void Update()
    {

    }
    private void SpawnBrickF1()
    {
        for (int i = 0; i < _brickListFloor1.Count; i++)
        {
            Renderer _brickRenderer = _brickListFloor1[i].GetComponent<Renderer>();
            if (!_brickRenderer.enabled)
            {
                _brickRenderer.enabled = true;
            }
        }
    }
    public void SpawnBrickF2(Color _characterColor, int _currentFloor)
    {
        List<GameObject> _brickListFloor = (_currentFloor == 2) ? _brickListFloor2 : _brickListFloor3;
        foreach (GameObject brick in _brickListFloor)
        {
            Renderer _brickRenderer = brick.GetComponent<Renderer>();
            Color _brickColor = _brickRenderer.material.color;
            _brickRenderer.enabled = (_brickColor == _characterColor);
        }
    }
    //private void BrickMaker()
    //{
    //    for (int x = 0; x < _gridSizeX; x++)
    //    {
    //        for (int z = 0; z < _gridSizeZ; z++)
    //        {
    //            Vector3 _position = _mapBrick.position + new Vector3(x * _spacing, 0, z * _spacing);
    //            GameObject brick = Instantiate(_brickPrefab, _position, Quaternion.identity);
    //            ChangeColorType(brick, ColorType.Red);
    //        }
    //    }
    //}
    //private void ChangeColorType(GameObject brick, ColorType color)
    //{
    //    Renderer renderer = brick.GetComponent<Renderer>();
    //    if (renderer != null)
    //    {
    //        Material materialColor = _colorSO.GetMaterial(color);
    //        renderer.material = materialColor;
    //    }
    //}
}

