using UnityEngine;

public class ResetToCheckpointCommand : ICommand
{
    private Transform carTransform;
    private Vector3 checkPointPosition;
    private Quaternion checkPointRotation;

    public ResetToCheckpointCommand(Transform carTransform, Vector3 checkPointPosition, Quaternion checkPointRotation)
    {
        this.carTransform = carTransform;
        this.checkPointPosition = checkPointPosition;
        this.checkPointRotation = checkPointRotation;
    }

    public void Execute()
    {
        carTransform.position = checkPointPosition;
        carTransform.rotation = checkPointRotation;
    }

    public void Undo()
    {

    }
}
