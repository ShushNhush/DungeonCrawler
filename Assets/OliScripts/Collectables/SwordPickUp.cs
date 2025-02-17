using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SwordPickUp : MonoBehaviour

{

    public Sword sword;

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();
            
            // Ensure there's a weapon before trying to remove it
            if (player.equippedWeapon != null)
            {
                player.equippedWeapon.Remove();
            }
            
            player.equippedWeapon = Instantiate(sword, player.transform);
            Destroy(gameObject);
            
        }
        
    }
    
}
