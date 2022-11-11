using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    Vector2 movementVect;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementVect.x = Input.GetAxisRaw("Horizontal");
        movementVect.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        transform.Translate(movementVect * movementSpeed * Time.fixedDeltaTime);
        Camera.main.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("test");
    }
}
