using System.Collections.Generic;
using UnityEngine;

public class ObjectPool: MonoBehaviour {
    private Dictionary<string, Queue<GameObject>> objectMap;

    private static ObjectPool instance;

    public int Length { get => Instance.Length; }

    public static ObjectPool Instance { get => instance; }

    private void Awake() {
        instance = this;
        objectMap = new Dictionary<string, Queue<GameObject>>();
    }

    /// <summary>
    /// 从对象池获取对象的一个实例
    /// - 注意：获取到的对象将为初始状态
    /// - 注意：此方法将会把父物体设为对象池
    /// </summary>
    /// <param name="prefab">对象预制件</param>
    /// <returns>返回对象的一个实例</returns>
    public GameObject GetObject(GameObject prefab, Transform transform = null, Vector3 position = default, bool relative = false) {
        string objName = Utils.RemoveClone(prefab.name);
        if (objectMap.ContainsKey(objName)) {
            var queue = objectMap[objName];
            if (queue.Count == 0) {
                GameObject obj = Instantiate(prefab, transform);
                obj.name = objName;
                if (relative)
                    obj.transform.localPosition = position;
                else
                    obj.transform.position = position;
                return obj;
            } else {
                var obj = queue.Dequeue();
                obj.transform.parent = transform;
                if (relative)
                    obj.transform.localPosition = position;
                else
                    obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }
        } else {
            objectMap.Add(objName, new Queue<GameObject>());
            GameObject seperater = new GameObject();
            seperater.name = objName;
            seperater.transform.SetParent(this.transform);
            GameObject obj = Instantiate(prefab, transform);
            if (relative)
                obj.transform.localPosition = position;
            else
                obj.transform.position = position;
            return obj;
        }
    }

    /// <summary>
    /// 将物体放入对象池
    /// - 注意：在放入对象池之前必须将对象初始化，以清楚脏数据
    /// </summary>
    /// <param name="obj">已初始化的对象</param>
    public void Push(GameObject obj) {
        string objName = Utils.RemoveClone(obj.name);
        obj.SetActive(false);
        if (objectMap.ContainsKey(objName)) {
            objectMap[objName].Enqueue(obj);
            var separator = GameObject.Find($"/ObjectPool/{objName}");
            if (separator != null) {
                obj.transform.SetParent(separator.transform);
            } else {
                separator = new GameObject();
                separator.transform.SetParent(transform);
                separator.name = objName;
                obj.transform.SetParent(separator.transform);
            }
        } else {
            objectMap.Add(objName, new Queue<GameObject>());
            GameObject separator = new GameObject();
            separator.transform.SetParent(transform);
            separator.name = objName;
            obj.transform.SetParent(separator.transform);
            objectMap[objName].Enqueue(obj);
        }
    }

    /// <summary>
    /// 清空对象池内所有对象
    /// </summary>
    public void Clear() {
        foreach (var item in objectMap) {
            foreach (var listitem in item.Value) {
                Destroy(listitem.gameObject);
            }
        }
        objectMap.Clear();
    }

    /// <summary>
    /// 对象池被销毁时销毁一切对象
    /// </summary>
    void OnDestroy() {
        Clear();
    }
}
