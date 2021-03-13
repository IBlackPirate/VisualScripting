using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BranchDescription : MonoBehaviour
{
    public int ID = -1;
    public Dropdown TypeDropdown;
    public InputField Text;
    [HideInInspector] public Button Button;

    public void Save()
    {

        if (ID == -1)
        {
            NodeData.instance.GetMyNodeData(ref ID, transform);
        }
        if (Text.text.Length > 0)
            QuestButtonColors.ApplyColorsToButton(true, Button);
        else
            QuestButtonColors.ApplyColorsToButton(false, Button);

        BranchManager.SaveBranch(ID, (BranchType)TypeDropdown.value, Text.text);

        gameObject.transform.parent.GetComponentsInChildren<NodeDescriptionCharactersPanel>(true).First(x => x.name == "NodeDescription-Panel").gameObject.SetActive(true);
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}