using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

	private GameObject _caster;

	public void SetCaster(GameObject caster){
		_caster = caster;
	}

	public void Blast(){
		iTween.ScaleTo(gameObject,iTween.Hash("scale",new Vector3(10,10,10),"time",1.5f,"oncomplete","destroy"));
		iTween.FadeTo(gameObject, 0, 1.5f);
	}

	void destroy () {
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other){
		
		if(other.gameObject != _caster){

			Vector3 dir = other.transform.position - _caster.transform.position;
			other.GetComponent<Rigidbody>().AddForce(dir * 50000);

		}


	}
}
