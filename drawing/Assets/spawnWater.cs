using UnityEngine;
using System.Collections;
public class spawnWater : MonoBehaviour
{
    public GameObject water;
    [HideInInspector]
    public int amount = 50;
    public GameObject[] spawnPoints;
    private int counter;
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
            Vector2 pos = new Vector2(spawnPoints[Random.Range(0,8)].transform.position.x, spawnPoints[Random.Range(0,8)].transform.position.y + (float)counter / (float)2);
            GameObject wat = Instantiate(water, pos, Quaternion.identity);
            wat.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Random.Range(-15, 15), - 6);
        }
    }
    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
