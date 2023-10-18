using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// THIS SCRIPT ASSUMES YOU HAVE A MOVEMENT SCRIPT ATTACHED TO THE PLAYER
public class Grappler : MonoBehaviour
{
    [Header("Grappling adjustments")] 
    [Tooltip("How far the grappler can shoot")] public float grappleRange = 5f;
    public KeyCode grappleKey;
    public LayerMask grappleLayer;
    public float jointSpringForce = 5f;
    public float jointDamperForce = 0.5f;
    public float jointMassScale = 1f;

    
    [Header("References")]
    [Tooltip("The place where the grappler hooks from")] public Transform hookPoint;
    [Tooltip("Player Transform")] public Transform player;
    public Transform playerCamera;
    [Tooltip("Line Renderer component to cast a visible line")] public LineRenderer lineRenderer;
    private Movement mvment;

    private SpringJoint joint;
    private RaycastHit hit;  // stores hit info
    private Vector3 grapplePoint;
    [HideInInspector] public bool grappling;  // public flag that denotes if we are currently trying to grapple

    private void Start()
    {
        mvment = player.GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            grappling = true;
        }
        else
        {
            grappling = false;
        }
        
        RenderLine();
    }

    private void FixedUpdate()
    {
        if (grappling)
        {
            StartGrapple();
        }
        else
        {
            StopGrapple();
        }
    }

    private void StartGrapple()
    {
        // check raycast to see if grappleable object is in grapple range
        if (Physics.Raycast(player.position, mvment.lookDir, out hit, grappleRange, grappleLayer))
        {
            Debug.Log("Grappled!");
            grapplePoint = hit.point;
            
            // Grapple joint
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            
            // Joint modification code:
            joint.spring = jointSpringForce;
            joint.damper = jointDamperForce;
            joint.massScale = jointMassScale;
        }
    }

    private void StopGrapple()
    {
        Destroy(joint);
    }

    private void RenderLine()
    {
        if (grappling && joint)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.positionCount = 1;
        }
    }
}
