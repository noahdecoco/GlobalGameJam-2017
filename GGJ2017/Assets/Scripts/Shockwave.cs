using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
	private GameObject _caster;

	private SphereCollider _sc;

	public void SetCaster(GameObject caster)
    {
		_caster = caster;
	}

	public void Blast()
    {
		_sc = gameObject.GetComponent<SphereCollider>();
		Invoke("destroy", 1f);
		//iTween.ScaleTo(sc,iTween.Hash("radius",2,"time",1.5f,"oncomplete","destroy"));
	}

	void Update()
    {
		//_sc.radius += 0.1f * Time.deltaTime;
	}

	void destroy ()
    {
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
    {
		if (other.gameObject != _caster)
        {
			Vector3 dir = other.transform.position - _caster.transform.position;

			other.GetComponent<Rigidbody>().AddForce(dir.normalized * 50000);

            other.GetComponent<Rumbler>().SetFadingRumbleOverTime(0.25f, 1f, 0.25f);
        }
	}
}
