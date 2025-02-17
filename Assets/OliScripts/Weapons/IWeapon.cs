using UnityEngine;

public interface IWeapon
{
    void Attack();
    
    void SetProjectile(GameObject projectile);
    GameObject GetProjectile();
    
    float SetSpeed(GameObject speed);

    void EnableMultiShot(bool enable);

    void Remove();
}