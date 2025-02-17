using UnityEngine;

public class HealingPowerUp : PowerUp
{
    public int healAmount = 30; // Adjust as needed

    public override void ApplyPowerUp(PlayerController player)
    {
        if (player != null)
        {
            player.currentHealth += healAmount;
            player.healthBar.SetHealth(player.currentHealth);
            if (player.currentHealth > player.maxHealth) player.currentHealth = player.maxHealth; // Prevent overhealing
            
            Debug.Log($"Player healed by {healAmount}. Current health: {player.currentHealth}");
            Destroy(gameObject); // Remove power-up after use
        }
        else
        {
            Debug.LogError("PlayerController is null!");
        }
    }
}