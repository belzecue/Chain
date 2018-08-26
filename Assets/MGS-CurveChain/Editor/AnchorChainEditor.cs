/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AnchorChainEditor.cs
 *  Description  :  Editor for AnchorChain.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  8/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.CurveChain
{
    [CustomEditor(typeof(AnchorChain), true)]
    [CanEditMultipleObjects]
    public class AnchorChainEditor : CurveChainEditor
    {
        #region Field and Property
        protected new AnchorChain Target { get { return target as AnchorChain; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            Handles.color = Blue;
            var scaleDelta = Mathf.Max(Delta, Delta * GetHandleSize(Target.transform.position));
            for (float t = 0; t < Target.MaxKey; t += scaleDelta)
            {
                Handles.DrawLine(Target.GetPointAt(t), Target.GetPointAt(Mathf.Min(Target.MaxKey, t + scaleDelta)));
            }

            if (Application.isPlaying)
                return;

            for (int i = 0; i < Target.AnchorsCount; i++)
            {
                var anchorItem = Target.GetAnchorAt(i);
                if (Event.current.alt)
                {
                    Handles.color = Color.green;
                    DrawAdaptiveButton(anchorItem, Quaternion.identity, NodeSize, NodeSize, SphereCap, () =>
                    {
                        var offset = Vector3.zero;
                        if (i > 0)
                            offset = (anchorItem - Target.GetAnchorAt(i - 1)).normalized * GetHandleSize(anchorItem);
                        else
                            offset = Vector3.forward * GetHandleSize(anchorItem);

                        Target.InsertAnchor(i + 1, anchorItem + offset);
                        Target.Rebuild();
                    });
                }
                else if (Event.current.shift)
                {
                    Handles.color = Color.red;
                    DrawAdaptiveButton(anchorItem, Quaternion.identity, NodeSize, NodeSize, SphereCap, () =>
                    {
                        if (Target.AnchorsCount > 1)
                        {
                            Target.RemoveAnchorAt(i);
                            Target.Rebuild();
                        }
                    });
                }
                else
                {
                    Handles.color = Blue;
                    DrawFreeMoveHandle(anchorItem, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
                    {
                        Target.SetAnchorAt(i, position);
                        Target.Rebuild();
                    });
                }
            }
        }
        #endregion
    }
}