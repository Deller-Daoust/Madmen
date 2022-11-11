using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] GameObject enemyOb;
    [SerializeField] GameObject playerOb;
    [SerializeField]ChunkLoading ChunkLoader;

    public GameObject currentChunk;

    Vector2Int gridPos;

    public int xPosition;
    public int yPosition;

    public int numOfEnemies;

    int[] validNumbers = new int[12] {-10 , -9, -8, -7, -6, -5, 5, 6, 7, 8, 9, 10};

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawn()
    {
        while(numOfEnemies < 100)
        {
            yield return new WaitForSeconds(5f);

            int plusOrMinus = Random.Range(0, 2) * 2-1;

            xPosition = Random.Range(0, 15) * plusOrMinus;

            if(xPosition > 5 || xPosition < -5)
            {
                yPosition = Random.Range(0, 10) * plusOrMinus;
            }
            else
            {
                yPosition = validNumbers[Random.Range(0, validNumbers.Length)];
            }

            Vector3 relativeToPlayer = new Vector3(playerOb.transform.position.x + xPosition, playerOb.transform.position.y + yPosition, 0);

            gridPos.x = ChunkLoader.GetGridPos(xPosition, true);
            gridPos.y = ChunkLoader.GetGridPos(yPosition, false);

            currentChunk = ChunkLoader.chunks[gridPos.x, gridPos.y];
            
            var newEnemy = Instantiate(enemyOb, relativeToPlayer, Quaternion.identity, currentChunk.transform);
            newEnemy.name = $"Enemy{numOfEnemies}";

            numOfEnemies++;
        }
    }
}
