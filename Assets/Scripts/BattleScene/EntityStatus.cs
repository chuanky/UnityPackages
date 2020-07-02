using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatus : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage;
    public int defence;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setHealth(GetHealthNorm());
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.setHealth(GetHealthNorm());
    }

    public float GetHealthNorm()
    {
        return currentHealth / (float)maxHealth;
    }

    public bool ShouldDie()
    {
        return currentHealth <= 0;
    }
}
