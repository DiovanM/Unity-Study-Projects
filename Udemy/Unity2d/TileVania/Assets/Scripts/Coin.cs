using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] private AudioClip coinPickupSound;
    [SerializeField] private int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickupSound, Camera.main.transform.position);
        FindObjectOfType<GameManager>().AddScore(points);
        Destroy(gameObject);
    }
}
