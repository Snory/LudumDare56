using UnityEngine;

public class GeneralEventRaiser : MonoBehaviour
{
    public GeneralEvent GeneralEventToRaise;
    public void Raise()
    {
        GeneralEventToRaise.Raise();
    }
}
