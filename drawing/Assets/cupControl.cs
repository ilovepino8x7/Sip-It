using UnityEngine;

public class cupControl : MonoBehaviour
{
    public int inCup;
    public GameObject pipe;
    private float percentFilled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        percentFilled = inCup / (float)pipe.GetComponent<spawnWater>().amount * 100;
        Debug.LogWarning(inCup);
        Debug.LogWarning(percentFilled.ToString() + "%");
    } 
}
