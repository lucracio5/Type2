using UnityEngine;
using Avante;

/// <summary>
/// Renders a World Space Canvas onto the domemaster output RenderTexture
/// after the fulldome warp blit, so UI appears crisp and undistorted on the dome.
/// Attach this to the same GameObject as FulldomeCamera, or assign fields manually.
/// </summary>
[RequireComponent(typeof(FulldomeCamera))]
public class DomeUIRenderer : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The FulldomeCamera component producing the domemaster output.")]
    [SerializeField] private FulldomeCamera fulldomeCam;

    [Tooltip("A dedicated camera that renders only the UI layer onto the dome output.")]
    [SerializeField] private Camera uiCamera;

    [Header("UI Canvas Settings")]
    [Tooltip("The World Space Canvas to render onto the dome.")]
    [SerializeField] private Canvas worldSpaceCanvas;

    private RenderTexture _lastKnownTarget;

    private void Reset()
    {
        fulldomeCam = GetComponent<FulldomeCamera>();
    }

    private void Awake()
    {
        ValidateReferences();
        ConfigureUICamera();
    }

    private void LateUpdate()
    {
        // Reattach if the domemaster RenderTexture reference changes at runtime.
        if (fulldomeCam.domemasterFbo != null && fulldomeCam.domemasterFbo != _lastKnownTarget)
        {
            AttachUICameraToTarget(fulldomeCam.domemasterFbo);
        }
    }

    /// <summary>
    /// Configures the UI camera with the correct settings for dome overlay rendering.
    /// </summary>
    private void ConfigureUICamera()
    {
        if (uiCamera == null)
        {
            Debug.LogError("[DomeUIRenderer] No UI Camera assigned.", this);
            return;
        }

        // Only render the UI layer.
        uiCamera.cullingMask = LayerMask.GetMask("UI");

        // Don't clear — composite on top of the dome blit output.
        uiCamera.clearFlags = CameraClearFlags.Nothing;

        // Orthographic projection for a flat UI overlay on the dome output.
        uiCamera.orthographic = true;

        // The camera depth must be higher than the main camera so it renders on top.
        uiCamera.depth = 10;

        // Disable audio listener to avoid conflicts.
        AudioListener uiAudioListener = uiCamera.GetComponent<AudioListener>();
        if (uiAudioListener != null)
            uiAudioListener.enabled = false;

        if (worldSpaceCanvas != null)
        {
            worldSpaceCanvas.renderMode = RenderMode.WorldSpace;
            worldSpaceCanvas.worldCamera = uiCamera;
        }

        if (fulldomeCam.domemasterFbo != null)
            AttachUICameraToTarget(fulldomeCam.domemasterFbo);
    }

    /// <summary>
    /// Points the UI camera's target texture to the domemaster RenderTexture.
    /// </summary>
    private void AttachUICameraToTarget(RenderTexture target)
    {
        uiCamera.targetTexture = target;
        _lastKnownTarget = target;

        // Fit the orthographic size to the square domemaster texture dimensions.
        float halfSize = target.height * 0.5f;
        uiCamera.orthographicSize = halfSize;
    }

    private void ValidateReferences()
    {
        if (fulldomeCam == null)
            fulldomeCam = GetComponent<FulldomeCamera>();

        if (fulldomeCam == null)
            Debug.LogError("[DomeUIRenderer] FulldomeCamera reference is missing.", this);
    }
}
