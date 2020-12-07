using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARTaptoPlaceBin : MonoBehaviour
{
    //variables for placing bin
    [SerializeField] private GameObject objToInstantiate;

    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private Vector2 touchPosition;

    private bool binInScene = false;

    private static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();


    static bool condition;

    // Start is called before the first frame update
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlaceBin();
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition) 
    {
        if (Input.touchCount > 0) 
        {
            touchPosition = Input.GetTouch(index:0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void PlaceBin() 
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

        if (raycastManager.Raycast(touchPosition, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = raycastHits[0].pose;

            if (spawnedObject == null) spawnedObject = Instantiate(objToInstantiate, hitPose.position, hitPose.rotation);
            else spawnedObject.transform.position = hitPose.position;

            binInScene = true;
            condition = binInScene;
        }
    }

    public void PlaceOrMoveBin() 
    {
        binInScene = false;
        condition = binInScene;
    }

    public static bool GoalIsPlaced() 
    {
        return condition;
    }
}
