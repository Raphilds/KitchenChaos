using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;
    
    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnCuttingProgressChanged;
        
        barImage.fillAmount = 0f;
        
        Hide();
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        
        if (e.progressNormalized is 0 or >= 1f)
        {
            Hide();
        }
        else
        {
            Show();
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
