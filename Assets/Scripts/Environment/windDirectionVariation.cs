using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windDirectionVariation : MonoBehaviour
{
    // variables
    private WindZone wind;
    private Quaternion windRotation;
    [SerializeField] private float maxAngle, minAngle;
    [SerializeField] private float randomizationTimeInterval;
    private float timeElapsed, defaultTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
       wind = gameObject.AddComponent<WindZone>(); 
    }

    // Update is called once per frame
    void Update()
    {
        RandomizeWindRotation();
    }

    private void RandomizeWindRotation() 
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= randomizationTimeInterval) 
        {
            float rotationY = Random.Range(minAngle, maxAngle);
            windRotation = new Quaternion(this.transform.rotation.x, rotationY, this.transform.rotation.z, 0f);
            wind.gameObject.transform.rotation = windRotation;

            timeElapsed = defaultTime;
        }
    }
}
