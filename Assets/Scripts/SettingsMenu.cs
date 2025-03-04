using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions = new List<Resolution>();

    void Start()
    {
        dropdown.ClearOptions();
        resolutions = Screen.resolutions; // Obtiene todas las resoluciones disponibles

        // Filtrar resoluciones únicas (sin duplicados)
        HashSet<string> uniqueResolutions = new HashSet<string>();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resText = resolutions[i].width + "x" + resolutions[i].height;
            if (!uniqueResolutions.Contains(resText)) // Evita resoluciones repetidas
            {
                uniqueResolutions.Add(resText);
                filteredResolutions.Add(resolutions[i]);
                options.Add(resText);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = options.Count - 1;
                }
            }
        }

        if (dropdown == null)
        {
            print("vacio");

        }
        dropdown.AddOptions(options);
        dropdown.value = currentResolutionIndex;
        dropdown.RefreshShownValue();

        dropdown.onValueChanged.AddListener(ChangeResolution);
    }

    void ChangeResolution(int index)
    {
        Resolution selectedResolution = filteredResolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}
