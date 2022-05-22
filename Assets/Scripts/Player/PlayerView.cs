using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Image delayedHealthBar;

    float currentHealth;
    float maxHealth;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void LifeChange(float amount)
    {
        healthBar.fillAmount = amount;
    }

    public void KnockBack()
    {
        anim.SetTrigger("GetHit");
    }

    public void Shoot()
    {
        Debug.Log("Shooting");
        anim.SetTrigger("Shoot");
    }

    public void Movement(Vector3 v)
    {
        anim.SetFloat("Velocity", v.magnitude);
    }

    public void Death()
    {
        anim.SetTrigger("Die");
    }

    public void SetHealthBar(float _currentHealth, float _maxHealth)
    {
        maxHealth = _maxHealth;
        currentHealth = _currentHealth;

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
