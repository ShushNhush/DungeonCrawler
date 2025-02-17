using UnityEngine;

public class ExitCollider : MonoBehaviour
{
    public RoomGenerator roomGenerator;
    
    private void Awake()
    {
        roomGenerator = FindFirstObjectByType<RoomGenerator>();
        if (roomGenerator == null)
        {
            Debug.LogError("ExitCollider: No RoomGenerator found in the scene!");
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.tag == "Player")
        {
            var frontRoom = roomGenerator.GetFrontRoom();
            var middleRoom = roomGenerator.GetMiddleRoom();
            
            
            if (frontRoom == null)
            {
                Vector3 spawnPosition = middleRoom.transform.position + middleRoom.transform.forward * roomGenerator.GetSegmentLength(middleRoom);
                frontRoom = roomGenerator.GetRandomRoom();
                frontRoom = Instantiate(frontRoom, spawnPosition, Quaternion.identity);
            }
            
            else
            {
                Destroy(roomGenerator.GetBackRoom());
                roomGenerator.SetBackRoom(middleRoom);
                roomGenerator.SetMiddleRoom(frontRoom);

                middleRoom = roomGenerator.GetMiddleRoom();
                Vector3 spawnPosition = middleRoom.transform.position + middleRoom.transform.forward * roomGenerator.GetSegmentLength(middleRoom);
                
                frontRoom = roomGenerator.GetRandomRoom();
                frontRoom = Instantiate(frontRoom, spawnPosition, Quaternion.identity);
            
            }
            roomGenerator.SetFrontRoom(frontRoom);
            Destroy(gameObject);
            
        }
    }
}
