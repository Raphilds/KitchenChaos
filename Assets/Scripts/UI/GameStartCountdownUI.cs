using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private static int NUMBER_POPUP = Animator.StringToHash("NumberPopup");
    
    [SerializeField] private TextMeshProUGUI countdownText;
    
    private Animator animator;
    
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountingdownToStartActive())
        {
            Show();
            
            return;
        }
        
        Hide();
    }

    private void Update()
    {
        if (KitchenGameManager.Instance.IsCountingdownToStartActive())
        {
            int countdownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
            //countdownText.text = KitchenGameManager.Instance.GetCountdownToStartTimer().ToString("0");
            countdownText.text = countdownNumber.ToString();
            
            if (previousCountdownNumber != countdownNumber)
            {
                previousCountdownNumber = countdownNumber;
                
                animator.SetTrigger(NUMBER_POPUP);
                
                SoundManager.Instance.PlayCountdownSound();
            }
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
