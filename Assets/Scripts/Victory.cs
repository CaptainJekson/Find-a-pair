using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Victory : MonoBehaviour
{
    public event UnityAction LevelUp;

    public void OnNextLevelButtonClick()
    {
        LevelUp?.Invoke();
    }
}
