using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stork : MonoBehaviour
{
    private bool m_StartMigration = false;
    private GameObject m_MigrationDestination;
    private Vector3 m_StartPosition;

    private void Start()
    {
        m_StartPosition = transform.position;
    }

    private void Update()
    {
        if(m_StartMigration)
            transform.position = Vector3.MoveTowards(transform.position, m_MigrationDestination.transform.position, Time.deltaTime * 1.5f);
    }

    public void StartMigrating(GameObject _migrationDestination)
    {
        m_MigrationDestination = _migrationDestination;
        m_StartMigration = true;
    }

    public void ResetStork()
    {
        transform.position = m_StartPosition;
        m_StartMigration = false;
        m_MigrationDestination = null;
    }
}
