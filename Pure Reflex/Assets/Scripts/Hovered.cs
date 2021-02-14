using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovered : MonoBehaviour
{
    private Outline outlineScript;

    // Start is called before the first frame update
    void Start()
    {
        outlineScript = GetComponent<Outline>();
    }

    void OnHitEnter(GameObject Player)
    {   
        if (gameObject.Equals(Player))
            outlineScript.OutlineColor = Color.blue;
        else
            outlineScript.OutlineColor = Color.red;
        outlineScript.OutlineWidth = 3;
    }

    void OnHitExit()
    {   
        outlineScript.OutlineColor = Color.white;
        outlineScript.OutlineWidth = 0;
    }
}
