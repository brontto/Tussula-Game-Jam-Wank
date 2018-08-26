using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public List<GameObject> mounts;
    public List<GameObject> riders;
    public int maxRiders;
    public int maxMounts;

	void OnEnable () {
        mounts = new List<GameObject>();
        riders = new List<GameObject>();
        ReadResources(riders, "Riders");
        ReadResources(mounts, "MountPrefabs");
        maxRiders = riders.Count;
        maxMounts = mounts.Count;
        Debug.Log("Riders read: " + maxRiders);
        Debug.Log("Mounts read: " + maxMounts);

    }

    void ReadResources (List<GameObject> list, string path) {
        Object[] objs = Resources.LoadAll(path, typeof(GameObject));
        foreach (Object obj in objs) {
            list.Add((GameObject)obj);
        }
    }

    public GameObject GetRider(int i) {
        return riders[i];
    }

    public GameObject GetMount(int i) {
        return mounts[i];
    }

    


}
