using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_FinalPositions;
    [SerializeField] private GameObject[] m_StorkObjects;

    private int m_TimeToWait = 6;
    private bool m_Recall = true;
    private int m_StorkIndex = 0;

    private void Update()
    {
        if (m_Recall)
            StartCoroutine(AwakenStork(m_TimeToWait));
    }

    private IEnumerator AwakenStork(int _timeToWait)
    {
        m_Recall = false;
        yield return new WaitForSeconds(_timeToWait);
        if (m_StorkIndex <= 3)
        {
            m_StorkObjects[m_StorkIndex].GetComponent<Stork>().StartMigrating(m_FinalPositions[m_StorkIndex]);
            m_Recall = true;
            m_StorkIndex++;
        }
        else
            ResetAll();
    }

    private void ResetAll()
    {
        for (int i = 0; i < m_StorkObjects.Length; i++)
        {
            m_StorkObjects[i].GetComponent<Stork>().ResetStork();
        }
        m_StorkIndex = 0;
        m_Recall = true;
    }
}
