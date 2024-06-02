using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountingdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    
    private float waitingToStartTimer = 1f;
    private float coutingdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 10f;
    private bool isGamePaused = false;
    
    private void Awake()
    {
        Instance = this;
        
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
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
                waitingToStartTimer -= Time.deltaTime;
                
                if (waitingToStartTimer <= 0f)
                {
                    state = State.CountingdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
}
