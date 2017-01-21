using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizAnimPreview : MonoBehaviour
{
    public enum Anims
    {
        idle,
        walk,
        magic
    }

    public Anims setAnim = Anims.idle;
    Animator myAnim = null;

	void Start ()
    {
        myAnim = GetComponent<Animator>();
	}
	
	void LateUpdate ()
    {
		switch(setAnim)
        {
            case Anims.idle:
                myAnim.SetTrigger("idle");
                break;
            case Anims.magic:
                myAnim.SetTrigger("magic");
                break;
            case Anims.walk:
                myAnim.SetTrigger("walk");
                break;
        }
	}
}
