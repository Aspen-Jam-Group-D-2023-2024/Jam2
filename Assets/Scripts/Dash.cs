using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float distance;
    Rigidbody rig;
    private bool dashing = true;

    public float dashCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
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
        rig.AddForce(transform.forward * distance, ForceMode.Impulse);
        dashing = false;

        yield return new WaitForSeconds(dashCooldown);
        dashing = true;
    }
    
}
