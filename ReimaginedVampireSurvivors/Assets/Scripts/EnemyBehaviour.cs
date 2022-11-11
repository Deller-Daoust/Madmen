using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;

    Rigidbody2D enemyPhys;
    Transform targetPlayer;
    Vector2 moveDirection;

    float playerDistance;

    void Awake()
    {
        enemyPhys = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPlayer)
        {
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if(targetPlayer)
        {
            enemyPhys.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("Test");
    }
}
