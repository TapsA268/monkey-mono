using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100f;
    public Slider healthBar;  // Asigna el slider de la barra de vida en el inspector
    private float currentHealth;
    private Image fillImage;
    
    void Start()
    {
        currentHealth = maxHealth;
        fillImage = healthBar.fillRect.GetComponent<Image>(); // Accede al componente de imagen
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;

            // Cambia el color en función de la salud
            if (currentHealth < maxHealth * 0.3f)
                fillImage.color = Color.red; // Cambia a rojo cuando la salud es baja
            else if (currentHealth < maxHealth * 0.6f)
                fillImage.color = Color.yellow; // Cambia a amarillo cuando la salud es media
            else
                fillImage.color = Color.green; // Cambia a verde cuando la salud es alta
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        Destroy(gameObject);
    }
}
