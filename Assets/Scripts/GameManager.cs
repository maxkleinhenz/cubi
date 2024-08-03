using System.Collections;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("GameOver")] public TimeManager TimeManager;
    public Text TimeCounter;
    public Text HighscoreText;
    public Text GameOverText;
    public GameObject GameOverUi;
    public Button RestartButton;

    [Header("Obstacle per Wave")] public float ObstaclePerWaveTimer = 20f;
    public float MinObstaclePerWave = 1;
    public int MaxObstaclePerWave = 4;
    public int MaxMinObstaclePerWave = 6;
    public int MaxMaxObstaclePerWave = 8;

    private const string HighscoreKey = "Highscore";
    private float _highscore = 0f;

    private AudioSource _audioSource;

    public GameState GameState { get; private set; }

    public bool IsCountdown
    {
        get { return GameState == GameState.Countdown; }
    }

    public bool IsRunning
    {
        get { return GameState == GameState.Running; }
    }

    public bool IsGameEnded
    {
        get { return GameState == GameState.Ended; }
    }

    public bool CanSpawnObstacle
    {
        get { return GameState == GameState.Running; }
    }

    private float _time;
    private float _startMinObstaclePerWaveTimer;
    private bool _isNewHighscore;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    public void Start()
    {
        TimeManager.Realtime();
        GameState = GameState.Countdown;
        _time = 0;
        DisplayTime();

        _highscore = LoadHighscore();
        _startMinObstaclePerWaveTimer = ObstaclePerWaveTimer;
    }

    // Update is called once per frame
    public void Update()
    {
        if (IsRunning)
        {
            if (!IsGameEnded && TimeCounter != null)
            {
                _time += Time.deltaTime;
                DisplayTime();
            }

            ObstaclePerWaveTimer -= Time.deltaTime;
            if (ObstaclePerWaveTimer <= 0f)
            {
                MinObstaclePerWave++;
                MaxObstaclePerWave++;

                MinObstaclePerWave = Mathf.Clamp(MinObstaclePerWave, 0, MaxMinObstaclePerWave);
                MaxObstaclePerWave = Mathf.Clamp(MaxObstaclePerWave, 0, MaxMaxObstaclePerWave);

                ObstaclePerWaveTimer = _startMinObstaclePerWaveTimer;
            }
        }
    }

    private void DisplayTime()
    {
        if (TimeCounter != null)
            TimeCounter.text = _time.ToString("0.00");
    }

    public void EndGame(bool playGameOverSound = true)
    {
        GameState = GameState.Ended;
        TimeManager.SlowDown();

        SetHighscore(_time);
        DisplayHighscore();
        DisplayGameOverText();

        if (playGameOverSound)
            _audioSource.Play();

        GameOverUi.SetActive(true);

        EventSystem.current.SetSelectedGameObject(RestartButton.gameObject, new PointerEventData(EventSystem.current));
    }

    public void Restart()
    {
        TimeManager.Realtime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        TimeManager.Realtime();
        SceneManager.LoadScene(0);
    }

    public void SetRunning()
    {
        GameState = GameState.Running;
    }

    private void DisplayGameOverText()
    {
        GameOverText.text = _isNewHighscore ? "New Highscore" : "Game Over";
    }

    private void DisplayHighscore()
    {
        HighscoreText.text = "Highscore: " + _highscore.ToString("0.00");
    }

    private float LoadHighscore()
    {
        var highscore = PlayerPrefs.GetFloat(HighscoreKey, 0f);
        return highscore;
    }

    private void SetHighscore(float time)
    {
        if (_highscore < time)
        {
            PlayerPrefs.SetFloat(HighscoreKey, _time);
            _isNewHighscore = true;
        }
        else
        {
            _isNewHighscore = false;
        }
    }
}
