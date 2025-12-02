using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (Input.GetMouseButtonUp(0) && drawing)
        {
            EndLine();
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
        line.GetComponent<Rigidbody2D>().simulated = false;
        points.Add(line.transform.InverseTransformPoint(mousePos));
    }
    private void UpdateLine()
    {
        Vector2 newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lastPos = line.transform.TransformPoint(points[points.Count - 1]);
        if (Vector2.Distance(lastPos ,newMousePos) < 0.1f)
        {
            return;
        }
        Collider2D hit = Physics2D.OverlapCircle(newMousePos, 0.2f);
        if (hit != null && hit.gameObject != line)
        {
            EndLine();
            return;
        }

        points.Add(line.transform.InverseTransformPoint(newMousePos));
    }
    private void EndLine()
    {
        drawing = false;
        points.Add(line.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        if (points.Count < 2)
        {
            Destroy(line);
            line = null;
            points.Clear();
            return;
        }
        line.GetComponent<LineRenderer>().positionCount = points.Count;
        line.GetComponent<LineRenderer>().SetPositions(points.ToArray());
        MakePhysics();
        line.GetComponent<Rigidbody2D>().simulated = true;
        line = null;
        points.Clear();
    }
    private void MakePhysics()
    {
        PolygonCollider2D polyphemus = line.AddComponent<PolygonCollider2D>();
        List<Vector2> top = new List<Vector2>();
        List<Vector2> bottom = new List<Vector2>();
        for (int i = 0; i < points.Count; i++)
        {
            Vector2 current = line.transform.TransformPoint(points[i]);
            Vector2 direction = (i == points.Count - 1) ? (line.transform.TransformPoint(points[i]) - line.transform.TransformPoint(points[i - 1])).normalized : (line.transform.TransformPoint(points[i + 1]) - line.transform.TransformPoint(points[i])).normalized;
            Vector2 normal = new Vector2(-direction.y, direction.x);
            top.Add(current + normal * 0.1f);
            bottom.Add(current - normal * 0.1f);
        }
        bottom.Reverse();
        List<Vector2> final = new List<Vector2>();
        final.AddRange(top);
        final.AddRange(bottom);
        final.Add(final[0]);
        for (int i = 0; i < final.Count; i++)
        {
            final[i] = line.transform.InverseTransformPoint(final[i]);
        }
        polyphemus.points = final.ToArray();
    }
}
