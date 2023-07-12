using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentScaner : MonoBehaviour
{
    [SerializeField] Vector3 forwardRayOffset = new Vector3(0, .25f, 0);
    [SerializeField] float forwardRayLength = 0.2f;
    [SerializeField] float heightRayLength = 5f;
    [SerializeField] LayerMask obstacleLayer;
    public ObstacleHitData ObstacleCheck()
    {
        var hitdata = new ObstacleHitData();
        var forwardOrigin = transform.position + forwardRayOffset;
        hitdata.forwardHitFound = Physics.Raycast(forwardOrigin, transform.forward, out hitdata.forwardHit,
             forwardRayLength, obstacleLayer);

        Debug.DrawRay(forwardOrigin, transform.forward * forwardRayLength, (hitdata.forwardHitFound) ? Color.red : Color.white);

        if (hitdata.forwardHitFound)
        {
            var heightOrigin = hitdata.forwardHit.point + Vector3.up * heightRayLength;
            hitdata.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitdata.heightHit, heightRayLength, obstacleLayer);
            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitdata.heightHitFound) ? Color.red : Color.white);

        }

        return hitdata;
    }
}

public struct ObstacleHitData
{
    public bool forwardHitFound;
    public bool heightHitFound;
    public RaycastHit forwardHit;
    public RaycastHit heightHit;

}
