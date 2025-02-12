using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Player.transform.position, speed);
    }
}
