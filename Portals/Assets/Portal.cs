using UnityEngine;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{

    [Tooltip("Tag mask to test for, leave blank if dont' want to filter.")]
    public string TagMask;
    public Transform TargetLocation;
    public Camera PortalCamera;
    public Renderer PortalVisualSurface;
    
    #region Messages

    private void Start()
    {
        if(this.PortalCamera.targetTexture != null)
        {
            this.PortalCamera.targetTexture.Release();
        }
        
        this.PortalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        this.PortalVisualSurface.material.mainTexture = this.PortalCamera.targetTexture;
    }

    private void LateUpdate()
    {
        var cam = Camera.main; //get the main rendering camera... we're going to be positioning based off this
        
        var q = Quaternion.LookRotation(-this.TargetLocation.forward, this.TargetLocation.up);
        var dq = Quaternion.Inverse(this.transform.rotation) * q;

        //update pos
        var dv = cam.transform.position - this.transform.position;
        dv = dq * dv;
        this.PortalCamera.transform.position = this.TargetLocation.position + dv;
        
        //update rot
        this.PortalCamera.transform.rotation = dq * cam.transform.rotation;

        //make sure camera projection matches
        this.PortalCamera.projectionMatrix = cam.projectionMatrix;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrEmpty(this.TagMask) || other.CompareTag(this.TagMask))
        {
            var targ = other.transform;
            float dotProduct = Vector3.Dot(transform.forward, targ.position - transform.position);
            if (dotProduct > 0f)
            {
                var q = Quaternion.LookRotation(-this.transform.forward, this.transform.up);
                var dq = Quaternion.Inverse(q) * this.TargetLocation.rotation;

                //update position
                var dv = targ.position - this.transform.position;
                dv = dq * dv;
                targ.position = this.TargetLocation.position + dv;

                //update rot
                targ.rotation = dq * targ.rotation;

                UnityEngine.EventSystems.ExecuteEvents.Execute<ITeleportMessageReceiver>(targ.gameObject, null, (x, y) => x.Teleported(this));
            }
            
        }
    }

    #endregion


    public interface ITeleportMessageReceiver : UnityEngine.EventSystems.IEventSystemHandler
    {

        void Teleported(Portal portal);

    }


}
