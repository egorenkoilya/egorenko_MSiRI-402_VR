using UnityEngine;

public class Driver : MonoBehaviour
{
    public string Name = "Илья";
    public int Age = 20;

    void Start()
    {
        Car myCar = GetComponent<Car>();

        Debug.Log("=== Информация о водителе ===");
        Debug.Log("Имя: " + Name);
        Debug.Log("Возраст: " + Age);

        Debug.Log("=== Обращение к Car ===");

        Debug.Log("Марка: " + myCar.Brand);
        Debug.Log("Модель: " + myCar.Model);

        myCar.ShowInfo();
        myCar.StartEngine();
        myCar.StopEngine();
    }
}