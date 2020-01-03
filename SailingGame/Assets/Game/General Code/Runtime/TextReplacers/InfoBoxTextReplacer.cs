﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class InfoBoxTextReplacer : MonoBehaviour
{
    private const string VersionTag = "{v}";
    private const string HashTag = "{hash}";

    private const string PatternDescription = "You can use tags to replace text with global values\n"
            + VersionTag + " = Application's version\n"
            + HashTag + " = Hash ID of application";

    [SerializeField, TextArea(3, 8), NaughtyAttributes.InfoBox(PatternDescription)]
    private string pattern = "";

    private TMP_Text box = null;

    private TMP_Text Box {
        get {
            if (!box) {
                box = GetComponent<TMP_Text>();
            }
            return box;
        }
    }

    private void OnValidate()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        var box = Box;
        box.text = Application.isPlaying
            ? InjectApplicationData(pattern)
            : InjectPlaceholders(pattern);
    }

    private string InjectPlaceholders(string p)
    {
        return p
            .Replace(VersionTag, "__.__.__")
            .Replace(HashTag, "b5ff5f2574a503452e32737a4d116aa16b0808f2");
    }

    private string InjectApplicationData(string pattern) => throw new NotImplementedException();
}