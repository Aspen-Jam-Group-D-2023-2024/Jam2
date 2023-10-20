using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// THIS SCRIPT ASSUMES YOU HAVE A MOVEMENT SCRIPT ATTACHED TO THE PLAYER
[RequireComponent(typeof(LineRenderer))]
public class Grappler : MonoBehaviour
{
    [Header("Grappling adjustments")] 
    [Tooltip("How far the grappler can shoot")] public float grappleRange = 5f;
    public KeyCode grappleKey;
    public LayerMask whatIsGrappleable;
    [Tooltip("The force that the joint uses to try to keep the two objects a certain distance away")] public float jointSpringForce = 4.5f;
    [Tooltip("How strong the force to smooth out the spring force curve is")] public float jointDamperForce = 7f;
    [Tooltip("How accurate the amount of forced applied to each object based on their mass is")] public float jointMassScale = 4.5f;
    [Tooltip("How much the player gets pushed forward whenever they're grappling")] public float grappleForceModifier = 5f;
    private Vector3 currentGrapplePosition;
    // public float jointTolerance = 1f;

    
    [Header("References")]
    [Tooltip("The place where the grappler hooks from")] public Transform hookPoint;
    [Tooltip("Player Transform")] public Transform player;
    public Transform playerCamera;
    [Tooltip("Line Renderer component to cast a visible line")] public LineRenderer lineRenderer;

    private SpringJoint joint;
    private RaycastHit hit;  // stores hit info
    private Vector3 grapplePoint;

    private void Start()
    {
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }
        else if (Input.GetKeyUp(grappleKey))
        {
            StopGrapple();
        }
        
        // PushPlayer();
    }

    private void LateUpdate()
    {
        RenderLine();
    }

    // Code is from https://www.youtube.com/watch?v=Xgh4v1w5DxU
    private void StartGrapple()
    {
        // check raycast to see if grappleable object is in grapple range
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, grappleRange, whatIsGrappleable))
        {
            Debug.Log("Grappler hit " + hit.transform.name + "!");
            
            // Grapple joint
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            joint.anchor = player.position;
            
            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            Debug.Log(player.position + " " + grapplePoint + " " + distanceFromPoint);
            
            // The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.3f;
            joint.minDistance = 0.5f;

            
            // Joint modification code:
            joint.spring = jointSpringForce;
            joint.damper = jointDamperForce;
            joint.massScale = jointMassScale;

            lineRenderer.positionCount = 2;
            currentGrapplePosition = hookPoint.position;
        }
    }

    private void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }

    private void PushPlayer()
    {
        if (!joint) return;
        
        // push forward with force
        player.GetComponent<Rigidbody>().AddForce(playerCamera.forward * grappleForceModifier * Time.deltaTime, ForceMode.Impulse);
        Debug.DrawRay(player.position, playerCamera.forward, Color.cyan, 0.5f);
    }

    private void RenderLine()
    {
        // no joint, so don't draw the rope
        if (!joint) return;
        
        // rope animation
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lineRenderer.SetPosition(0, hookPoint.position);
        lineRenderer.SetPosition(1, currentGrapplePosition);
    }
}
