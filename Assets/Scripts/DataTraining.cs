using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataTraining
{
    private static bool _trainingComplite = false;

    public static bool CheckTraining()
    {
        if (_trainingComplite) return true;
        else return false;
    }

    public static void EndTraining()
    {
        _trainingComplite = true;
    }
}
