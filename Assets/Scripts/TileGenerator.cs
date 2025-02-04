using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _tilePrefabs;
    [SerializeField] private Transform _player;
    
    private List<GameObject> _tiles = new List<GameObject>();
    private float _spawnPos = 0;
    private float _tileLength = 50f;
    private int _startTiles = 8;


    private void Awake()
    {
        for (int i = 0; i < _startTiles; i++)
        {
            if(i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(1, _tilePrefabs.Length));
            }
        }
    }

    void Update()
    {
        if (_player.position.z > _spawnPos + _tileLength - ((_startTiles * _tileLength)))
        {
            SpawnTile(Random.Range(1, _tilePrefabs.Length));
            DestroyTiles();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject tile = Instantiate(_tilePrefabs[tileIndex], transform.forward * _spawnPos, Quaternion.identity);
        _tiles.Add(tile);
        _spawnPos += _tileLength;
    }

    private void DestroyTiles()
    {
        Destroy(_tiles[0]);
        _tiles.Remove(_tiles[0]);
    }
}
