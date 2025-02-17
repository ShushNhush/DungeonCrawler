using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public float duration = 5f;
    public abstract void ApplyPowerUp(PlayerController player);
}