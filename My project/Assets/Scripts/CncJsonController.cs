
using System;
using System.Collections;
using System.Globalization;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class CncJsonController : MonoBehaviour
{
    [Header("Подключение к ЧПУ")]
    public string url = "https://cnc.kovalev.team/get/3";
    public int channel = 1;
    public float interval = 0.5f;

    [Header("Управляющие узлы")]
    public Transform nodeX;
    public Transform nodeY;
    public Transform nodeZ;

    [Header("Вращение патрона")]
    public Transform rotatingChuck;
    public float chuckRotationSpeed = 60f;

    private Vector3 startX;
    private Vector3 startY;
    private Vector3 startZ;

    private IEnumerator Start()
    {
        if (nodeX == null || nodeY == null || nodeZ == null)
        {
            Debug.LogError("Назначьте три узла станка в Inspector.");
            yield break;
        }

        if (rotatingChuck == null)
        {
            Debug.LogError("Назначьте корпус патрона в Inspector.");
            yield break;
        }

        startX = nodeX.localPosition;
        startY = nodeY.localPosition;
        startZ = nodeZ.localPosition;

        Debug.Log("CNC станок подключен. Управление запущено.");

        while (true)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.timeout = 10;
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        MoveNodes(request.downloadHandler.text);
                    }
                    catch (Exception error)
                    {
                        Debug.LogWarning("Ошибка чтения JSON: " + error.Message);
                    }
                }
                else
                {
                    Debug.LogWarning("Ошибка запроса: " + request.error);
                }
            }

            yield return new WaitForSecondsRealtime(
                Mathf.Max(0.1f, interval));
        }
    }

    private void Update()
    {
        if (rotatingChuck != null)
        {
            rotatingChuck.Rotate(
                Vector3.forward,
                chuckRotationSpeed * Time.deltaTime,
                Space.Self
            );
        }
    }

    private float GetCoordinate(JObject data, string axis)
    {
        foreach (JToken section in data["data"])
        {
            if ((string)section[0] != "Оси Канала " + channel)
                continue;

            foreach (JToken row in section[1])
            {
                if ((string)row[0] == "Перемещение по оси " + axis)
                {
                    string text = ((string)row[1])
                        .Replace("мм", "").Trim();

                    text = text.Replace(',', '.');

                    float value = float.Parse(
                        text, CultureInfo.InvariantCulture);

                    if (float.IsNaN(value) || float.IsInfinity(value))
                        throw new FormatException(
                            "Некорректная координата " + axis);

                    return value;
                }
            }
        }

        throw new FormatException("Не найдена координата " + axis);
    }

    private void MoveNodes(string json)
    {
        JObject data = JObject.Parse(json);

        // Увеличенный масштаб перемещения
        float x = GetCoordinate(data, "X") * 0.003f;
        float y = GetCoordinate(data, "Y") * 0.003f;
        float z = GetCoordinate(data, "Z") * 0.003f;

        nodeX.localPosition = startX + Vector3.right * x;
        nodeY.localPosition = startY + Vector3.forward * y;
        nodeZ.localPosition = startZ + Vector3.up * z;
    }
}
