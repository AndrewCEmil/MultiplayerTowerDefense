using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Teleport : MonoBehaviour, ICardboardGazeResponder {
  private Vector3 startingPosition;

  void Start() {
    startingPosition = transform.localPosition;
    SetGazedAt(false);
  }

  void LateUpdate() {
    Cardboard.SDK.UpdateState();
    if (Cardboard.SDK.BackButtonPressed) {
      Application.Quit();
    }
  }

  public void SetGazedAt(bool gazedAt) {
    GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
  }

  public void Reset() {
    transform.localPosition = startingPosition;
  }

  public void ToggleVRMode() {
    Cardboard.SDK.VRModeEnabled = !Cardboard.SDK.VRModeEnabled;
  }

  public void ToggleDistortionCorrection() {
    switch(Cardboard.SDK.DistortionCorrection) {
    case Cardboard.DistortionCorrectionMethod.Unity:
      Cardboard.SDK.DistortionCorrection = Cardboard.DistortionCorrectionMethod.Native;
      break;
    case Cardboard.DistortionCorrectionMethod.Native:
      Cardboard.SDK.DistortionCorrection = Cardboard.DistortionCorrectionMethod.None;
      break;
    case Cardboard.DistortionCorrectionMethod.None:
    default:
      Cardboard.SDK.DistortionCorrection = Cardboard.DistortionCorrectionMethod.Unity;
      break;
    }
  }

  public void ToggleDirectRender() {
    Cardboard.Controller.directRender = !Cardboard.Controller.directRender;
  }

  public void TeleportRandomly() {
    Vector3 direction = Random.onUnitSphere;
    direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
    float distance = 2 * Random.value + 1.5f;
    transform.localPosition = direction * distance;
  }

  #region ICardboardGazeResponder implementation

  /// Called when the user is looking on a GameObject with this script,
  /// as long as it is set to an appropriate layer (see CardboardGaze).
  public void OnGazeEnter() {
    SetGazedAt(true);
  }

  /// Called when the user stops looking on the GameObject, after OnGazeEnter
  /// was already called.
  public void OnGazeExit() {
    SetGazedAt(false);
  }

  // Called when the Cardboard trigger is used, between OnGazeEnter
  /// and OnGazeExit.
  public void OnGazeTrigger() {
    TeleportRandomly();
  }

  #endregion
}
