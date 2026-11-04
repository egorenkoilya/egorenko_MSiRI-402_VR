using UnityEngine;

public class Car : MonoBehaviour
{
    public string Brand = "BMW";
    public string Model = "M5";
    public int Year = 2022;
    public string Color = "Черный";

    void Start()
    {
        ShowInfo();
    }

    public void ShowInfo()
    {
        Debug.Log("=== Информация об автомобиле ===");
        Debug.Log("Марка: " + Brand);
        Debug.Log("Модель: " + Model);
        Debug.Log("Год: " + Year);
        Debug.Log("Цвет: " + Color);
    }

    public void StartEngine()
    {
        Debug.Log(Brand + " " + Model + ": двигатель запущен!");
    }

    public void StopEngine()
    {
        Debug.Log(Brand + " " + Model + ": двигатель остановлен!");
    }
}