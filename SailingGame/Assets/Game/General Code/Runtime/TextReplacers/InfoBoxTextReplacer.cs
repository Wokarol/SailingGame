using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
[ExecuteAlways]
public class InfoBoxTextReplacer : MonoBehaviour
{
    private const string VersionTag = "{VERSION}";

    [SerializeField, TextArea(3, 8)]
    [NaughtyAttributes.InfoBox(
        "You can use tags to replace text with global values\n" 
        + VersionTag + " = Application.version")]
    private string format = "";

    private TMP_Text box = null;
    
    private TMP_Text GetBox()
    {
        if (!box) {
            box = GetComponent<TMP_Text>();
        }
        return box;
    }

    private void Update()
    {
        var box = GetBox();
        box.text = format
            .Replace(VersionTag, Application.version);
    }
}
