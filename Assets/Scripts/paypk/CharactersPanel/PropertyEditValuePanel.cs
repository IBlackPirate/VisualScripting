﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PropertyEditValuePanel : MonoBehaviour
{
    public int Index { get; set; }
    public string Name { get; set; }

    public void OnEndEdit(string value)
    {
        DataManager.instance.PropertyEditPanel.GetComponent<PropertyEditPanel>().OnPropertyEdit(Index, value);
    }

}


