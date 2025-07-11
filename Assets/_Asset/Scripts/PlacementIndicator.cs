using System;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private Color validColor = new Color(0, 1, 0, 0.5f);   // Xanh trong suốt
    [SerializeField] private Color invalidColor = new Color(1, 0, 0, 0.5f); // Đỏ trong suốt
    [SerializeField] private GameObject currentTower;
    private Material indicatorMaterial;

    public GameObject CurrentTower
    {
        get => currentTower;
        set => currentTower = value;
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        indicatorMaterial = meshRenderer.material;
    }

    private void FixedUpdate()
    {
        var isValidPosition = IsValidPosition();
        SetValid(isValidPosition);
    }

    public void SetPosition(Vector3 worldPosition)
    {
        transform.position = worldPosition;   
    }

    public void SetValid(bool isValid)
    {
        if (indicatorMaterial != null)
        {
            indicatorMaterial.color = isValid ? validColor : invalidColor;
        }
        else
        {
            Debug.Log("indicator material is null");
        }
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }

    public bool IsValidPosition()
    {
        var collliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (var colllider in collliders)
        {
            var obj = colllider.gameObject;
            if (obj.CompareTag("Obstacle") || obj.CompareTag("Road"))
            {
                Debug.Log($"{obj.name} is not valid");
                return false;
            }

            if (obj.CompareTag("Tower") && obj != currentTower)
            {
                return false;
            }
        }
        return true;
    }
}