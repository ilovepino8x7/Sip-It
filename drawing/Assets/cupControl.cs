using UnityEngine;
using UnityEngine.UI;

public class cupControl : MonoBehaviour
{
    public int inCup;
    public GameObject pipe;
    private float percentFilled;
    public Slider sly;
    private logicManager ls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ls = GameObject.FindWithTag("Brian").GetComponent<logicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] waters = GameObject.FindGameObjectsWithTag("what");
        if (waters.Length < 10)
        {
            EndLevel();
        }
        percentFilled = inCup / (float)pipe.GetComponent<spawnWater>().amount * 100;
        sly.value = percentFilled / 100;
    } 

    public void EndLevel()
    {
        if (percentFilled >= 50)
        {
            ls.Success();
        }
        else
        {
            ls.Fail();
        }
    }
}
