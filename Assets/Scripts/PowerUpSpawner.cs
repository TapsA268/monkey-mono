using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public List<Transform> spawnPoints;
    public float spawnInterval = 8f;

    private void Start(){
        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnPowerUps(){
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp(){
        Transform spawnPoint=spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject newPowerUp=Instantiate(powerUpPrefab,spawnPoint.position,Quaternion.identity);

        PowerUp powerUpScript=newPowerUp.GetComponent<PowerUp>();
        powerUpScript.powerUpType=(PowerUp.PowerUpType)Random.Range(0,3);

        powerUpScript.AssignSprite();
    }
}