using UnityEngine;

public class AstarAdapter : MonoBehaviour
{
    public void AstarScan()
    {
        AstarPath.active.Scan();
    }
}
