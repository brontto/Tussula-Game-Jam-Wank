using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTargetGroup;

public class TargetGroupHandler : MonoBehaviour {

    CinemachineTargetGroup group;
    public List<GameObject> list;

    private void Awake()
    {
        group = GetComponent<CinemachineTargetGroup>();
    }

    void Start()
    {

        ChangeTargetGroup();
    }


    public void AddToList(GameObject player)
    {
        list.Add(player);
        ChangeTargetGroup();
    }

    public void ChangeTargetGroup()
    {
        Target[] newGroup = new Target[list.Count];

        for (int i = 0; i < list.Count; i++)
        {
            Target target = new Target();
            target.target = list[i].transform;
            target.radius = 0f;
            target.weight = 1f;
            newGroup[i] = target;
        }

        group.m_Targets = newGroup;
    }
}