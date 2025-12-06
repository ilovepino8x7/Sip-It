using UnityEngine;

public class spawnWater : MonoBehaviour
{
    public GameObject water;
    [HideInInspector]
    public int amount = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  void OnMouseDown()
  {
    SpawnWater();
  }


  public void SpawnWater()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject wat = Instantiate(water, transform.position, Quaternion.identity);
            wat.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Random.Range(-5, 5), 0);
        }
    }

}
