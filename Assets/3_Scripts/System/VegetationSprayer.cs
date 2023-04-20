using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class VegetationSprayer : MonoBehaviour
{
    public GameObject[] plantPrefabs;
    public float density = 10;
    public float xBounds = 10f;
    public float zBounds = 10f;
    public LayerMask groundLayer;

#if UNITY_EDITOR
    [ContextMenu("Spray")]

    public void Spray()
    {
        var root = new GameObject("PlantGroup");
        root.transform.parent = transform.parent;
        float area = xBounds * zBounds;
        int numPlants = Mathf.RoundToInt(area * density);
        for (int i = 0; i < numPlants; i++)
        {
            GameObject randomPlant = plantPrefabs[Random.Range(0, plantPrefabs.Length)];
            Vector3 position = new Vector3(Random.Range(-xBounds / 2f, xBounds / 2f), 0f, Random.Range(-zBounds / 2f, zBounds / 2f)) + transform.position;
            RaycastHit hit;
            if (!Physics.Raycast(position + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, groundLayer)) continue;
            position.y = hit.point.y;
            Instantiate(randomPlant, position, Quaternion.identity, root.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(xBounds, 0f, zBounds));

        float area = xBounds * zBounds;
        int numPlants = Mathf.RoundToInt(area * density);
        for (int i = 0; i < numPlants; i++)
        {
            Vector3 position = new Vector3(Random.Range(-xBounds / 2f, xBounds / 2f), 0f, Random.Range(-zBounds / 2f, zBounds / 2f)) + transform.position;
            RaycastHit hit;
            if (!Physics.Raycast(position + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, groundLayer)) continue;
            position.y = hit.point.y;
            Gizmos.DrawWireSphere(position, 0.1f);
        }
    }

#endif
}