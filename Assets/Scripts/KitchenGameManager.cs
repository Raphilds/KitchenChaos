using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    private static string TIME_SELECTED_KEY = "TimeSelected";
    
    public static KitchenGameManager Instance { get; private set; }
    
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    
    [SerializeField] private float gamePlayingTimerMax = 60f;

    private enum State
    {
        WaitingToStart,
        CountingdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    
    private float coutingdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private bool isGamePaused = false;
    
    private void Awake()
    {
        Instance = this;
        
        state = State.WaitingToStart;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(TIME_SELECTED_KEY))
        {
            gamePlayingTimerMax = PlayerPrefs.GetInt(TIME_SELECTED_KEY);
        }
        
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        
        GameInput.Instance.OnInteractionAction += GameInput_OnInteractionAction;
    }

    private void GameInput_OnInteractionAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountingdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                
                break;
            case State.CountingdownToStart:
                coutingdownToStartTimer -= Time.deltaTime;
                
                if (coutingdownToStartTimer <= 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                if (IsInfiniteTimeSelected())
                {
                    return;
                }
                
                gamePlayingTimer -= Time.deltaTime;
                
                if (gamePlayingTimer <= 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    
    public bool IsCountingdownToStartActive()
    {
        return state == State.CountingdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return coutingdownToStartTimer;
    }
    
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    
    public float GetGamePlayingTimerNormalized()
    {
        if (IsInfiniteTimeSelected())
        {
            return 1;
        }
        
        return 1 - gamePlayingTimer / gamePlayingTimerMax;
    }
    
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        
        if (isGamePaused)
        {
            Time.timeScale = 0;
            
            OnGamePaused?.Invoke(this, EventArgs.Empty);

            return;
        }
        
        Time.timeScale = 1;
        
        OnGameUnpaused?.Invoke(this, EventArgs.Empty);
    }
    
    private bool IsInfiniteTimeSelected()
    {
        return gamePlayingTimerMax == 0;
    }
}
