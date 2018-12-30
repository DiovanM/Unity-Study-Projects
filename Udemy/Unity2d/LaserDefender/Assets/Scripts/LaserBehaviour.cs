using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    [SerializeField] private float damage = 10;
    [SerializeField] private float speed = 20;
    private Vector2 direction; 

    private Rigidbody2D laserRigidbody;

    private void Awake()
    {
        laserRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start () {
        laserRigidbody.velocity = new Vector2(0, direction.y * speed);
        Invoke("Destroy", 5f);
	}

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<DeathManager>().Damage(damage);
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
