using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_A_GrabPosit : MonoBehaviour
{

    [SerializeField] public bool _Enable = true;



    abstract public GameObject[] GetNearPosit(Vector3 Posit);


}
