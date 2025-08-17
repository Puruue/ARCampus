using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInstantPlacementManager : MonoBehaviour
{
    [SerializeField] public GameObject placedPrefab;

    public ARRaycastManager raycastManager;
    public GameObject spawnedObject;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Start()
    {
        // Get ARRaycastManager from XR Origin
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPos = touch.position;

                // Use Instant Placement if available
                if (raycastManager.Raycast(
                    touchPos,
                    s_Hits,
                    TrackableType.InstantPlacement | TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    if (spawnedObject == null)
                        spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    else
                        spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
