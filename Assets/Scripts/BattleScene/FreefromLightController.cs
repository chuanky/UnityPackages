using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreefromLightController : MonoBehaviour
{
    private bool dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 step = new Vector3(1f * Time.deltaTime, 0, 0);
        if (transform.position.x >= 2 || transform.position.x <= -6) dir = !dir;
        if (dir)
        {
            transform.position += step;
        } else
        {
            transform.position -= step;
        }

        //Debug.Log("deltaTime: " + Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //Debug.Log("fixedDeltaTime: " + Time.fixedDeltaTime);
    }

}
