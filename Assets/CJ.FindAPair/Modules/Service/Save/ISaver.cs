using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaver
{
    event Action SaveCreated;
    SaveData LoadData();
    void SaveData(SaveData saveData);
}
