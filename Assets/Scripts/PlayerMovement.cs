using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackDamage = 10f; // Daño de ataque del jugador
    public float attackRange = 1.5f; // Rango de ataque del jugador
    public float defenseMultiplier = 1f; //Multiplicador de defensa
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private HealthBar healthBar;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthBar = GetComponent<HealthBar>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(movement.x < 0 ? -1 : 1, 1, 1);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking",true);
            Attack();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.CompareTag("Enemy"))
            {
                IA_Enemy enemyScript = enemy.GetComponent<IA_Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(attackDamage);
                }
            }
        }

        Invoke("EndAttack",0.3f);
    }

    void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking",false);
    }

    public void TakeDamage(float damage)
    {
        float finalDamage=damage*defenseMultiplier;
        healthBar.TakeDamage(finalDamage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
