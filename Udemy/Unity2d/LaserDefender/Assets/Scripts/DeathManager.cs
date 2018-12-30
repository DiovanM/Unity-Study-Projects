using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour {

    public delegate void KillOwner();
    public event KillOwner Death;
                                           
    [HideInInspector] public float life;  

    public void Damage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Death();
        }
    }  

}
