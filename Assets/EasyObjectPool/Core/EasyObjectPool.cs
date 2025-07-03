/* 
 * Unless otherwise licensed, this file cannot be copied or redistributed in any format without the explicit consent of the author.
 * (c) Preet Kamal Singh Minhas, http://marchingbytes.com
 * contact@marchingbytes.com
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MarchingBytes {

	[System.Serializable]
	public class PoolInfo {
		public string poolName;
		public GameObject prefab;
		public int poolSize;
		public bool fixedSize;
	}

public class Pool
{
    private Stack<PoolObject> availableObjStack = new Stack<PoolObject>();
    private GameObject poolObjectPrefab;
    private string poolName;
    private bool fixedSize;
    private int poolSize;

    private EasyObjectPool owner;

    public Pool(string poolName, GameObject poolObjectPrefab, int initialCount, bool fixedSize, EasyObjectPool owner)
    {
        this.poolName = poolName;
        this.poolObjectPrefab = poolObjectPrefab;
        this.poolSize = initialCount;
        this.fixedSize = fixedSize;
        this.owner = owner;

        for (int i = 0; i < initialCount; i++)
        {
            AddObjectToPool(NewObjectInstance());
        }
    }

    private void AddObjectToPool(PoolObject po)
    {
        po.gameObject.SetActive(false);
        availableObjStack.Push(po);
        po.isPooled = true;
    }

    private PoolObject NewObjectInstance()
    {
        GameObject go = GameObject.Instantiate(poolObjectPrefab);
        PoolObject po = go.GetComponent<PoolObject>();
        if (po == null)
        {
            po = go.AddComponent<PoolObject>();
        }
        po.poolName = poolName;
        return po;
    }

    public GameObject NextAvailableObject(Vector3 position, Quaternion rotation)
    {
        PoolObject po = null;
        if (availableObjStack.Count > 0)
        {
            po = availableObjStack.Pop();
        }
        else if (!fixedSize)
        {
            poolSize++;
            po = NewObjectInstance();
        }
        else
        {
            Debug.LogWarning("No available object in pool: " + poolName);
            return null;
        }

        GameObject result = po.gameObject;
        po.isPooled = false;
        result.SetActive(true);
        result.transform.position = position;
        result.transform.rotation = rotation;

        owner.OnSpawn(result); // Gọi hàm custom khi spawn

        return result;
    }

    public void ReturnObjectToPool(PoolObject po)
    {
        if (po.isPooled)
        {
            Debug.LogWarning(po.gameObject.name + " is already in pool.");
            return;
        }

        owner.OnReturn(po.gameObject); // Gọi hàm custom khi return

        AddObjectToPool(po);
    }
}

	/// <summary>
	/// Easy object pool.
	/// </summary>
	public class EasyObjectPool : MonoBehaviour {

		// public static EasyObjectPool instance;
		[Header("Editing Pool Info value at runtime has no effect")]
		public List<PoolInfo> poolInfo;

		//mapping of pool name vs list
		private Dictionary<string, Pool> poolDictionary  = new Dictionary<string, Pool>();
		
		// Use this for initialization
		void Start () {
			//set instance
			//instance = this;
			//check for duplicate names
			CheckForDuplicatePoolNames();
			//create pools
			CreatePools();
		}
		
		private void CheckForDuplicatePoolNames() {
			for (int index = 0; index < poolInfo.Count; index++) {
				string poolName = poolInfo[index].poolName;
				if(poolName.Length == 0) {
					Debug.LogError(string.Format("Pool {0} does not have a name!",index));
				}
				for (int internalIndex = index + 1; internalIndex < poolInfo.Count; internalIndex++) {
					if(poolName.Equals(poolInfo[internalIndex].poolName)) {
						Debug.LogError(string.Format("Pool {0} & {1} have the same name. Assign different names.", index, internalIndex));
					}
				}
			}
		}

		private void CreatePools() {
			foreach (PoolInfo currentPoolInfo in poolInfo) {
				
				Pool pool = new Pool(currentPoolInfo.poolName, currentPoolInfo.prefab, 
				                     currentPoolInfo.poolSize, currentPoolInfo.fixedSize,this);

				
				Debug.Log("Creating pool: " + currentPoolInfo.poolName);
				//add to mapping dict
				poolDictionary[currentPoolInfo.poolName] = pool;
			}
		}


		/* Returns an available object from the pool 
		OR 
		null in case the pool does not have any object available & can grow size is false.
		*/
		public GameObject GetObjectFromPool(string poolName, Vector3 position, Quaternion rotation) {
			GameObject result = null;
			
			if(poolDictionary.ContainsKey(poolName)) {
				Pool pool = poolDictionary[poolName];
				result = pool.NextAvailableObject(position,rotation);
				//scenario when no available object is found in pool
				if(result == null) {
					Debug.LogWarning("No object available in pool. Consider setting fixedSize to false.: " + poolName);
				}
				
			} else {
				Debug.LogError("Invalid pool name specified: " + poolName);
			}
			
			return result;
		}

		public void ReturnObjectToPool(GameObject go) {
			PoolObject po = go.GetComponent<PoolObject>();
			if(po == null) {
				Debug.LogWarning("Specified object is not a pooled instance: " + go.name);
			} else {
				if(poolDictionary.ContainsKey(po.poolName)) {
					Pool pool = poolDictionary[po.poolName];
					pool.ReturnObjectToPool(po);
				} else {
					Debug.LogWarning("No pool available with name: " + po.poolName);
				}
			}
		}
		// Cho phép custom khi spawn object
		public virtual void OnSpawn(GameObject obj) { }

		// Cho phép custom khi return object
		public virtual void OnReturn(GameObject obj) { }
		// lấy Data từ ScriptableObject
		public virtual void GetData() { }
	}
}
