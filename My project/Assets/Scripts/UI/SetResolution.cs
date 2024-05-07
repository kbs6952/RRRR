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

    [Header("�ػ󵵸� ������ �ݷ���")]
    List<Resolution> resolutions = new List<Resolution>();
}
