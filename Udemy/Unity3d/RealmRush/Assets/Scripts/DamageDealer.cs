using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    public float damage = 10;

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

}
