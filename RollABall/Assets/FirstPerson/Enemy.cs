using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            health -= 1;
            Destroy(other.gameObject);
        }
        print("hit");
    }
}
