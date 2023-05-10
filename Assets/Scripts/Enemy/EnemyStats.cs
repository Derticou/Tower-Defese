using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public float startHealth = 100;
    private float health;
    public int value = 50;

    public Image healthBar;

    private bool isDead;
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
    }

    public void TakeDamage(float amount) 
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        PlayerStats.money += value;

        GameObject effect= (GameObject)Instantiate(deathEffect,transform.position,Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        
        Destroy(gameObject);
    }
}
