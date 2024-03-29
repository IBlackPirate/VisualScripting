﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class RootNode : Node
{
    public static RootNode instance;

    public static bool IsBuldSuccess;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if (instance == null)
        {
            instance = this;
            if (Id < 1)
                Id = GenerateID();
            Name = "Root node";
        }
        else Destroy(gameObject);

        Panel.SetActive(false);
    }

    public static void StartBuild()
    {
        IsBuldSuccess = true;
        instance.WorldState = DataManager.instance.Characters.Clone();

        foreach (var node in GraphController.Nodes.Values)
        {
            if (node.gameObject != instance.gameObject)
                node.ClearState();
        }

        instance.StartRecursiveBuild();

        foreach (var node in GraphController.Nodes.Values)
        {
            if (node.build_visitedCount.HasValue && node.build_visitedCount == 0)
            {
                node.OnErrorOccurred();
                node.LogError("Нода недостижима");
            }
        }

        GraphController.SetPlayButton(IsBuldSuccess);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void OnLoadingStart()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = null;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (eventData.clickCount == 2)
        {
            DataManager.instance.BaseCharactersPanel.GetComponent<CharactersPanel>().UpdatePanels();
        }
    }
}
