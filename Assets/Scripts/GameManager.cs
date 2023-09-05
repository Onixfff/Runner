using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI _textCoin;
    [SerializeField] private Image _image;
    [SerializeField] private  Button _restart;

    private float _maxSpeed = 6;
    private int timeCorutene = 21;
    private float _addSpeed = 0.2f;
    private float _setJumpFors = 0.2f;
    private int _coins;
    private float _startSpeed;
    private float _startJumpFors;
    private float _startCoin = 0;
    private bool isDead;


    private void Start()
    {
        _restart.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
        _textCoin.text = _startCoin.ToString();
        _startJumpFors = player._jumpForce;
        _startSpeed = player._speed;
        StartCoroutine(SpeedIncrease());
    }

    public void Finish()
    {
        _image.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
        isDead = true;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Debug.Log("restart");
        isDead = false;
        Time.timeScale = 1;
        _textCoin.text = _startCoin.ToString();
        _coins = 0;
        player.EditSpeed(_startSpeed);
        player.EditJumpForce(_startJumpFors);
    }

    public void AddCoin()
    {
        _coins = _coins + 1;
        _textCoin.text = _coins.ToString();
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
