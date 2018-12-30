using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed *= 10;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 velocity = new Vector2(moveSpeed * Time.deltaTime * transform.localScale.x, myRigidbody.velocity.y);
        myRigidbody.velocity = velocity;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale *= new Vector2(-1, 1);
    } 

}
