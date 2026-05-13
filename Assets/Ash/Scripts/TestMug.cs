using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TestMug : MonoBehaviour
{
    public int glassType;
    public int garnishType;
    public int[] drinkTypes;
    public int maxSize = 3;

    private void Awake()
    {
        drinkTypes = new int[4];
    }
}
