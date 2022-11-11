using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoading : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] int chunkHorizontalCount;
    [SerializeField] int chunkVerticalCount;
    [SerializeField] float chunkSize = 20f;
    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    Vector2Int currentChunkPos = new Vector2Int(0, 0);
    Vector2Int playerChunkPos;
    Vector2Int gridPos;

    [HideInInspector]
    public GameObject[,] chunks;

    private void Awake()
    {
        chunks = new GameObject[chunkHorizontalCount, chunkVerticalCount];
    }

    private void Start()
    {
        UpdateChunks();
    }

    private void Update()
    {
        playerChunkPos.x = (int)(playerTransform.position.x / chunkSize);
        playerChunkPos.y = (int)(playerTransform.position.y / chunkSize);

        playerChunkPos.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerChunkPos.y -= playerTransform.position.y < 0 ? 1 : 0;

        if(currentChunkPos != playerChunkPos)
        {
            currentChunkPos = playerChunkPos;

            gridPos.x = GetGridPos(gridPos.x, true);
            gridPos.y = GetGridPos(gridPos.y, false);
            UpdateChunks();
        }
    }

    private void UpdateChunks()
    {
        for(int fov_x = -(fieldOfVisionWidth / 2); fov_x <= fieldOfVisionWidth / 2; fov_x++)
        {
            for(int fov_y = -(fieldOfVisionHeight / 2); fov_y <= fieldOfVisionHeight / 2; fov_y++)
            {
                int updateChunk_x = GetGridPos(playerChunkPos.x + fov_x, true);
                int updateChunk_y = GetGridPos(playerChunkPos.y + fov_y, false);

                GameObject chunk = chunks[updateChunk_x, updateChunk_y];
                chunk.transform.position = CalcGridPos(playerChunkPos.x + fov_x, playerChunkPos.y + fov_y);
            }
        }
    }

    private Vector3 CalcGridPos(int x, int y)
    {
        return new Vector3(x * chunkSize, y * chunkSize, 0f);
    }

    public int GetGridPos(float gridValue, bool horizontal)
    {
        if(horizontal)
        {
            if(gridValue >= 0)
            {
                gridValue = gridValue % chunkHorizontalCount;
            }
            else
            {
                gridValue += 1;
                gridValue = chunkHorizontalCount -1 + gridValue % chunkHorizontalCount;
            }
        }
        else
        {
            if(gridValue >= 0)
            {
                gridValue = gridValue % chunkVerticalCount;
            }
            else
            {
                gridValue += 1;
                gridValue = chunkVerticalCount -1 + gridValue % chunkVerticalCount;
            }
        }

        return (int)gridValue;
    }

    public void Add(GameObject chunkOb, Vector2Int chunkPos)
    {
        chunks[chunkPos.x, chunkPos.y] = chunkOb;
    }
}
