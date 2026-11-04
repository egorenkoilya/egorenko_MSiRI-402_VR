
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Shape
    {
        public string name;
        public int count;
    }

    public Shape[] shapes =
    {
        new Shape { name = "Cube", count = 3 },
        new Shape { name = "Sphere", count = 2 },
        new Shape { name = "Cylinder", count = 4 },
        new Shape { name = "Capsule", count = 2 }
    };

    void Start()
    {
        int position = 0;

        foreach (Shape shape in shapes)
        {
            for (int i = 0; i < shape.count; i++)
            {
                GameObject obj = null;

                switch (shape.name)
                {
                    case "Cube":
                        obj = GameObject.CreatePrimitive(
                            PrimitiveType.Cube);
                        break;

                    case "Sphere":
                        obj = GameObject.CreatePrimitive(
                            PrimitiveType.Sphere);
                        break;

                    case "Cylinder":
                        obj = GameObject.CreatePrimitive(
                            PrimitiveType.Cylinder);
                        break;

                    case "Capsule":
                        obj = GameObject.CreatePrimitive(
                            PrimitiveType.Capsule);
                        break;
                }

                if (obj != null)
                {
                    obj.name = shape.name + "_" + (i + 1);

                    obj.transform.position =
                        new Vector3(position * 2, 1, 0);

                    obj.AddComponent<Rigidbody>();

                    obj.AddComponent<CollisionHandler>();

                    position++;
                }
            }
        }

        Debug.Log("Все фигуры созданы!");
    }
}
