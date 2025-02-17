using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour, IWeapon
{
    public GameObject projectile;

    public Transform muzzle;

    public float speed = 20f;

    public bool multiShotEnable = false;

    public void Attack()
    {
        if (multiShotEnable)
        {
            multiShotFire();
        }
        else
        {
            singleShotFire();
        }
    }

    public void Remove()
    {
        throw new System.NotImplementedException();
    }

    public void singleShotFire()
    {
        GameObject bulletInstance = Instantiate(projectile, muzzle.position + transform.forward, muzzle.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(bulletInstance, 5);
    }

    public void multiShotFire()
    {
        Vector3[] bulletOffsets = new Vector3[]
        {
            muzzle.position + transform.forward + transform.right * 1.0f, // Right
            muzzle.position + transform.forward - transform.right * 1.0f, // Left
            muzzle.position + transform.forward + transform.up * 0.5f,    // Up
            muzzle.position + transform.forward - transform.up * 0.5f,    // Down
            muzzle.position + transform.forward * 2.0f                   // Center
        };
        foreach (Vector3 offset in bulletOffsets)
        {
            GameObject bulletInstance = Instantiate(projectile, offset, muzzle.rotation);
            bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
            Destroy(bulletInstance, 5);
        }
        
    }


    public void SetProjectile(GameObject projectile)
    {
        this.projectile = projectile;
    }

    public GameObject GetProjectile()
    {
        return this.projectile;
    }

    public float SetSpeed(GameObject speed)
    {
        return this.speed;
    }

    public void EnableMultiShot(bool enable)
    {
        multiShotEnable = enable;
    }
}