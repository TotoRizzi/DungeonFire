using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Image delayedHealthBar;

    bool setDelayedHealthBar = false;

    float currentHealth;
    float maxHealth;

    float delayTime = 2f;
    float currentDelayTime = 0;

    private void Update()
    { 
        if (setDelayedHealthBar)
        {
            currentDelayTime += Time.deltaTime;
            delayedHealthBar.fillAmount = Mathf.Lerp(delayedHealthBar.fillAmount, currentHealth / maxHealth, currentDelayTime * delayTime * .05f);
            
            if(currentDelayTime >= 2)
            {
                setDelayedHealthBar = false;
            }
        }
    }

    public void SetHealthBar(float _currentHealth, float _maxHealth)
    {
        currentHealth = _currentHealth;
        maxHealth = _maxHealth;
        
        healthBar.fillAmount = currentHealth / maxHealth;
       
        setDelayedHealthBar = true;
        currentDelayTime = 0;
    }
}
