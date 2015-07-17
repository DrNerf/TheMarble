using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Drawline : MonoBehaviour
{
    public GameObject TrailObject;
    public float NewObjectDistance = 1;
    public float SizeRatio = 1;

    private Vector3 CurrentObjectStartPosition;
    private GameObject CurrentObject;
    GameObject[] CollidersPool = new GameObject[20];
    LineRenderer Line;

    void Awake()
    {
        Line = GetComponent<LineRenderer>();
        for (int i = 0; i < CollidersPool.Length; i++)
        {
            CollidersPool[i] = Instantiate(TrailObject, transform.position, Quaternion.identity) as GameObject;
            CollidersPool[i].SetActive(false);
        }
    }

    void Update()
    {
        if (CurrentObject == null)
        {
            CurrentObject = GetAvailableColliderFromPool();
            //CurrentObject = GetAvailableColliderFromPool();
            CurrentObjectStartPosition = transform.position;
        }
        else
        {
            Vector3 toTransformVector = transform.position - CurrentObjectStartPosition;
            CurrentObject.transform.rotation = (toTransformVector == Vector3.zero) ? Quaternion.identity : Quaternion.LookRotation(toTransformVector);
            Vector3 newScale = CurrentObject.transform.localScale;
            float distance = toTransformVector.magnitude;
            newScale.z = distance * SizeRatio;
            CurrentObject.transform.localScale = newScale;

            if (distance >= NewObjectDistance)
            {
                CurrentObject = GetAvailableColliderFromPool();
                CurrentObjectStartPosition = transform.position;
            }
        }
    }

    GameObject GetAvailableColliderFromPool()
    {
        for (int i = 0; i < CollidersPool.Length; i++)
        {
            if (!CollidersPool[i].activeSelf)
            {
                CollidersPool[i].SetActive(true);
                CollidersPool[i].transform.position = transform.position;
                Line.SetVertexCount(i + 1);
                Line.SetPosition(i, transform.position);
                return CollidersPool[i];
            }
        }
        //foreach (var item in CollidersPool)
        //{
        //    item.SetActive(false);
        //}
        GameObject temp = CollidersPool[0];
        for (int i = 0; i < CollidersPool.Length - 1; i++)
        {
            CollidersPool[i] = CollidersPool[i + 1];
        }
        CollidersPool[CollidersPool.Length - 1] = temp;
        temp.transform.position = transform.position;
        for (int i = 0; i < CollidersPool.Length; i++)
        {
            Line.SetPosition(i, CollidersPool[i].transform.position);
        }
        return CollidersPool[CollidersPool.Length - 1];
    }

    public void Restart()
    {
        foreach (var item in CollidersPool)
        {
            item.SetActive(false);
            Line.SetVertexCount(0);
        }
    }
}