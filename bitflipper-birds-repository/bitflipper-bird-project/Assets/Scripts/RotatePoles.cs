using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePoles : MonoBehaviour
{
    [SerializeField] private GameObject m_RotateParent;

    private void Update()
    {
        transform.RotateAround(m_RotateParent.transform.position, Vector3.forward, 30 * Time.deltaTime);
    }
}
