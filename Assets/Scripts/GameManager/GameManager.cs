using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Button restartGameButton;
    [SerializeField] Text gameStateText,timerText;
    private float currentTime;
    [SerializeField] float totalTime;
    public bool isGameStart;
    private bool keepTiming;
    private void Awake()
    {
        currentTime = totalTime;
        timerText.text = currentTime.ToString();
        Cursor.lockState = CursorLockMode.Locked;
        isGameStart = false;
        if (Instance == null)
            Instance = this;
    }
    private void OnEnable()
    {
        KeyPickUp.OnGameWin += GameSuccessEvent;
    }
    private void Start()
    {
        StartTimer();
        AudioManager.instance.Play("BackgroundMusic");
    }
    private void Update()
    {
        DisplayTimer(currentTime);
        if (keepTiming)
        {
            UpdateTime();
        }
        
       
        if (PlayerHealth.Instance.isGameOver)
        {
            GameOverState("GAME OVER");
        }
    }
    float StopTimer()
    {
        keepTiming = false;
        return currentTime;
    }
    void StartTimer()
    {
        keepTiming = true;
    }
    private void UpdateTime()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            PlayerHealth.Instance.isGameOver = true;
            currentTime = 0;
        }
    }


    private void GameOverState(string result)
    {
        isGameStart = false;
        gameStateText.gameObject.SetActive(true);
        gameStateText.text = result;
        restartGameButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        StopTimer();


    }
    private void GameSuccessEvent()
    {
        GameOverState("GAME WIN");

    }


    private void DisplayTimer(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float seconds = Mathf.FloorToInt(timeToDisplay );
        timerText.text = seconds.ToString(); ;
    }

    public void RestartGame()
    {
        PlayerHealth.Instance.isGameOver = false;
        PlayerHealth.Instance.Health = PlayerHealth.Instance.maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        isGameStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }

   

    private void OnDisable()
    {
        KeyPickUp.OnGameWin -= GameSuccessEvent;
    }

}
