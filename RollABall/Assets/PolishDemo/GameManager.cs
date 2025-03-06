using UnityEngine;

using UnityEngine.SceneManagement;  //This makes swtiching scenes easier

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame() 
    {
        SceneManager.LoadScene("PolishEndGame");
    }
}
