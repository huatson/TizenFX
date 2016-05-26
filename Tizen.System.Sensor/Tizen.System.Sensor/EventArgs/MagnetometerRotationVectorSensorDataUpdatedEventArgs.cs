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
    /// MagnetometerRotationVectorSensor changed event arguments. Class for storing the data returned by magnetometer rotation vector sensor
    /// </summary>
    public class MagnetometerRotationVectorSensorDataUpdatedEventArgs : EventArgs
    {
        internal MagnetometerRotationVectorSensorDataUpdatedEventArgs(float[] values, SensorDataAccuracy accuracy)
        {
            X = values[0];
            Y = values[1];
            Z = values[2];
            W = values[3];
            Accuracy = accuracy;
        }

        /// <summary>
        /// Gets the X component of the magnetometer rotation vector.
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Gets the Y component of the magnetometer rotation vector.
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// Gets the Z component of the magnetometer rotation vector.
        /// </summary>
        public float Z { get; private set; }

        /// <summary>
        /// Gets the W component of the magnetometer rotation vector.
        /// </summary>
        public float W { get; private set; }

        /// <summary>
        /// Gets the accuracy of the magnetometer rotation vector data.
        /// </summary>
        public SensorDataAccuracy Accuracy { get; private set; }
    }
}