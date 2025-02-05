using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public GameObject cube;
    Transform t; 
    float speed = 0.01f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = cube.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotation = rotation + 0.001f;
        //t.Rotate(rotation,0,0);

        if (t.position.z > 2) 
        {
            speed = speed * -1;
        } else if (t.position.z < -8)
        {
            speed = speed * -1;
        }
        
        t.Translate(0,speed,0);

    }
}
