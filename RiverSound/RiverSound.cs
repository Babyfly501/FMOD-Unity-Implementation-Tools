
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;
// ============== INSTRUCTION ==============
// Create empty game object and add "Spline Container" component
// Add waypoints and draw line
// Create another empty game object and add this script
// Select "Path" as well as "Player" in the inspector
// Add sound to the object
	public class RiverSound : MonoBehaviour
	{
		[Tooltip("Cinemachine Path to follow")]
		public SplineContainer m_Path;
		[Tooltip("Character to track")]
		public GameObject Player;
		float m_Position; // The normalized position along the path (0 to 1)

		void Update()
		{
			if (m_Path == null || Player == null || m_Path.Splines.Count == 0) return;

			// Convert player position to local space of the path
			Vector3 localPlayerPos = m_Path.transform.InverseTransformPoint(Player.transform.position);

			// Find closest point on the spline
			SplineUtility.GetNearestPoint(m_Path.Splines[0], localPlayerPos, out float3 _, out m_Position);

			SetCartPosition(m_Position);
		}

		// Set cart's position to the normalized unit
		void SetCartPosition(float normalizedUnit)
		{
			m_Position = normalizedUnit;

			// Evaluate position and orientation in local space
			SplineUtility.Evaluate(m_Path.Splines[0], m_Position, out float3 localPos, out float3 tangent, out float3 upVector);

			// Transform to world space
			transform.position = m_Path.transform.TransformPoint(localPos);
			transform.rotation = Quaternion.LookRotation(tangent, upVector);
		}
	}
