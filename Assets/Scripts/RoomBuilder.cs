using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    [Header("Dimensiuni cameră")]
    [SerializeField] private float width = 10f;   // lățime (X)
    [SerializeField] private float length = 10f;  // lungime (Z)
    [SerializeField] private float height = 4f;   // înălțime pereți (Y)
    [SerializeField] private float wallThickness = 0.2f;

    [Header("Material")]
    [SerializeField] private Material wallMaterial;
    [SerializeField] private bool addCeiling = true;

    [ContextMenu("Generează Camera")]
    public void GenerateRoom()
    {
        // Șterge pereții vechi generați anterior, dacă există
        ClearExistingWalls();

        CreateWall("Perete_Nord", new Vector3(0, height / 2f, length / 2f), new Vector3(width, height, wallThickness));
        CreateWall("Perete_Sud", new Vector3(0, height / 2f, -length / 2f), new Vector3(width, height, wallThickness));
        CreateWall("Perete_Est", new Vector3(width / 2f, height / 2f, 0), new Vector3(wallThickness, height, length));
        CreateWall("Perete_Vest", new Vector3(-width / 2f, height / 2f, 0), new Vector3(wallThickness, height, length));

        if (addCeiling)
        {
            CreateWall("Tavan", new Vector3(0, height, 0), new Vector3(width, wallThickness, length));
        }
    }

    private void CreateWall(string name, Vector3 localPosition, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = name;
        wall.transform.SetParent(transform);
        wall.transform.localPosition = localPosition;
        wall.transform.localScale = scale;

        if (wallMaterial != null)
        {
            wall.GetComponent<MeshRenderer>().sharedMaterial = wallMaterial;
        }
    }

    [ContextMenu("Șterge Camera")]
    private void ClearExistingWalls()
    {
        string[] names = { "Perete_Nord", "Perete_Sud", "Perete_Est", "Perete_Vest", "Tavan" };
        foreach (Transform child in transform)
        {
            foreach (string wallName in names)
            {
                if (child.name == wallName)
                {
                    DestroyImmediate(child.gameObject);
                    break;
                }
            }
        }
    }
}
