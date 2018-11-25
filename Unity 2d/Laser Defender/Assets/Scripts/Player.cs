using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float life = 50;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject laser;
    [SerializeField] private float timeBetweenShots = 0.1f;
    [SerializeField] GameObject explosionParticle;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] [Range(0, 1)] private float shootSoundVolume;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] [Range(0, 1)] private float deathSoundVolume; 

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private Coroutine shootCoroutine;    

    // Use this for initialization
    void Start () {
        GetComponent<DeathManager>().life = life;
        GetComponent<DeathManager>().Death += Die;
        MoveBoundaries();
    }

    private void OnDestroy()
    {                                             
        GetComponent<DeathManager>().Death -= Die;
        explosionParticle = Instantiate(explosionParticle, transform.position, transform.rotation) as GameObject;
        Destroy(explosionParticle, 1f);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < 0.1f)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            shootCoroutine = StartCoroutine(Shoot());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(shootCoroutine);
        }

    }

    private void Move()
    {
        var moveX = 0f;
        var moveY = 0f;

        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        var xPos = Mathf.Clamp(transform.position.x + moveX, xMin + 0.2f, xMax - 0.2f);
        var yPos = Mathf.Clamp(transform.position.y + moveY, yMin + 0.25f, yMax - 0.25f);

        transform.position = new Vector2(xPos, yPos);
    }

    private IEnumerator Shoot()
    {
        while (true) { 
            GameObject _laser = Instantiate(laser, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            _laser.GetComponent<LaserBehaviour>().SetDirection(Vector2.up);
            yield return new WaitForSeconds(timeBetweenShots);
        }      
    }

    private void MoveBoundaries()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y; 
    }

    public float GetHealth()
    {
        return GetComponent<DeathManager>().life;
    }

    public void Die()
    {
        Debug.Log("KILL PLAYER");
        GetComponent<DeathManager>().life = 0;
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
        FindObjectOfType<Level>().LoadGameOver();
    }

}
