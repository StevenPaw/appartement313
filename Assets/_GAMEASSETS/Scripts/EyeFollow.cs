using System;
using System.Collections;
using System.Collections.Generic;
using _GAMEASSETS.Scripts;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EyeFollow : MonoBehaviour
{
    [SerializeField] private GameObject eyes;
    [SerializeField] private float leftPosition;
    [SerializeField] private float frontPosition;
    [SerializeField] private float rightPosition;
    [SerializeField] private float minDistance;
    private GameObject playerGO;

    private void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag(GameTags.PLAYER);
    }

    private void Update()
    {
        Vector3 newPos = eyes.transform.localPosition;
        if (playerGO.transform.position.x < eyes.transform.position.x - minDistance)
        {
            newPos.x = leftPosition;
            eyes.transform.localPosition = newPos;
        } 
        else if (playerGO.transform.position.x > eyes.transform.position.x + minDistance)
        {
            newPos.x = rightPosition;
            eyes.transform.localPosition = newPos;
        }
        else
        {
            newPos.x = frontPosition;
            eyes.transform.localPosition = newPos;
        }
    }
}
