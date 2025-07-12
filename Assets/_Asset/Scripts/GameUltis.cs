using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class GameUltis {

    public static void ReplaceArrayElements<T>(T[] a, T[] b) {
        if (a.Length != b.Length) {
            Debug.LogError("Both arrays must have the same number of elements.");
            return;
        }

        for (int i = 0; i < a.Length; i++) {
            a[i] = b[i];
        }
    }
    public static void ShowObjInArray(int value, GameObject[] gameObjects) {
        HideAllObjectInArray(gameObjects);
        gameObjects[value].SetActive(true);
    }

    public static void HideAllObjectInArray(GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects) {
            gameObject.SetActive(false);
        }
    }
    public static IEnumerator IEDelayCall(float time, Action Callback) {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }

    public static int RandomNumber(int a, int b) {
        return UnityEngine.Random.Range(a, b + 1);
    }
    public static T GetComponentFromObject<T>(GameObject obj) where T : Component {
        return obj.GetComponent<T>();
    }


    public static int GenerateRandomValue(int startValue, int endValue, int[] notTargetValues) {
        int randomValue;

        do {
            randomValue = UnityEngine.Random.Range(startValue, endValue + 1);
        } while (notTargetValues.Contains(randomValue));
        return randomValue;
    }

    public static string FormatNumber(int number) {
        string numStr = number.ToString();
        char[] numArray = numStr.ToCharArray();
        Array.Reverse(numArray);
        string reversedStr = new string(numArray);

        string result = "";
        for (int i = 0; i < reversedStr.Length; i++) {
            if (i > 0 && i % 3 == 0) {
                result += ".";
            }
            result += reversedStr[i];
        }

        char[] resultArray = result.ToCharArray();
        Array.Reverse(resultArray);
        return new string(resultArray);
    }
    

    public static void SetParent(GameObject obj, Transform parent) {
        obj.transform.SetParent(parent, false);
    }
    public static void Hide(GameObject obj) {
        if (obj.activeSelf)
            obj.gameObject.SetActive(false);
    }
    public static void Show(GameObject obj) {
        if (!obj.activeSelf)
            obj.gameObject.SetActive(true);
    }
    public static bool ExitScreen(Vector3 currentPosition) {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(currentPosition);
        return (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height * 2);
    }


    public static bool ExitLeftScreen(Vector3 currentPosition) {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(currentPosition);
        return (screenPosition.x < 0);
    }
    public static bool ExitRightScreen(Vector3 currentPosition) {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(currentPosition);
        return (screenPosition.x > Screen.width);
    }
    
    public static T[] RemoveFirstElement<T>(T[] array)
    {
        T[] result = new T[array.Length - 1];
        Array.Copy(array, 1, result, 0, array.Length - 1);
        return result;
    }

    public static T GetOrAddComponent<T>(this GameObject self) where T : Component
    {
        var component = self.GetComponent<T>();
        if (component == null)
        {
            component = self.AddComponent<T>();
        }

        return component;
    }
    
    public static T GetOrAddComponent<T>(this MonoBehaviour self) where T : Component
    {
        return self.gameObject.GetOrAddComponent<T>();
    }
    
    public static void LogObjectInArray<T>(T[] array)
    {
        if (array == null || array.Length == 0)
        {
            Debug.Log("Array is empty");
            return;
        }
        foreach (var tmp in array)
        {
            Debug.Log(tmp.ToString());
        }
    }

    public static Vector2Int GetSize(GameObject obj)
    {
        var collider = obj.GetComponent<Collider>();
        if (collider == null) return new Vector2Int(0, 0);
        var boundsSize = collider.bounds.size;
        Vector2 size = new Vector2(boundsSize.x,boundsSize.z);
        Vector2Int objSize = Vector2Int.RoundToInt(size);
        return objSize;
    }

    public static Vector3 GetCellPosition(Grid grid, Vector3 position)
    {
        var gridPos = grid.WorldToCell(position);
        return grid.CellToWorld(gridPos);
    }
    public static Vector3Int GetCellPositionInt(Grid grid, Vector3 position)
    {
        return grid.WorldToCell(position);
    }

}
