﻿using System;

namespace NHM.DeviceDetection.NVIDIA
{
    // format 372.54;
    internal struct NvidiaSmiDriver : IComparable<NvidiaSmiDriver>
    {
        public static readonly Version MinimumVersion = new Version(461, 33);

        public bool IsCorrectVersion;
        public int LeftPart { get; }

        private readonly int _rightPart;
        public int RightPart
        {
            get
            {
                if (_rightPart >= 10)
                {
                    return _rightPart;
                }

                return _rightPart * 10;
            }
        }

        public NvidiaSmiDriver(int left, int right)
        {
            LeftPart = left;
            _rightPart = right;
            //IsCorrectVersion = false;
            IsCorrectVersion = false;
            IsVersionGreater();
        }

        private void IsVersionGreater()
        {
            if (LeftPart < MinimumVersion.Major)
            {
                IsCorrectVersion = false;
                return;
            }
            else if (RightPart < MinimumVersion.Minor)
            {
                IsCorrectVersion = false;
                return;
            }
            this.IsCorrectVersion = true;
        }

        public override string ToString()
        {
            return $"{LeftPart}.{RightPart}";
        }

        public bool IsValid()
        {
            return RightPart != -1 && LeftPart != -1;
        }

        public Version ToVersion()
        {
            return new Version(LeftPart, RightPart);
        }

        #region IComparable implementation

        public int CompareTo(NvidiaSmiDriver other)
        {
            var leftPartComparison = LeftPart.CompareTo(other.LeftPart);
            if (leftPartComparison != 0) return leftPartComparison;
            return RightPart.CompareTo(other.RightPart);
        }

        public static bool operator <(NvidiaSmiDriver left, NvidiaSmiDriver right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(NvidiaSmiDriver left, NvidiaSmiDriver right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(NvidiaSmiDriver left, NvidiaSmiDriver right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(NvidiaSmiDriver left, NvidiaSmiDriver right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion
    }
}
