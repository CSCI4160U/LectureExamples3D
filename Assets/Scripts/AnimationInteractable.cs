using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInteractable : ToggleInteractable {
    [SerializeField] private Animator[] animatorsOnActivate;
    [SerializeField] private Animator[] animatorsOnDeactivate;

    [SerializeField] private string[] toSetBooleanOnActivate;
    [SerializeField] private string[] toSetBooleanOnDeactivate;

    [SerializeField] private bool[] booleanValuesOnActivate;
    [SerializeField] private bool[] booleanValuesOnDeactivate;

    public override void OnActivate() {
        for (int i = 0; i < animatorsOnActivate.Length; i++) {
            animatorsOnActivate[i].SetBool(toSetBooleanOnActivate[i], booleanValuesOnActivate[i]);
        }
    }

    public override void OnDeactivate() {
        for (int i = 0; i < animatorsOnDeactivate.Length; i++) {
            animatorsOnDeactivate[i].SetBool(toSetBooleanOnDeactivate[i], booleanValuesOnDeactivate[i]);
        }
    }
}
