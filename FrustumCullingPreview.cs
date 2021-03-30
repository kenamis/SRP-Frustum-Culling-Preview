#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

/// <summary>
/// Created by Kenamis of Rainy Reign Games
/// </summary>

namespace RainyReignGames.Utilities
{
    /// <summary>
    /// Place this script on the camera that you want its culling matrix to get applied to the scene view camera.
    /// </summary>
    [ExecuteInEditMode, RequireComponent(typeof(Camera))]
    public class FrustumCullingPreview : MonoBehaviour
    {
        Camera cullingCamera;
        ScriptableCullingParameters cameraCullingParameters;

        void OnEnable()
        {
            cullingCamera = GetComponent<Camera>();
            RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
            RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
        }

        private void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
            RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
        }

        private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            if (ReferenceEquals(camera, SceneView.lastActiveSceneView.camera))
            {
                camera.cullingMatrix = cullingCamera.projectionMatrix * cullingCamera.worldToCameraMatrix;
                camera.cullingMask = cullingCamera.cullingMask;
            }
        }

        private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            if (ReferenceEquals(camera, SceneView.lastActiveSceneView.camera))
            {
                camera.ResetCullingMatrix();
            }
        }
    }
}
#endif
