using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunks : MonoBehaviour
{
    [SerializeField] Vector2Int chunkPos;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<ChunkLoading>().Add(gameObject, chunkPos);
    }
}
