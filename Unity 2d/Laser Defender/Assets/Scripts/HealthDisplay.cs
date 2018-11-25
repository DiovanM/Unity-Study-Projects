using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {  

    Text healthText;
    Player player;
                                    
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }
                                              
    void Update()
    {
        if (player)
        {
            healthText.text = player.GetHealth().ToString();
        } else
        {
            healthText.text = "0";
        }
    }
}
