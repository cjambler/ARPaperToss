using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectCollisions : MonoBehaviour
{
    private Collider objectCollider;
    [SerializeField] private string groundTag = "floor", goaltag = "goal";
    private static bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        objectCollider = this.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == groundTag) 
        {
            calculateScores.ResetStreakAndScore();
            isGrounded = true;
        }

        if (collision.gameObject.tag == goaltag) 
        {
            calculateScores.AddToStreakAndScore();
            isGrounded = true;
        }
    }

    public static bool ObjectIsGrounded() 
    {
        return isGrounded;
    }

    public static void SetGroundedFalse() 
    {
        isGrounded = false;
    }
}
