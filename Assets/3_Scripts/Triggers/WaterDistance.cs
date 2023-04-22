using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDistance : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float innerRadius;
    [SerializeField] private AK.Wwise.RTPC waterDistanceRTPC;
    private SphereCollider coll;

    private static float globalWaterDistance;
    private float distanceToWater;

    private void Awake()
    {
        coll = GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        float distanceToPlayer = (transform.position - other.transform.position).magnitude;
        float lerpFactor = (coll.radius - distanceToPlayer) / (coll.radius - innerRadius);
        distanceToWater = Mathf.Lerp(0, maxDistance, lerpFactor);
    }

    private void Update()
    {
        globalWaterDistance = Mathf.Max(globalWaterDistance, distanceToWater);
    }

    private void LateUpdate()
    {
        if (globalWaterDistance <= 0) return;

        //Debug.Log(globalWaterDistance);
        waterDistanceRTPC.SetGlobalValue(globalWaterDistance);
        globalWaterDistance = 0;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
    }
}
