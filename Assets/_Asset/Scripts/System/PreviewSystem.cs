using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previewYOffset = 0.06f;
    [SerializeField] private Material previewMaterial;
    [SerializeField] private GameObject previewObject;
    [SerializeField] private GameObject previewObjectInstance;

    private void Update()
    {
        if (previewObjectInstance == null || previewObject == null) return;
    }

    public void StartPreview(GameObject previewObject)
    {
        this.previewObject = previewObject;
        previewObjectInstance = Instantiate(previewObject);
        var materials = previewObjectInstance.GetComponentsInChildren<MeshRenderer>();
        foreach (var material in materials)
        {
            material.material = previewMaterial;
        }
    }

    public void StopPreview()
    {
        Destroy(previewObjectInstance);
        previewObject = null;
    }

    public void UpdatePreview(bool isValid)
    {
        Color color = isValid ? Color.white : Color.red;
        color.a = previewMaterial.color.a;
        previewMaterial.SetColor("_Color", color);
    }

    public void MovePreview(Vector3 position)
    {
        if (previewObjectInstance == null) return;
        previewObjectInstance.transform.position = position;
    }
}
