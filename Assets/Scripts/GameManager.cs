using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;

    private float _maxSpeed = 6;
    private int timeCorutene = 30;
    private float _addSpeed = 0.2f;
    private float _setJumpFors = 0.2f;

    private void Start()
    {
        StartCoroutine(SpeedIncrease());
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
