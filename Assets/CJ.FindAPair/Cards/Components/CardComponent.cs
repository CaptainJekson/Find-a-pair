using Leopotam.Ecs;
using UnityEngine.UI;

public class CardComponent : IEcsAutoReset
{
    public Button Button;
    public int Number;

    public void Reset()
    {
        Button = null;
    }
}
