using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _tilePrefabs;
    private float _spawnPos = 0;
    private float _tileLength = 50f;

    Transform[] _tiles = new Transform[6];

    [SerializeField] private Transform _player;
    private int _startTiles = 6;
    private int indexTiles = 0;

    private void Awake()
    {
        for (int i = 0; i < _startTiles; i++)
        {
            SpawnTile(Random.Range(0, _tilePrefabs.Length));
        }
    }

    void Update()
    {
        if (_player.position.z > _spawnPos - (_startTiles * _tileLength))
        {
            //SpawnTile(Random.Range(0, _tilePrefabs.Length));
            EditPositionTile();
            _spawnPos += _tileLength;
        }
    }

    private void EditPositionTile()
    {
        if(_player.position.z > _spawnPos  - (_startTiles * _tileLength))
        {
            Debug.Log($"SpawnPos: {_spawnPos} \n{_spawnPos - (_startTiles * _tileLength)}");
            
            if(indexTiles == _tiles.Length)
            {
                indexTiles = 0;
            }
            _tiles[indexTiles].position = transform.forward * _spawnPos;
            indexTiles++;
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject tile = Instantiate(_tilePrefabs[tileIndex], transform.forward * _spawnPos, transform.rotation);
        _tiles[indexTiles] = tile.GetComponent<Transform>();
        indexTiles++;
        _spawnPos += _tileLength;
    }
}
