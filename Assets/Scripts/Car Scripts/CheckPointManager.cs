using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private Vector3 lastCheckPointPosition;
    private Quaternion lastCheckPointRotation;

    public void SetCheckPoint(Transform checkPointPosition)
    {
        lastCheckPointPosition = checkPointPosition.position;
        lastCheckPointRotation = Quaternion.Euler(0,180,0);
    }

    public Vector3 GetCheckPointPosition() => lastCheckPointPosition;
    public Quaternion GetCheckPointRotation() => lastCheckPointRotation;
}
