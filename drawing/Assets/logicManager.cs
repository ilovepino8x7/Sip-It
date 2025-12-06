using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logicManager : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject Pipe;
    public GameObject Cup;
    public GameObject bolt1;
    public GameObject bolt2;
    public GameObject bolt3;
    public GameObject waterLevel;
    private bool drawing = false;
    private GameObject line;
    private List<Vector3> points = new List<Vector3>();
    public TMP_Text tut;
    [HideInInspector]
    public int tutNum = 0;
    public GameObject nextLevelScreen;
    private float timer;
    public TMP_Text time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time.gameObject.SetActive(false);
        Pipe.SetActive(false);
        nextLevelScreen.SetActive(false);
        Cup.SetActive(false);
        bolt1.SetActive(false);
        bolt2.SetActive(false);
        bolt3.SetActive(false);
        waterLevel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tutNum >= 2)
        {
            
            if (timer >= 15)
            {
                Cup.GetComponent<cupControl>().EndLevel();
            }
            else
            {
                timer += Time.deltaTime;
            }
            if (timer >= 14)
            {
                time.text = "0";
            }
            else if (timer >= 13)
            {
                time.text = "1";
            }
            else if (timer >= 12)
            {
                time.text = "2";
            }
            else if (timer >= 11)
            {
                time.text = "3";
            }
            else if (timer >= 10)
            {
                time.text = "4";
            }
            else if (timer >= 9)
            {
                time.gameObject.SetActive(true);
                time.text = "5";
            }
        }
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            UpdateTutorial();
        }
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
        if (Vector2.Distance(lastPos, newMousePos) < 0.1f)
        {
            return;
        }
        Collider2D hit = Physics2D.OverlapCircle(newMousePos, 0.2f);
        if (hit != null && hit.gameObject != line && hit.tag != "kill")
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
        if (tutNum == 0)
        {
            tutNum++;
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
    private void UpdateTutorial()
    {
        if (tutNum == 0)
        {
            tut.text = "Press & Drag to Draw Lines";
        }
        else if (tutNum == 1)
        {
            Pipe.SetActive(true);
            Cup.SetActive(true);
            bolt1.SetActive(true);
            bolt2.SetActive(true);
            bolt3.SetActive(true);
            waterLevel.SetActive(true);
            tut.text = "Press the pipe to spawn water";
        }
        else if (tutNum == 2)
        {
            tut.text = "Draw a line from under the pipe to the cup for the water to flow over";
        }
    }
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("One");
        }
    }
    public void Success()
    {
        // play music
        time.gameObject.SetActive(false);
        nextLevelScreen.SetActive(true);
    }
    public void Fail()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
