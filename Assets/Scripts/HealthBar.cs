using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Image delayedHealthBar;

    bool setDelayedHealthBar = false;

    float currentHealth;
    float maxHealth;

    float delayTime = 2f;

    public void SetHealthBar(float _currentHealth, float _maxHealth)
    {
        currentHealth = _currentHealth;
        maxHealth = _maxHealth;
        
        healthBar.fillAmount = currentHealth / maxHealth;

        //setDelayedHealthBar = true;
        StartCoroutine(SetDelayedHB());
    }

    IEnumerator SetDelayedHB()
    {
        float ticks = 0;
        float initialFillAmount = delayedHealthBar.fillAmount;
        while (ticks < 1)
        {
            ticks += Time.deltaTime;
            delayedHealthBar.fillAmount = Mathf.Lerp(initialFillAmount, currentHealth / maxHealth, ticks);
            yield return new WaitForEndOfFrame();
        }
        
        yield return null;
    }
}
