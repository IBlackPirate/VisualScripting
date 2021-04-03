using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EffectsPanel : MonoBehaviour
{
    public int ID = -1;

    public GameObject Panel;
    public Transform Parent;

    public Button AddButton;
    public Button SaveButton;

    public List<GameObject> panels = new List<GameObject>();
    public List<string> Names = new List<string>();

    public void AddPanel(string name)
    {
        var panel = Instantiate(Panel, Parent);
        panels.Add(panel);
        panel.GetComponent<EffectsCharacterPanel>().SetName(name);
        Names.Add(name);

        SetDynamicSize();
    }

    private void SetDynamicSize()
    {
        var transformParent = Parent.GetComponent<RectTransform>();
        var cellSize = Parent.GetComponent<GridLayoutGroup>().cellSize.y;
        int count = panels.Count;
        var countInTheRow = 3;
        var r = (count / countInTheRow + count % countInTheRow - 3) * (cellSize + 40) + 25;
        transformParent.sizeDelta = new Vector2(transformParent.sizeDelta.x, Math.Max(349, r));
        transformParent.anchoredPosition = new Vector2(0, -transformParent.sizeDelta.y / 2);
    }

    public void OpenExist(string name, List<PropertyValue> propertyValues)
    {
        var panel = Instantiate(Panel, Parent);
        panels.Add(panel);
        panel.GetComponent<EffectsCharacterPanel>().SetName(name);
        panel.GetComponent<EffectsCharacterPanel>().OpenExistCharacter(name, propertyValues);
        Names.Add(name);

        SetDynamicSize();
    }

    public List<string> GetMyProperties(string parentName, string name)
    {
        return panels.Select(x => x.GetComponent<EffectsCharacterPanel>()).First(x => x.Name == parentName).GetMyProperties(name);
    }

    public void OnChangePropertyType(string parentName, string name)
    {
        panels.Select(x => x.GetComponent<EffectsCharacterPanel>()).First(x => x.Name == parentName).OnChangePropertyType(name);
    }

    public void ClearAll()
    {
        foreach (var p in panels)
        {
            Destroy(p);
        }
        panels = new List<GameObject>();
    }

    public void Delete(string name)
    {
        Names.Remove(name);
        var p = panels.First(x => x.GetComponent<EffectsCharacterPanel>().Name == name);
        panels.Remove(p);
        Destroy(p);
    }

    public void DeleteProperty(string parentName, string name)
    {
        panels.Select(x => x.GetComponent<EffectsCharacterPanel>()).First(x => x.Name == parentName).Delete(name);
    }
}
