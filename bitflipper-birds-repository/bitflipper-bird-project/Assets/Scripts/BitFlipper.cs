using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BitFlipper : MonoBehaviour
{
    [SerializeField] private TMP_Text m_TextAsset;
    [SerializeField] private int m_TimeToWait = 6;

    [SerializeField] [TextArea] private string m_OriginalText;
    
    private bool m_Recall = true;
    private int m_Index = 0;

    private void Update()
    {
        if (m_Recall)
            StartCoroutine(Flipper(m_TimeToWait));
    }

    private IEnumerator Flipper(int _timeToWait)
    {
        m_Recall = false;
        yield return new WaitForSeconds(_timeToWait);
        if (m_Index <= 3)
        {
            FlipText();
            m_Index++;
            m_Recall = true;
        }
        else
            ResetAll();
    }

    private void ResetAll()
    {
        m_TextAsset.text = m_OriginalText;
        m_Recall = true;
        m_Index = 0;
    }

    private void FlipText()
    {
        string[] allWords = m_TextAsset.text.Split(' ');

        for (int i = 0; i < 20; i++)
        {
            int random = Random.Range(0, allWords.Length);
            //Debug.Log("random word =" + allWords[random]);
            string binary = StringToBinary(allWords[random]);
            //Debug.Log("binary word = " + binary);
            string bitFlipped = BitFlipWord(binary);
            //Debug.Log("Bitflipped word = " + bitFlipped);
            string returned = BinaryToString(bitFlipped);
            //Debug.Log("returned word =" + returned);
            allWords[random] = returned;
        }

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < allWords.Length; i++)
        {
            builder.Append(allWords[i]);
            builder.Append(" ");
        }

        m_TextAsset.text = builder.ToString();
    }
    
    private string BitFlipWord(string _binary)
    {
        char[] byteChar = _binary.ToCharArray();

        int[] bytes = new int[byteChar.Length];

        for (int i = 0; i < byteChar.Length; i++)
        {
            bytes[i] = Convert.ToInt32(byteChar[i].ToString());
        }
        
        for (int i = 0; i < bytes.Length; i++)
        {
            //Debug.Log("byte before = " + bytes[i].ToString());
            if (bytes[i] == 0)
                bytes[i] = 1;
            else if (bytes[i] == 1)
                bytes[i] = 0;
            //Debug.Log("byte after = " + bytes[i].ToString());
        }
        
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append((bytes[i]));
        }

        return builder.ToString();
    }

    private string StringToBinary(string _data)
    {
        StringBuilder sb = new StringBuilder();
 
        foreach (char c in _data.ToCharArray())
        {
            sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
        }
        return sb.ToString();
    }

    private string BinaryToString(string _data)
    {
        List<Byte> byteList = new List<Byte>();
 
        for (int i = 0; i < _data.Length; i += 8)
        {
            byteList.Add(Convert.ToByte(_data.Substring(i, 8), 2));
        }
        return Encoding.ASCII.GetString(byteList.ToArray());
    }
}
