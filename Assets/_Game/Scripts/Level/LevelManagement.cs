using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ColorManagement;

public class LevelManagement : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private Transform _mapBrick;
    [SerializeField] private float _gridSizeX;
    [SerializeField] private float _gridSizeZ;
    [SerializeField] private float _spacing;

    private ColorSO _colorSO;
    void Start()
    {
        BuildBrickGrid();
    }
    void Update()
    {

    }
    private void BuildBrickGrid()
    {
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int z = 0; z < _gridSizeZ; z++)
            {
                Vector3 _position = _mapBrick.position + new Vector3(x * _spacing, 0, z * _spacing); 
                GameObject _brick = Instantiate(_brickPrefab, _position, Quaternion.identity);
                
            }
        }
    }
    private void ChangeColorType(ColorType color)
    {
        Material materialColor = _colorSO.GetMaterial(color);
        if (materialColor != null)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            renderer.material = materialColor;
        }
    }
}


