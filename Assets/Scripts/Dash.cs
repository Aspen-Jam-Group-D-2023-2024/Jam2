using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public CharacterController player;
    public float distance;

    private bool dashing = true;
    public float dashCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  

        if(Input.GetKeyDown(KeyCode.Q) && dashing)
        {
            StartCoroutine(dash());
        }

    }

    private IEnumerator dash()
    { 
        dashing = true;

        Vector3 move = transform.forward;
        player.Move(move * distance);
        dashing = false;

        yield return new WaitForSeconds(dashCooldown);
        dashing = true;
    }
    
}
