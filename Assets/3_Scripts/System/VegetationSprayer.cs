using UnityEngine;
using UnityEditor;

public class VegetationSprayer : MonoBehaviour
{
    public float density;
    public bool isCircle;
    public float circleRadius;
    public Vector2 rectangleBounds;
    public string customParentName;
    public GameObject[] plantPrefabs;

    [ContextMenu("Spray")]

    public void Spray()
    {
        if (isCircle) Circle();
        else Rectangle();
    }

    private void Circle()
    {
        var root = new GameObject(customParentName);
        root.transform.parent = transform.parent;
        int numPlants = Mathf.RoundToInt(Mathf.PI * circleRadius * circleRadius * density);

        for (int i = 0; i < numPlants; i++)
        {
            float angle = (float)i / numPlants * Mathf.PI * 2f;
            Vector2 randomPoint = Random.insideUnitCircle * circleRadius;
            Vector3 point = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
            RaycastHit hit;
            if (Physics.Raycast(point + Vector3.up * 50f, Vector3.down, out hit, Mathf.Infinity, 1 << Layer.ground))
            {
                GameObject prefab = plantPrefabs[Random.Range(0, plantPrefabs.Length)];
                Instantiate(prefab, hit.point, Quaternion.identity, root.transform);
            }
        }
    }

    private void Rectangle()
    {
        var root = new GameObject(customParentName);
        root.transform.parent = transform.parent;
        float area = rectangleBounds.x * rectangleBounds.y;
        int numPlants = Mathf.RoundToInt(area * density);

        for (int i = 0; i < numPlants; i++)
        {
            GameObject randomPlant = plantPrefabs[Random.Range(0, plantPrefabs.Length)];
            Vector3 position = new Vector3(Random.Range(-rectangleBounds.x / 2f, rectangleBounds.x / 2f), 0f, Random.Range(-rectangleBounds.y / 2f, rectangleBounds.y / 2f)) + transform.position;
            RaycastHit hit;
            if (!Physics.Raycast(position + Vector3.up * 50f, Vector3.down, out hit, Mathf.Infinity, 1 << Layer.ground)) continue;
            position.y = hit.point.y;
            Instantiate(randomPlant, position, Quaternion.identity, root.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (isCircle) CircleGizmo();
        else RectangleGizmo();
    }

    private void CircleGizmo()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, circleRadius);

        int numPlants = Mathf.RoundToInt(Mathf.PI * circleRadius * circleRadius * density);

        for (int i = 0; i < numPlants; i++)
        {
            float angle = (float)i / numPlants * Mathf.PI * 2f;
            Vector2 randomPoint = Random.insideUnitCircle * circleRadius;
            Vector3 point = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
            RaycastHit hit;
            if (!Physics.Raycast(point + Vector3.up * 50f, Vector3.down, out hit, Mathf.Infinity, 1 << Layer.ground)) continue;
            point.y = hit.point.y;
            Gizmos.DrawSphere(point, 0.1f);
        }
    }

    private void RectangleGizmo()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(rectangleBounds.x, 0f, rectangleBounds.y));

        float area = rectangleBounds.x * rectangleBounds.y;
        int numPlants = Mathf.RoundToInt(area * density);
        for (int i = 0; i < numPlants; i++)
        {
            Vector3 position = new Vector3(Random.Range(-rectangleBounds.x / 2f, rectangleBounds.x / 2f), 0f, Random.Range(-rectangleBounds.y / 2f, rectangleBounds.y / 2f)) + transform.position;
            RaycastHit hit;
            if (!Physics.Raycast(position + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, 1 << Layer.ground)) continue;
            position.y = hit.point.y;
            Gizmos.DrawSphere(position, 0.1f);
        }
    }
}

[CustomEditor(typeof(VegetationSprayer))]
public class RandomPlantPlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 30;
        buttonStyle.margin.top = 20;
        if (GUILayout.Button("Spray!", buttonStyle))
        {
            VegetationSprayer placer = (VegetationSprayer)target;
            placer.Spray();
        }
    }
}