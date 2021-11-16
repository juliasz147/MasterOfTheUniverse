using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static float health = 100f;

    private bool isDead;

    [SerializeField]
    private Image healthStats;

    public void ApplyDamage(float damage)
    {
        //if dead, don't do anything
        if (isDead)
        {
            return;
        }

        health -= damage;

        //show stats (health UI value)
        DisplayHealthStats(health);

        if (health <= 0f)
        {
            PlayerDeath();

            isDead = true;
        }
    }

    void PlayerDeath()
    {
        Invoke("RestartGame", 3f);
        
    }

    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100f;

        healthStats.fillAmount = healthValue;

    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
    }


}
