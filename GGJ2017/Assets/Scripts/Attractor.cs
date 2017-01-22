using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    // Public vars.
    public float Power = 1000f;

    public float Range = 7f;

    public AnimationCurve Curve;

    // Unity callbacks.
    void Start () {
		
	}
	
	void Update ()
    {
		foreach (Attractable a in Attractable.__global_Attractables)
        {
            var direction = transform.position - a.transform.position;

            var distance = direction.magnitude;

            if (distance < Range)
            {
                var distancePercentage = distance / Range;

                a.GetComponent<Rigidbody>().AddForce(
                    direction.normalized 
                    * Power 
                    * Curve.Evaluate(1f - distancePercentage));
            }
        }
	}
}
