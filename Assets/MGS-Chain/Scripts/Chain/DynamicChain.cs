﻿/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DynamicChain.cs
 *  Description  :  Define DynamicChain component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/27/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Machinery;
using UnityEngine;

namespace Mogoson.CurveChain
{
    [AddComponentMenu("Mogoson/CurveChain/DynamicChain")]
    public class DynamicChain : Chain
    {
        #region Public Method
        /// <summary>
        /// Drive chain.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity, DriveType type)
        {
            RebuildCurve(true);

            var maxTime = Curve[Curve.KeyframeCount - 1].key;
            if (Mathf.Abs(timer) >= maxTime)
                timer -= maxTime;

            base.Drive(velocity, type);
        }
        #endregion
    }
}