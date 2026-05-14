using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBase : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar;

    private Renderer enemyRenderer;
    private Color originalColor;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        enemyRenderer = GetComponentInChildren<Renderer>();

        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = Color.red;

            yield return new WaitForSeconds(0.15f);

            enemyRenderer.material.color = originalColor;
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}