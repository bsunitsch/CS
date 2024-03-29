﻿using SenseHat.Portable.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public static class ConversionHelper
    {
        public static Vector3D<float> GetVector3D(object value)
        {
            var vector = value as Vector3D<float>;

            if (vector == null)
            {
                ThrowException();
            }

            return vector;
        }

        public static byte GetByteValueFromBitArray(BitArray bitArray)
        {
            Check.IsNull(bitArray);
            Check.IsLengthEqualTo(bitArray.Length, Constants.ByteBitLength);

            // Perform actual conversion
            var buffer = new byte[1];

            ((ICollection)bitArray).CopyTo(buffer, 0);

            return buffer[0];
        }

        public static void SetBitArrayValues(BitArray bitArray, bool[] values, int beginIndex, int endIndex)
        {
            Check.IsNull(bitArray);
            Check.IsNull(values);

            Check.IsPositive(beginIndex);
            Check.LengthNotLessThan(bitArray.Length, endIndex);

            for (int i = beginIndex, j = 0; i <= endIndex; i++, j++)
            {
                bitArray[i] = values[j];
            }
        }

        private static void ThrowException()
        {
            throw new Exception("Conversion error");
        }
    }
}
