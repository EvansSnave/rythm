using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
