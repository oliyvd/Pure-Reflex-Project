using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hovered : MonoBehaviourPunCallbacks
{
    private Outline outlineScript;
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        outlineScript = GetComponent<Outline>();
        PV = GetComponent<PhotonView>();
    }

    void OnHitEnter(GameObject Player)
    {   
        if (PV.IsMine)
            outlineScript.OutlineColor = Color.blue;
        else
            outlineScript.OutlineColor = Color.red;
        outlineScript.OutlineWidth = 3;
    }

    void OnHitExit()
    {   
        //outlineScript.OutlineColor = Color.white;
        outlineScript.OutlineWidth = 0;
    }
}
