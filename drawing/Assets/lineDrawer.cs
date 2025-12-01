using System.Collections.Generic;
using UnityEngine;

public class lineDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    private bool drawing = false;
    private GameObject line;
    private List<Vector3> points = new List<Vector3>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !drawing)
        {
            drawing = true;
            MakeLine();
        }
        if (drawing)
        {
            UpdateLine();
            if (points.Count >= 2)
            {
                line.GetComponent<LineRenderer>().positionCount = points.Count;
                line.GetComponent<LineRenderer>().SetPositions(points.ToArray());
            }
        }
    }
    private void MakeLine()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        line = Instantiate(linePrefab);
        points.Add(mousePos);
    }
    private void UpdateLine()
    {
        Vector2 newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        points.Add(newMousePos);
    }
}
