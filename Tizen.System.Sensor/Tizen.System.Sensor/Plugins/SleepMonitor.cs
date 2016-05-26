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
    /// SleepMonitor Class. Used for registering callbacks for sleep monitor and getting sleep data
    /// /// </summary>
    public class SleepMonitor : Sensor
    {
        /// <summary>
        /// Gets the value of the sleep state.
        /// </summary>
        public SleepMonitorState SleepState { get; private set; }

        /// <summary>
        /// Returns true or false based on whether sleep monitor is supported by device.
        /// </summary>
        public static bool IsSupported
        {
            get
            {
                Log.Info(Globals.LogTag, "Checking if the SleepMonitor is supported");
                return CheckIfSupported();
            }
        }

        /// <summary>
        /// Returns the number of sleep monitors available on the device.
        /// </summary>
        public static int Count
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the count of sleep monitors");
                return GetCount();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tizen.System.Sensor.SleepMonitor"/> class.
        /// </summary>
        /// <param name='index'>
        /// Index. Default value for this is 0. Index refers to a particular sleep monitor in case of multiple sensors
        /// </param>
        public SleepMonitor(int index = 0) : base(index)
        {
            Log.Info(Globals.LogTag, "Creating SleepMonitor object");
        }

        internal override SensorType GetSensorType()
        {
            return SensorType.HumanSleepMonitor;
        }

        /// <summary>
        /// Event Handler for storing the callback functions for event corresponding to change in sleep monitor data.
        /// </summary>

        public event EventHandler<SleepMonitorDataUpdatedEventArgs> DataUpdated;

        private static bool CheckIfSupported()
        {
            bool isSupported;
            int error = Interop.SensorManager.SensorIsSupported(SensorType.HumanSleepMonitor, out isSupported);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error checking if sleep monitor is supported");
                isSupported = false;
            }
            return isSupported;
        }

        private static int GetCount()
        {
            IntPtr list;
            int count;
            int error = Interop.SensorManager.GetSensorList(SensorType.HumanSleepMonitor, out list, out count);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor list for sleep");
                count = 0;
            }
            else
                Interop.Libc.Free(list);
            return count;
        }

        protected override void EventListenStart()
        {
            int error = Interop.SensorListener.SetEventCallback(ListenerHandle, Interval, SensorEventCallback, IntPtr.Zero);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error setting event callback for sleep monitor");
                throw SensorErrorFactory.CheckAndThrowException(error, "Unable to set event callback for sleep");
            }
        }

        protected override void EventListenStop()
        {
            int error = Interop.SensorListener.UnsetEventCallback(ListenerHandle);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error unsetting event callback for sleep monitor");
                throw SensorErrorFactory.CheckAndThrowException(error, "Unable to unset event callback for sleep");
            }
        }

        private void SensorEventCallback(IntPtr sensorHandle, IntPtr sensorPtr, IntPtr data)
        {
            Interop.SensorEventStruct sensorData = Interop.IntPtrToEventStruct(sensorPtr);
            TimeSpan = new TimeSpan((Int64)sensorData.timestamp);
            SleepState = (SleepMonitorState)sensorData.values[0];

            DataUpdated?.Invoke(this, new SleepMonitorDataUpdatedEventArgs((int)sensorData.values[0]));
        }
    }
}
