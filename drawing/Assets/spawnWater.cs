using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
public class spawnWater : MonoBehaviour
{
    public GameObject water;
    [HideInInspector]
    public int amount = 50;
    public GameObject[] spawnPoints;
    private int counter;
    public logicManager ls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (ls.tutNum == 0)
        {
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }
  void OnMouseDown()
  {
    if (ls.tutNum == 1)
        {
            ls.tutNum++;
        }
    SpawnWater();
  }


  public void SpawnWater()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 pos = new Vector2(spawnPoints[Random.Range(0,6)].transform.position.x, spawnPoints[Random.Range(0,6)].transform.position.y + (float)counter / (float)2);
            GameObject wat = Instantiate(water, pos, Quaternion.identity);
            wat.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, - 10);
        }
    }
    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
