#if (UNITY_EDITOR)
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class AutoPlankBridge : MonoBehaviour
{
    [SerializeField] private float spacing, spacingOffset, lateralOffset;
    [SerializeField] private Transform target;
    [SerializeField] private List<GameObject> plankPrefabs;

    void Start()
    {
        
    }

    [ContextMenu("Build")]
    public void Build()
    {
        if (target.localPosition.z < 0)
        {
            Debug.LogError("AutoPlankBridge going in the wrong direction.", gameObject);
            return;
        }

        Vector3 pos = Vector3.zero;
        int random;
        int lastRandom = -1;

        var root = new GameObject("PlankBridge").transform;
        root.position = transform.position;
        root.localScale = transform.localScale;
        root.rotation = transform.rotation;
        root.parent = transform.parent;

        while (pos.z < target.localPosition.z)
        {
            do random = Random.Range((int)0, plankPrefabs.Count);
            while(random == lastRandom);
            lastRandom = random;

            var plank = Instantiate(plankPrefabs[random], root);
            pos += Vector3.forward * Random.Range(-spacingOffset, spacingOffset);
            plank.transform.localPosition = pos;
            plank.transform.localPosition += Vector3.right * Random.Range(-lateralOffset, lateralOffset);
            pos += Vector3.forward * spacing;
        }
    }
}

[CustomEditor(typeof(AutoPlankBridge))]
public class AutoPlankBridgeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 30;
        buttonStyle.margin.top = 20;
        if (GUILayout.Button("Build!", buttonStyle))
        {
            AutoPlankBridge placer = (AutoPlankBridge)target;
            placer.Build();
        }
    }
}
#endif