using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class S_PositChest : S_A_GrabPosit{

    [SerializeField] public GameObject H_Point1;
    [SerializeField] public GameObject H_Point2;





    override public GameObject[] GetNearPosit(Vector3 Posit) {

        bool X1 = NavMesh.CalculatePath(Posit , H_Point1.transform.position , NavMesh.AllAreas , new NavMeshPath());
        bool X2 = NavMesh.CalculatePath(Posit , H_Point2.transform.position, NavMesh.AllAreas, new NavMeshPath());


        if (X1 && !X2) return new GameObject[] { H_Point1, H_Point2 };
        if (!X1 && X2) return new GameObject[] { H_Point2, H_Point1 };
        if (!X1 && !X2) return null;

        float Dis1 = Vector3.Distance(H_Point1.transform.position, Posit);
        float Dis2 = Vector3.Distance(H_Point2.transform.position, Posit);

        if (Dis1 < Dis2) return new GameObject[] { H_Point1, H_Point2 };
        return new GameObject[] { H_Point2, H_Point1 };

    }










}
