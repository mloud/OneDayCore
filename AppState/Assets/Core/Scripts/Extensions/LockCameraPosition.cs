using UnityEngine;
using Cinemachine;
 
/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Y co-ordinate
/// </summary>
[SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
[ExecuteInEditMode]
public class LockCameraPosition : CinemachineExtension
{
    [Tooltip("Lock the camera's X position to this value")]
    public float m_XPosition = 0;

    public bool m_LockXPosition = false;
    
    [Tooltip("Lock the camera's Y position to this value")]
    public float m_YPosition = 0;

    public bool m_LockYPosition = false;
    
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            if (m_LockXPosition)
            {
                pos.x = m_XPosition;
            }

            if (m_LockYPosition)
            {
                pos.y = m_YPosition;
            }

            state.RawPosition = pos;
        }
    }
}