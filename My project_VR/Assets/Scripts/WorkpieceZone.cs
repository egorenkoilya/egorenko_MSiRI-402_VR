
using UnityEngine;

public class WorkpieceZone : MonoBehaviour
{
    [Header("Обрабатываемая заготовка")]
    public Rigidbody workpiece;

    [Header("Точка фиксации")]
    public Transform snapPoint;

    [Header("Настройки")]
    public float attachDelay = 0.25f;
    public float maxSpeed = 0.15f;

    private float stableTime;
    private bool attached;

    private void OnTriggerStay(Collider other)
    {
        if (attached || workpiece == null)
            return;

        if (other.attachedRigidbody != workpiece)
            return;

        // Ждём, пока заготовку отпустят
        if (workpiece.isKinematic)
        {
            stableTime = 0f;
            return;
        }

        // Проверяем скорость движения
        if (workpiece.linearVelocity.magnitude > maxSpeed)
        {
            stableTime = 0f;
            return;
        }

        stableTime += Time.fixedDeltaTime;

        if (stableTime >= attachDelay)
            AttachWorkpiece();
    }

    private void AttachWorkpiece()
    {
        Transform point = snapPoint != null
            ? snapPoint
            : transform;

        workpiece.linearVelocity = Vector3.zero;
        workpiece.angularVelocity = Vector3.zero;

        workpiece.transform.SetPositionAndRotation(
            point.position,
            point.rotation
        );

        workpiece.transform.SetParent(point);

        workpiece.isKinematic = true;
        workpiece.useGravity = false;

        attached = true;

        Debug.Log("Заготовка зафиксирована в зоне обработки!");
    }

    public void ReleaseWorkpiece()
    {
        if (!attached || workpiece == null)
            return;

        workpiece.transform.SetParent(null);
        workpiece.isKinematic = false;
        workpiece.useGravity = true;

        attached = false;
        stableTime = 0f;
    }
}
