using UnityEngine;
using System.Collections;

public class ToggleInteractable : InteractableObject {
    [SerializeField] protected string deactivateText = "Right-click to deactivate";
    [SerializeField] protected string lockedText = "Cannot open: locked";

    [SerializeField] protected bool isActive = false;
    [SerializeField] protected bool isLocked = false;

    [SerializeField] protected bool autoDeactivateAfterDelay = false;
    [SerializeField] protected float autoDeactivateDelay = 8f;

    [SerializeField] protected AudioClip activateAudioClip;
    [SerializeField] protected AudioClip deactivateAudioClip;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Lock() {
        isLocked = true;
    }

    public void Unlock() {
        isLocked = false;
    }

    public override string GetInteractionText() {
        if (isLocked) {
            return lockedText;
        } else if (isActive) {
            return deactivateText;
        } else {
            return activateText;
        }
    }

    public override void Interact() {
        if (isLocked) {
            // TODO: Play a door handle jiggle sound effect

            return;
        }

        isActive = !isActive;

        if (isActive) {
            this.OnActivate();
            if (audioSource != null && activateAudioClip != null) {
                audioSource.PlayOneShot(activateAudioClip);
            }

            if (autoDeactivateAfterDelay) {
                StartCoroutine(DeactivateAfterDelay(autoDeactivateDelay));
            }
        } else {
            this.OnDeactivate();
            if (audioSource != null && deactivateAudioClip != null) {
                audioSource.PlayOneShot(deactivateAudioClip);
            }
        }
    }

    public virtual void OnActivate() {

    }

    public virtual void OnDeactivate() {

    }

    public IEnumerator DeactivateAfterDelay(float delay) {
        float timePassed = 0f;

        do {
            timePassed += Time.deltaTime;

            yield return null;
        } while (timePassed < delay);

        isActive = false;
        this.OnDeactivate();

        yield return null;
    }
}
