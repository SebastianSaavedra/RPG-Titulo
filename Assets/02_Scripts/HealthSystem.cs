using System;
public class HealthSystem
{
    public event EventHandler onHealthChanged;
    public event EventHandler onDead;

    private int health;
    private int maxHealth;

    public HealthSystem(int healthMax)
    {
        this.maxHealth = healthMax;
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / maxHealth;
    }

    public void Damage(int dmgAmount)
    {
        health -= dmgAmount;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        if (onHealthChanged != null)
        {
            onHealthChanged(this,EventArgs.Empty);
        }
    }

    public void Die()
    {
        if(onDead != null)
        {
            onDead(this,EventArgs.Empty);
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (onHealthChanged != null)
        {
            onHealthChanged(this, EventArgs.Empty);
        }
    }

}
