using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CharacterManagement : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private Transform _parentSpawnPos;
    [SerializeField] private Transform _charPos;
    [SerializeField] private SkinnedMeshRenderer _charSkin;


    private int _currentFloor = 1;

    public Transform _startPoint;
    public AnimatorController _animatorController;
    public List<GameObject> _brickList;

    public bool _canMove;
    public bool _goBrigde;

    public void AddBrick()
    {
        GameObject _newBrick = Instantiate(_brickPrefab, _parentSpawnPos.position, Quaternion.identity);
        _brickList.Add(_newBrick);
        _newBrick.transform.SetParent(_parentSpawnPos, false);

        Vector3 _pos = _newBrick.transform.position;
        //_pos.y -= 0.05f;
        _newBrick.transform.position = _pos;
    }
    public void RemoveBrick()
    {
        foreach (GameObject _brick in _brickList)
        {
            _brick.gameObject.SetActive(false);
        }
        _brickList.RemoveAt(0);

    }
    public Color GetCharacterColor()
    {
        Material _characterMaterial = transform.GetComponent<SkinnedMeshRenderer>().material;
        Color _characterColor = _characterMaterial.color;
        return _characterColor;
    }
    private void OnTriggerEnter(Collider _other)
    {
        Renderer _brickRen = _other.GetComponent<Renderer>();
        if (_other.CompareTag("Brigde"))
        {
            // doi mau khi len cau`
            _brickRen.material.color = GetCharacterColor();
            _other.GetComponent<MeshRenderer>().enabled = true;
        }
        if (_other.CompareTag("Gate"))
        {
            _currentFloor++;
            FloorManagement._instance.SpawnBrickF2(GetCharacterColor(), _currentFloor);
            _other.gameObject.SetActive(false);
        }
        if (_other.CompareTag("Victory"))
        {
            _canMove = false;
            _animatorController.PlayVictory();
        }
    }
    public IEnumerator ActivateBrick(GameObject _obj, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        _obj.SetActive(true);
    }
}
