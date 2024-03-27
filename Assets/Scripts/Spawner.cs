using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    [SerializeField] private float waveInterval = 5f;
    private WaitForSeconds intervalInSeconds;

    [SerializeField] private float waveSize = 4;
    private float difficultyModifier = 1f;

    private void OnEnable() {
        StartCoroutine(WaveSpawn());

        intervalInSeconds = new WaitForSeconds(waveInterval);
    }   

    IEnumerator WaveSpawn(){
        while(true){
            var difficultyIncreaseChance = Random.Range(0, 50);
            if(difficultyIncreaseChance > 25) difficultyModifier += 0.5f;

            var spawnAmount = (int) difficultyModifier * Random.Range(1, waveSize);
            var maxCoords = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            var minCoords = Camera.main.ScreenToWorldPoint(Vector2.zero);

            for(int i = 0; i < waveSize; i++){
                float xPos, yPos;
                if(Random.Range(0, 50) > 25)
                    xPos = Random.Range(maxCoords.x, 2*maxCoords.x);
                else
                    xPos = Random.Range(minCoords.x, 2*minCoords.x);

                if(Random.Range(0, 50) > 25)
                    yPos = Random.Range(maxCoords.y, 2*maxCoords.y);
                else
                    yPos = Random.Range(minCoords.y, 2*minCoords.y);

                var newPos = new Vector3(xPos, yPos, 0f);

                Instantiate(enemyPrefab, newPos, transform.rotation, transform);
            }

            yield return intervalInSeconds;
        }
    }
}
