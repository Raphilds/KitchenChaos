using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private static string TIME_SELECTED_KEY = "TimeSelected";
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMP_Dropdown timeDropdown;

    private int timeSelected;
    
    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        
        timeDropdown.onValueChanged.AddListener((value) =>
        {
            switch (value)
            {
                case 0: // 1 minute 
                    timeSelected = 60;
                    break;
                case 1: // 2 minutes
                    timeSelected = 60 * 2;
                    break;
                case 2: // 5 minutes
                    timeSelected = 60 * 5;
                    break;
                case 3: // 10 minutes
                    timeSelected = 60 * 10;
                    break;
                case 4: // Infinite
                    timeSelected = 0;
                    break;
            }
            
            PlayerPrefs.SetInt(TIME_SELECTED_KEY, timeSelected);
            PlayerPrefs.Save();
        });
        
        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(TIME_SELECTED_KEY))
        {
            PlayerPrefs.SetInt(TIME_SELECTED_KEY, 60);
            PlayerPrefs.Save();

            return;
        }
        
        timeSelected = PlayerPrefs.GetInt(TIME_SELECTED_KEY, 60);
        timeDropdown.value = GetIndexFromTimeSelected();
    }

    private int GetIndexFromTimeSelected()
    {
        switch (timeSelected)
        {
            case 60:
                return 0;
            case 60 * 2:
                return 1;
            case 60 * 5:
                return 2;
            case 60 * 10:
                return 3;
            case 0:
                return 4;
            default:
                return 60;
        }
    }
}
