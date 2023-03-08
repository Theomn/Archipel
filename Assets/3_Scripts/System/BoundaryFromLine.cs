using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryFromLine : MonoBehaviour
{
    [SerializeField] private bool invertDirection;
    [SerializeField] private bool showCollider;
    [SerializeField] private GameObject wallMesh;
    private LineRenderer line;

    private static float totalElapsedTimeMs;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        if(line.positionCount == 0)
        {
            return;
        }
        
        var watch = System.Diagnostics.Stopwatch.StartNew();
        Vector3[] pos = new Vector3[line.positionCount];
        line.GetPositions(pos);
        for(int i = 0; i < line.positionCount-1; i++)
        {
            PlaceBoundaryBetween(pos[i], pos[i+1]);
        }
        if (line.loop)
        {
            PlaceBoundaryBetween(pos[pos.Length -1], pos[0]);
        }
        line.enabled = false;
        watch.Stop();
        totalElapsedTimeMs += watch.ElapsedMilliseconds;
    }

    private void PlaceBoundaryBetween(Vector3 a, Vector3 b)
    {
        var wall = Instantiate(wallMesh, transform);
        var half = (b - a) / 2;
        wall.transform.localPosition = a + half;
        var a2 = new Vector2(a.x, a.z);
        var b2 = new Vector2(b.x, b.z);
        wall.transform.rotation = Quaternion.LookRotation(half);
        wall.transform.eulerAngles = new Vector3(0, wall.transform.eulerAngles.y + (invertDirection? 90 : -90), 0);
        wall.transform.localScale = new Vector3(Vector3.Distance(a2, b2), 1, 1);
        if (showCollider)
        {
            wall.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }
}
