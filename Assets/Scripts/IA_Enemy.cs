using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Enemy : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float moveSpeed = 2f; // Velocidad de movimiento del enemigo
    public float attackRange = 1.5f; // Distancia para atacar
    public float attackCooldown = 1f; // Tiempo entre ataques
    public float enemyAttackDamage = 20f; // Daño del enemigo

    private HealthBar healthBar;
    private float attackTimer = 0f;
    private Animator animator;
    private bool isAttacking = false;
    
    void Start()
    {
        healthBar = GetComponent<HealthBar>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                if (attackTimer <= 0f)
                {
                    animator.SetBool("isAttacking",true);
                    animator.SetBool("isWalking", false);
                    isAttacking = true;
                    AttackPlayer();
                    attackTimer = attackCooldown;
                }                
            }
            else
            {
                MoveTowardsPlayer();
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }

            attackTimer -= Time.deltaTime;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // Mueve al enemigo hacia el jugador
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        // Ajusta la escala para voltear al enemigo hacia el jugador
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Voltea a la izquierda
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // Voltea a la derecha
        }
    }

    void AttackPlayer()
    {
        if(player.CompareTag("Player"))
        {
            PlayerMovement Player = player.GetComponent<PlayerMovement>();
            if (Player != null)
            {
                Player.TakeDamage(enemyAttackDamage);
            }
        }

        Invoke("EndAttack",0.3f);
    }

    void EndAttack()
    {
        animator.SetBool("isAttacking",false);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        healthBar.TakeDamage(damage);
    }
}
