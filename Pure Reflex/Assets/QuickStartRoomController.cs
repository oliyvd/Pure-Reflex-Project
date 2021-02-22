using UnityEngine;
using Photon.Pun;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    public int multiplayerSceneIndex; // Number for the build index to the multiplay scene    

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        StartGame();
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);        
        }
    }

}
