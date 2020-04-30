using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamIsoScript : MonoBehaviour
{
    public Transform playerTrans;
    public Transform cameraTrans;
    public Vector3 test;
    private int zTrans = -10;

    void Update()
    {
        test = playerTrans.transform.position;
        test.z = zTrans;
        Camera.main.transform.position = test;
    }
}
