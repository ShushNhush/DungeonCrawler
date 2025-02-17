using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUpManager : MonoBehaviour
{ 

    private static Queue<ItemType> powerUpQueue = new();
    public TextMeshProUGUI powerUpOverlayText;
    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        if (player == null) Debug.LogError("PlayerController reference missing in PowerUpManager!");
        powerUpOverlayText.SetText("Collected: ");
    }

    public void CollectItem(ItemType itemType)
    {
        powerUpQueue.Enqueue(itemType);
        Debug.Log($"Player collected {itemType}! Total in queue: {powerUpQueue.Count}");;
        UpdateOverlayText();
    }

    public void UseNextItem()
    {
        if (powerUpQueue.Count > 0)
        {
            ItemType itemType = powerUpQueue.Dequeue();
            Debug.Log($"Player used {itemType}! Remaning: {powerUpQueue.Count}");

            ApplyPowerUp(itemType);
            UpdateOverlayText();
        }
        else {
            Debug.Log("No powerups left!");
        }
    }

    private void ApplyPowerUp(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.SpeedBoost:
                ApplyPowerUp<SpeedBoostPowerUp>();
                break;
            case ItemType.JumpBoost:
                ApplyPowerUp<JumpBoostPowerUp>();
                break;
            case ItemType.Healing:
                ApplyPowerUp<HealingPowerUp>();
                break;
        }
    }

    private void ApplyPowerUp<T>() where T : PowerUp
    {
        GameObject powerUpObject = new GameObject(typeof(T).Name);
        T powerUp = powerUpObject.AddComponent<T>();
        powerUp.ApplyPowerUp(player);
        
        float powerUpDuration = powerUp.duration > 0 ? powerUp.duration : 5f;
        Destroy(powerUpObject, powerUpDuration + 0.5f);
    }
    
    private void UpdateOverlayText()
    {
        if (powerUpQueue.Count > 0)
        {
            powerUpOverlayText.SetText($"Next Power-Up: {powerUpQueue.Peek()} (Total: {powerUpQueue.Count})");
        }
        else
        {
            powerUpOverlayText.SetText("Collected: None");
        }
    }
}