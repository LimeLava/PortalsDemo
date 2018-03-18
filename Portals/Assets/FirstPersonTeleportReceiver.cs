using UnityEngine;
using System.Collections.Generic;

public class FirstPersonTeleportReceiver : MonoBehaviour, Portal.ITeleportMessageReceiver
{

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;

    void Portal.ITeleportMessageReceiver.Teleported(Portal portal)
    {
        this.Controller.MouseLook.Init(this.transform, Camera.main.transform);
    }
}
