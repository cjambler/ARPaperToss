using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class playerInputActions : MonoBehaviour
{
    //mobile input variables
    private Vector2 fingerDownPos, fingerUpPos, tossDirection;

    //time variables
    private float touchTimeStart, touchTimeEnd, touchTimeInterval;

    //access throwable object component variables
    [SerializeField] private Rigidbody tossMeRB;
    [SerializeField] private Transform tossMeTransform;

    //Object Force variables and others
    [SerializeField]private float throwForceMultiplierXY = 1f;
    [SerializeField]private float throwForceMultiplierZ = 20f;
    private Vector3 defaultObjectPosition;
    private Quaternion defaultObjectRotation;

    //computer control test variables
     
    void Start()
    {
        if (tossMeTransform != null) 
            {
                defaultObjectPosition = tossMeTransform.position;
                defaultObjectRotation = tossMeTransform.rotation;
            }
    }
    
    void Update()
    {
        PlayerActions();
    }

    private void PlayerActions() 
    {
        //detects if the player has touched the screen and records the position of the player's finger on the screen
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) 
        {
            touchTimeStart = Time.time;
            if (Input.GetMouseButtonDown(0)) fingerDownPos = Input.mousePosition;
            else fingerDownPos = Input.GetTouch(0).position;
        }

        //detects when the player releases their touch on the screen
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0)) 
        {
            touchTimeEnd = Time.time;
           
            if (Input.GetMouseButtonUp(0)) fingerUpPos = Input.mousePosition; 
            else fingerUpPos = Input.GetTouch(0).position;
            
            touchTimeInterval = touchTimeEnd - touchTimeStart;
            tossDirection = fingerUpPos - fingerDownPos;
            
            //detects that the rigidbody accessed is not null and runs the code 
            if (tossMeRB != null) 
                {
                    tossMeRB.useGravity = true;
                    tossMeRB.isKinematic = false;
                    Vector3 tossForce = new Vector3(tossDirection.x * throwForceMultiplierXY, tossDirection.y * throwForceMultiplierXY, throwForceMultiplierZ/touchTimeInterval); 

                    //uses the Vector3 calculated from the input variables to add force to the rigid body
                    tossMeRB.AddForce(tossForce);
                }
        }

        if (detectCollisions.ObjectIsGrounded()) Invoke("ResetObjectPosition", 2f);
    }

    private void ResetObjectPosition() 
    {
        detectCollisions.SetGroundedFalse();
        tossMeRB.useGravity = false;
        tossMeRB.isKinematic = true;
        tossMeRB.velocity = Vector3.zero;
        tossMeTransform.position = defaultObjectPosition;
        tossMeTransform.rotation = defaultObjectRotation;
    } 
}
