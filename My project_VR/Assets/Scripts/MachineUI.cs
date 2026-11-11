
using UnityEngine;
using TMPro;

public class MachineUI : MonoBehaviour
{
    [Header("Текст на экране")]
    public TMP_Text stateText;
    public TMP_Text axesText;
    public TMP_Text messageText;

    [Header("Деталь станка")]
    public Transform machinePart;

    [Header("Вращение")]
    public float rotationSpeed = 90f;
    public Vector3 rotationAxis = Vector3.right;

    private bool isRunning = false;

    void Start()
    {
        SetState("Остановлен");
        SetMessage("Станок готов к работе");
    }

    void Update()
    {
        if (machinePart != null)
        {
            Vector3 pos = machinePart.localPosition;

            axesText.text =
                $"X: {pos.x:F2} мм | Y: {pos.y:F2} мм | Z: {pos.z:F2} мм";

            if (isRunning)
            {
                machinePart.Rotate(
                    rotationAxis.normalized,
                    rotationSpeed * Time.deltaTime,
                    Space.Self
                );
            }
        }
    }

    public void StartMachine()
    {
        isRunning = true;
        SetState("Работает");
        SetMessage("Станок запущен");
    }

    public void StopMachine()
    {
        isRunning = false;
        SetState("Остановлен");
        SetMessage("Станок остановлен");
    }

    public void SetState(string state)
    {
        if (stateText != null)
            stateText.text = "Состояние: " + state;
    }

    public void SetMessage(string message)
    {
        if (messageText != null)
            messageText.text = message;
    }
}
