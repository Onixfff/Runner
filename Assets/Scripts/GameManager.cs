using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;

    private float _maxSpeed = 6;
    private int timeCorutene = 21;
    private float _addSpeed = 0.2f;
    private float _setJumpFors = 0.2f;
    private int _coins;
    [SerializeField] private GameObject losePanel;

    private void Start()
    {
        StartCoroutine(SpeedIncrease());
    }

    public void AddCoin()
    {
        _coins = _coins + 1;
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(timeCorutene);
        if(_maxSpeed > player._speed)
        {
            player.EditSpeed(player._speed + _addSpeed);
            player.EditJumpForce(player._jumpForce - _setJumpFors);
            StartCoroutine(SpeedIncrease());
        }
    }
}
