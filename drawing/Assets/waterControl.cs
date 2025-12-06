using UnityEngine;

public class waterControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }
}
