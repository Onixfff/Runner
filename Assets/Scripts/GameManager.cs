using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI _textCoin;
    [SerializeField] private Image _image;
    [SerializeField] private  Button _restart;
    [SerializeField] private Image _handler;


    private float _maxSpeed = 6;
    private int timeCorutene = 21;
    private float _addSpeed = 0.2f;
    private float _setJumpFors = 0.2f;
    private int _coins = 0;
    private bool isRight = false;
    private bool isLeft = false;

    private void Start()
    {
        if (DataTraining.CheckTraining())
        {
            _handler.gameObject.SetActive(false);
        }
        else
        {
            _handler.gameObject.SetActive(true);
        }

        _restart.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
        _textCoin.text = _coins.ToString();
        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        if (DataTraining.CheckTraining() == false) 
        {
            Animator handlerAnimator = _handler.gameObject.GetComponent<Animator>();
            if (SwipeController.swipeRight && isRight == false)
            {
                handlerAnimator.SetBool("Right", true);
                isRight = true;
            }

            if (SwipeController.swipeLeft && isRight == true)
            {
                handlerAnimator.SetBool("Left", true);
                isLeft = true;
            }

            if (SwipeController.swipeUp && isRight == true && isLeft == true)
            {
                _handler.gameObject.SetActive(false);
                DataTraining.EndTraining();
            }
        }
    }

    public void Finish()
    {
        _image.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
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
