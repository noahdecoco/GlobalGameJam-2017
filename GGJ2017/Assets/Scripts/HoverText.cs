using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour
{
    public GameObject HoverGUITextPrefab;

    public float HoverTextDuration = 1f;

    private HoverGUIText hoverGUIText;

	void Awake ()
    {
        GameObject hoverGUITextObject = GameObject.Instantiate(HoverGUITextPrefab);

        hoverGUIText = hoverGUITextObject.GetComponent<HoverGUIText>();

        hoverGUIText.target = transform;

        //iTween.MoveFrom(gameObject, iTween.Hash("y", -10, "time", HoverTextDuration));

        Invoke("Suicide", HoverTextDuration);
    }

    public void SetText(string text)
    {
        hoverGUIText.GetComponent<GUIText>().text = text;
    }
	
	private void Suicide()
    {
        GameObject.Destroy(hoverGUIText.gameObject);
        GameObject.Destroy(gameObject);
    }
}
