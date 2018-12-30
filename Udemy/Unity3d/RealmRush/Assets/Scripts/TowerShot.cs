using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TowerShot : MonoBehaviour
{

    private Transform targetPosition;
    private Rigidbody myRigidbody;
    private DamageDealer myDamageDealer;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myDamageDealer = GetComponent<DamageDealer>();
    }

    public void OnSpawn(Transform target)
    {
        targetPosition = target;
        Move();
    }

    public void Move()
    {
        transform.LookAt(targetPosition);
        Vector3 velocity = new Vector3(transform.position.x - targetPosition.position.x,
                                           transform.position.y - targetPosition.position.y,
                                           transform.position.z - targetPosition.position.z);

        myRigidbody.velocity = -velocity.normalized * Time.deltaTime * 800;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<DamageManager>().TakeDamage(myDamageDealer.damage);
            Destroy(gameObject);
        }
    }

}
