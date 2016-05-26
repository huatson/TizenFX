﻿// Copyright 2016 by Samsung Electronics, Inc.,
//
// This software is the confidential and proprietary information
// of Samsung Electronics, Inc. ("Confidential Information"). You
// shall not disclose such Confidential Information and shall use
// it only in accordance with the terms of the license agreement
// you entered into with Samsung.

using System;

namespace Tizen.System.Sensor
{
    /// <summary>
    /// GravitySensor changed event arguments. Class for storing the data returned by gravity sensor
    /// </summary>
    public class GravitySensorDataUpdatedEventArgs : EventArgs
    {
        internal GravitySensorDataUpdatedEventArgs(float[] values)
        {
            X = values[0];
            Y = values[1];
            Z = values[2];
        }

        /// <summary>
        /// Gets the X component of the gravity.
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Gets the Y component of the gravity.
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// Gets the Z component of the gravity.
        /// </summary>
        public float Z { get; private set; }
    }
}