using Flexalon;
using System.Linq;
using UnityEngine;

public class CurveRenderer : MonoBehaviour
{
    private FlexalonCurveLayout _curve;
    private LineRenderer _lineRenderer;

    void Awake()
    {
        _curve = GetComponent<FlexalonCurveLayout>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        _lineRenderer.positionCount = _curve.CurvePositions.Count;
        _lineRenderer.SetPositions(_curve.CurvePositions.Select(p => transform.rotation * p + transform.position).ToArray());
    }
}