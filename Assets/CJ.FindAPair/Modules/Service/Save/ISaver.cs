using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaver
{
    SaveData LoadData();
    void SaveData(SaveData saveData);
}
