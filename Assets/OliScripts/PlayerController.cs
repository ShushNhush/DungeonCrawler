using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Rigidbody rb;
    public int currentHealth;
    public int maxHealth = 100;
    
    public HealthBar healthBar;
    
    public float speed = 10f;
    public float rotationSpeed = 100f;
    public float jumpHeight = 5f;
    private float movementX;
    private float movementY;
    private bool isGrounded;
    
    private float m_RayDistance;
    private RaycastHit m_Hit;

    public Camera playerCamera;
    
    public IWeapon equippedWeapon;
    private PowerUpManager powerUpManager;
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        
        rb.freezeRotation = true; // Prevent physics-based rotation
        
        powerUpManager = GetComponent<PowerUpManager>();
        
    }
    
    

    void OnMove(InputValue value)
    {
        Vector2 movementVector = value.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void OnAttack()
    {

        if (equippedWeapon != null)
        {
            equippedWeapon.Attack();
        }
        
    }

    void OnJump()
    {
        if (isGrounded)
        {
            Vector3 jump = new Vector3(0.0f, jumpHeight, 0.0f);
            rb.AddForce(jump, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        RotateCharacterToMouse();
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            powerUpManager.UseNextItem();
        }
        
    }

    void FixedUpdate()
    {
       
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        rb.linearVelocity = new Vector3(movementX * speed, rb.linearVelocity.y, movementY * speed);

        
    }
    
    private void RotateCharacterToMouse()
    {
        if (Mouse.current == null || playerCamera == null) return;

        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        if (mouseScreenPosition.x < 0 || mouseScreenPosition.y < 0 || 
            mouseScreenPosition.x > Screen.width || mouseScreenPosition.y > Screen.height)
        {
            return; // Skip rotation if mouse is out of bounds
        }
        
        Ray ray = playerCamera.ScreenPointToRay(mouseScreenPosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = (targetPoint - transform.position).normalized;
            direction.y = 0; // Keep rotation flat

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}