using System.Collections;
using UnityEngine;

public class Soldier : Enemy
{
    public override int health { get; set; }
    public override float attackRange { get; set; }
    public override float attackCooldown { get; set; }
    
    public GameObject projectile;
    public Transform muzzle;
    protected override void Update()
    {
        HandleMovementAndAttack();
    }

    protected override IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
    }
    
    
    
    void HandleMovementAndAttack()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        float stopDistance = attackRange - 1f;
        
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z); // Keep the same Y-axis
        

        if (distance <= attackRange) // Ranged attack (stand still)
        {
            transform.LookAt(targetPosition);
            agent.SetDestination(transform.position);
            
            StartCoroutine(Attack());
        }
        else if (distance <= attackRange + stopDistance) // Move closer but stop at attack range with offset
        {
            transform.LookAt(targetPosition);
            // Move towards the player, but stop slightly before the target
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 offsetPosition = player.position - direction * stopDistance;
            agent.SetDestination(offsetPosition);
        }
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(projectile, muzzle.position + transform.forward, muzzle.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(bulletInstance, 5);
    }
}
