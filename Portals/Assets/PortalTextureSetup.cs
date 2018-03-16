using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour {

    public Camera cameraA;
    public Material cameraMata;
    public Camera cameraB;
    public Material cameraMatb;

	// Use this for initialization
	void Start () {

        if (cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }

        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMata.mainTexture = cameraA.targetTexture;

        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatb.mainTexture = cameraB.targetTexture;
	}
	
}
