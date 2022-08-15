using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(rotateCube(0.5f));
    }

    IEnumerator rotateCube(float speed)
    {
        while(true)
        {
            transform.Rotate(0, speed, 0, Space.World);

            yield return null;
        }
    }
}
