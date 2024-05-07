using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetResolution : MonoBehaviour
{
    FullScreenMode screenMode;
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private Toggle fullScreenBtn;

    [Header("해상도를 저장할 콜렉션")]
    List<Resolution> resolutions = new List<Resolution>();
}
