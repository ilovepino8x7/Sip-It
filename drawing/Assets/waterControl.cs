using UnityEngine;
using UnityEngine.SceneManagement;

public class waterControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Two")
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-15, 15);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "cup")
        {
            GetComponent<Rigidbody2D>().simulated = false;
            other.GetComponentInParent<cupControl>().inCup += 1;
            Destroy(gameObject);
        }
        else if (other.tag == "kill")
        {
            Destroy(gameObject);
        }
    }
}
