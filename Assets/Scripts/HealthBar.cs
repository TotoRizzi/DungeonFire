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

    private void Update()
    { 
        /*
        if (setDelayedHealthBar)
        {
            currentDelayTime += Time.deltaTime;
            delayedHealthBar.fillAmount = Mathf.Lerp(delayedHealthBar.fillAmount, currentHealth / maxHealth, currentDelayTime * delayTime * .05f);
            
            if(currentDelayTime >= 2)
            {
                setDelayedHealthBar = false;
            }
        }
        */
    }

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
        while (delayedHealthBar.fillAmount >= currentHealth / maxHealth)
        {
            delayedHealthBar.fillAmount = Mathf.Lerp(delayedHealthBar.fillAmount, currentHealth / maxHealth, delayTime * Time.deltaTime);
            Debug.Log("Tuki");
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        yield return null;
    }
}
