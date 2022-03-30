using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorInteractable : ToggleInteractable {
    [SerializeField] private GameObject[] toEnableOnActivate;
    [SerializeField] private GameObject[] toEnableOnDeactivate;

    public override void OnActivate() {
        // deactivate all of the toEnableOnDeactivate objects
        for (int i = 0; i < toEnableOnDeactivate.Length; i++) {
            toEnableOnDeactivate[i].SetActive(false);
        }

        // activate all of the toEnableOnActivate objects
        for (int i = 0; i < toEnableOnActivate.Length; i++) {
            toEnableOnActivate[i].SetActive(true);
        }
    }

    public override void OnDeactivate() {
        // deactivate all of the toEnableOnActivate objects
        for (int i = 0; i < toEnableOnActivate.Length; i++) {
            toEnableOnActivate[i].SetActive(false);
        }

        // activate all of the toEnableOnDeactivate objects
        for (int i = 0; i < toEnableOnDeactivate.Length; i++) {
            toEnableOnDeactivate[i].SetActive(true);
        }
    }
}
