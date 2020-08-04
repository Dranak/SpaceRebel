using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [Range(1, 10)]
    public int Density;

    public int SizePooller = 5;

    

    public bool CanPlace()
    {

        // Validation check to see if element can be placed. More detailed calculations can go here, such as checking perlin noise.

        if (Random.Range(0, 10) < Density)
            return true;
        else
            return false;

    }



}