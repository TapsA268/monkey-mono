using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType{Defense,Health,Speed}
    public PowerUpType powerUpType;
    public float duration = 5f;
    public float healthAmount = 20f;
    public float defenseMultiplier = 0.5f;
    public float speedMultiplier = 1.5f;

    public Sprite defenseSprite;
    public Sprite healthSprite;
    public Sprite speedSprite;

    private SpriteRenderer spriteRender;

    private void Awake(){
        spriteRender=GetComponent<SpriteRenderer>();
        AssignSprite();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            PlayerMovement player=other.GetComponent<PlayerMovement>();
            if (player!=null)
            {
                ActivatePowerUp(player);
                Destroy(gameObject);
            }
        }
    }

    public void AssignSprite(){
        switch (powerUpType)
        {
            case PowerUpType.Defense:
                spriteRender.sprite=defenseSprite;
                break;
            case PowerUpType.Health:
                spriteRender.sprite=healthSprite;
                break;
            case PowerUpType.Speed:
                spriteRender.sprite=speedSprite;
                break;
        }
    }

    void ActivatePowerUp(PlayerMovement player){
        switch (powerUpType)
        {
            case PowerUpType.Defense:
                StartCoroutine(ActivateDefense(player));
                break;
            case PowerUpType.Health:
                ActivateHealth(player);
                break;
            case PowerUpType.Speed:
                StartCoroutine(ActivateSpeed(player));
                break;
        }
    }

    IEnumerator ActivateDefense(PlayerMovement player){
        player.defenseMultiplier=defenseMultiplier;
        yield return new WaitForSeconds(duration);
        player.defenseMultiplier=1f;
    }

    void ActivateHealth(PlayerMovement player){
        player.TakeDamage(-healthAmount);
    }

    IEnumerator ActivateSpeed(PlayerMovement player){
        player.moveSpeed*=speedMultiplier;
        yield return new WaitForSeconds(duration);
        player.moveSpeed/=speedMultiplier;
    }
}