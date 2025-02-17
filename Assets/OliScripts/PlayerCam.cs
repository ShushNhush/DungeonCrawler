using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    
    public Transform player;
    public float rotationSpeed = 5f;
    private Vector3 offset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is missing in CapsuleCam!");
            return;
        }

        offset = transform.position - player.position;

        
        transform.position = player.position + offset;

    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        transform.position = player.position + offset;
        
    }
}