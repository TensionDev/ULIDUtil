// SPDX-License-Identifier: Apache-2.0
//
//   Copyright 2021 TensionDev <TensionDev@outlook.com>
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Reflection;
using System.Text;

namespace TensionDev.ULID
{
    /// <summary>
    /// Represents a Universally Unique Lexicographically Sortable Identifier (ULID).
    /// </summary>
    public sealed class Ulid : IComparable<Ulid>, IEquatable<Ulid>
    {
        private static readonly DateTime s_epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private const string INVALID_FORMAT_STRING = "The format of s is invalid";
        private readonly uint _time_high;
        private readonly ushort _time_low;
        private readonly ushort _random_high;
        private readonly uint _random_mid;
        private readonly uint _random_low;

        /// <summary>
        /// A read-only instance of the Ulid object whose value is all ones.
        /// </summary>
        public static readonly Ulid Max = new Ulid(new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff });

        /// <summary>
        /// Initializes a new instance of the Ulid object whose value is all zeros.
        /// </summary>
        public Ulid()
        {
            _time_high = 0;
            _time_low = 0;
            _random_high = 0;
            _random_mid = 0;
            _random_low = 0;
        }

        /// <summary>
        /// Initializes a new instance of the Ulid object by using the specified timestamp.
        /// </summary>
        /// <param name="timestamp">Given Timestamp.</param>
        public Ulid(DateTime timestamp)
        {
            TimeSpan timeSince = timestamp.ToUniversalTime() - s_epoch.ToUniversalTime();
            Int64 timeInterval = ((Int64)timeSince.TotalMilliseconds);

            Byte[] time = BitConverter.GetBytes(timeInterval);

            byte[] time_high = new byte[4];
            byte[] time_low = new byte[2];
            byte[] random_high = new byte[2];
            byte[] random_mid = new byte[4];
            byte[] random_low = new byte[4];

            time_high[0] = time[0];
            time_high[1] = time[1];
            time_high[2] = time[2];
            time_high[3] = time[3];

            time_low[0] = time[4];
            time_low[1] = time[5];

            if (BitConverter.IsLittleEndian)
            {
                time_high[0] = time[2];
                time_high[1] = time[3];
                time_high[2] = time[4];
                time_high[3] = time[5];

                time_low[0] = time[0];
                time_low[1] = time[1];
            }

            using (System.Security.Cryptography.RNGCryptoServiceProvider cryptoServiceProvider = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(random_high);
                cryptoServiceProvider.GetBytes(random_mid);
                cryptoServiceProvider.GetBytes(random_low);
            }

            _time_high = BitConverter.ToUInt32(time_high, 0);
            _time_low = BitConverter.ToUInt16(time_low, 0);
            _random_high = BitConverter.ToUInt16(random_high, 0);
            _random_mid = BitConverter.ToUInt32(random_mid, 0);
            _random_low = BitConverter.ToUInt32(random_low, 0);
        }

        /// <summary>
        /// Initializes a new instance of the Ulid object by using the specified array of bytes.
        /// </summary>
        /// <param name="b">A 16-element byte array containing values with which to initialize the Ulid.</param>
        /// <exception cref="System.ArgumentNullException">b is null.</exception>
        /// <exception cref="System.ArgumentException">b is not 16 bytes long.</exception>
        public Ulid(byte[] b) : this()
        {
            if (b == null)
                throw new ArgumentNullException(nameof(b));

            if (b.Length != 16)
                throw new ArgumentException("b is not 16 bytes long.", nameof(b));

            byte[] time_high = new byte[4];
            byte[] time_low = new byte[2];
            byte[] random_high = new byte[2];
            byte[] random_mid = new byte[4];
            byte[] random_low = new byte[4];

            Array.Copy(b, 0, time_high, 0, 4);
            Array.Copy(b, 4, time_low, 0, 2);
            Array.Copy(b, 6, random_high, 0, 2);
            Array.Copy(b, 8, random_mid, 0, 4);
            Array.Copy(b, 12, random_low, 0, 4);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(time_high);
                Array.Reverse(time_low);
                Array.Reverse(random_high);
                Array.Reverse(random_mid);
                Array.Reverse(random_low);
            }

            _time_high = BitConverter.ToUInt32(time_high, 0);
            _time_low = BitConverter.ToUInt16(time_low, 0);
            _random_high = BitConverter.ToUInt16(random_high, 0);
            _random_mid = BitConverter.ToUInt32(random_mid, 0);
            _random_low = BitConverter.ToUInt32(random_low, 0);
        }

        /// <summary>
        /// Initializes a new instance of the Ulid object by using the value represented by the specified string.
        /// </summary>
        /// <param name="s">A string that contains a Ulid</param>
        /// <exception cref="System.ArgumentNullException">s is null</exception>
        /// <exception cref="System.FormatException">The format of s is invalid</exception>
        public Ulid(string s) : this()
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            if (String.IsNullOrEmpty(s))
                throw new FormatException(INVALID_FORMAT_STRING);

            if (s.Length != 26)
                throw new FormatException(INVALID_FORMAT_STRING);

            string vs = s.ToUpper().Replace("O", "0").Replace("I", "1").Replace("L", "1");

            if (vs.Contains("U"))
                throw new FormatException(INVALID_FORMAT_STRING);

            const Int32 baseLength = 5;

            UInt64 bitStream = 0;
            UInt32 length = 0;

            Byte[] b = new Byte[16];

            Char c = vs[0];
            UInt64 value = FromCrockfordBase32(c);
            bitStream <<= baseLength;
            bitStream += value;

            c = vs[1];
            value = FromCrockfordBase32(c);
            bitStream <<= baseLength;
            bitStream += value;
            Byte[] bytes = BitConverter.GetBytes((long)(bitStream));
            b[0] = bytes[0];
            bitStream = 0;

            for (Int32 i = 2; i < vs.Length; ++i)
            {
                c = vs[i];
                value = FromCrockfordBase32(c);

                bitStream <<= baseLength;
                bitStream += value;

                length += baseLength;
                if (length == 40)
                {
                    int index = (i / 8) * 5 - 4;

                    bytes = BitConverter.GetBytes((long)bitStream);

                    if (index < 12)
                    {
                        b[index + 0] = bytes[4];
                        b[index + 1] = bytes[3];
                        b[index + 2] = bytes[2];
                        b[index + 3] = bytes[1];
                        b[index + 4] = bytes[0];
                    }
                    else
                    {
                        b[index + 0] = bytes[4];
                        b[index + 1] = bytes[3];
                        b[index + 2] = bytes[2];
                        b[index + 3] = bytes[1];
                    }

                    length = 0;
                    bitStream = 0;
                }
            }

            byte[] time_high = new byte[4];
            byte[] time_low = new byte[2];
            byte[] random_high = new byte[2];
            byte[] random_mid = new byte[4];
            byte[] random_low = new byte[4];

            Array.Copy(b, 0, time_high, 0, 4);
            Array.Copy(b, 4, time_low, 0, 2);
            Array.Copy(b, 6, random_high, 0, 2);
            Array.Copy(b, 8, random_mid, 0, 4);
            Array.Copy(b, 12, random_low, 0, 4);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(time_high);
                Array.Reverse(time_low);
                Array.Reverse(random_high);
                Array.Reverse(random_mid);
                Array.Reverse(random_low);
            }

            _time_high = BitConverter.ToUInt32(time_high, 0);
            _time_low = BitConverter.ToUInt16(time_low, 0);
            _random_high = BitConverter.ToUInt16(random_high, 0);
            _random_mid = BitConverter.ToUInt32(random_mid, 0);
            _random_low = BitConverter.ToUInt32(random_low, 0);
        }

        /// <summary>
        /// Initializes a new instance of the Ulid object.
        /// </summary>
        /// <returns>A new Ulid object.</returns>
        public static Ulid NewUlid()
        {
            return new Ulid(DateTime.UtcNow);
        }

        /// <summary>
        /// Initializes a new instance of the Ulid object.
        /// </summary>
        /// <param name="input">A universally unique lexicographically sortable identifier in the proper format.</param>
        /// <returns>A new Ulid object.</returns>
        public static Ulid Parse(string input)
        {
            return new Ulid(input);
        }

        /// <summary>
        /// Converts the string representation of a Ulid to the equivalent Ulid object.
        /// </summary>
        /// <param name="input">The Ulid to convert.</param>
        /// <param name="result">The object that will contain the parsed value. If the method returns true, result contains a valid Ulid.
        /// If the method returns false, result equals Ulid.Empty.</param>
        /// <returns>true if the parse operation was successful; otherwise, false.</returns>
        public static bool TryParse(string input, out Ulid result)
        {
            bool vs = false;
            result = new Ulid();

            try
            {
                result = Parse(input);
                vs = true;
            }
            catch (Exception)
            {
                // Quietly suppress exception on Parse.
            }

            return vs;
        }

        /// <summary>
        /// Compares the current instance with another object and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(object other)
        {
            if (other is Ulid u)
            {
                return CompareTo(u);
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An ULID to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(Ulid other)
        {
            if (other is null)
                return 1;

            return ToString().CompareTo(other.ToString());
        }

        /// <summary>
        /// Returns a value indicating whether this instance and a specified Ulid object represent the same value.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>true if other is equal to this instance; otherwise, false.</returns>
        public bool Equals(Ulid other)
        {
            if (other is null)
                return false;

            int result = CompareTo(other);
            return result == 0;
        }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>true if o is a Ulid that has the same value as this instance; otherwise,false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Ulid other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return (_time_high, _time_low, _random_high, _random_mid, _random_low).GetHashCode();
        }

        /// <summary>
        /// Returns a 16-element byte array that contains the value of this instance.
        /// </summary>
        /// <returns>A 16-element byte array.</returns>
        public byte[] ToByteArray()
        {
            Byte[] vs = new Byte[16];

            byte[] time_high = BitConverter.GetBytes(_time_high);
            byte[] time_low = BitConverter.GetBytes(_time_low);
            byte[] random_high = BitConverter.GetBytes(_random_high);
            byte[] random_mid = BitConverter.GetBytes(_random_mid);
            byte[] random_low = BitConverter.GetBytes(_random_low);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(time_high);
                Array.Reverse(time_low);
                Array.Reverse(random_high);
                Array.Reverse(random_mid);
                Array.Reverse(random_low);
            }

            time_high.CopyTo(vs, 0);
            time_low.CopyTo(vs, 4);
            random_high.CopyTo(vs, 6);
            random_mid.CopyTo(vs, 8);
            random_low.CopyTo(vs, 12);

            return vs;
        }

        /// <summary>
        /// Returns a string representation of the value of this instance as per ulid/spec.
        /// </summary>
        /// <returns>The value of this Ulid.
        /// An example of a return value is "01ARZ3NDEKTSV4RRFFQ69G5FAV"</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            ulong vs = _time_high;
            var remainder = vs % 16;
            vs >>= 4;
            for (int i = 0; i < 6; ++i)
            {
                var value = vs % 32;
                vs >>= 5;

                Char c = ToCrockfordBase32(value);

                sb.Insert(0, c);
            }

            vs = remainder;
            vs <<= 16;
            vs += _time_low;
            vs <<= 16;
            vs += _random_high;
            remainder = vs % 2;
            vs >>= 1;
            for (int i = 0; i < 7; ++i)
            {
                var value = vs % 32;
                vs >>= 5;

                Char c = ToCrockfordBase32(value);

                sb.Insert(6, c);
            }

            vs = remainder;
            vs <<= 32;
            vs += _random_mid;
            remainder = vs % 8;
            vs >>= 3;
            for (int i = 0; i < 6; ++i)
            {
                var value = vs % 32;
                vs >>= 5;

                Char c = ToCrockfordBase32(value);

                sb.Insert(13, c);
            }

            vs = remainder;
            vs <<= 32;
            vs += _random_low;
            for (int i = 0; i < 7; ++i)
            {
                var value = vs % 32;
                vs >>= 5;

                Char c = ToCrockfordBase32(value);

                sb.Insert(19, c);
            }

            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// Returns the System.Guid equivalent of this instance.
        /// </summary>
        /// <returns>A System.Guid object.</returns>
        public Guid ToGuid()
        {
            // System.Guid is Mixed Endian
            var array = ToByteArray();
            Array.Reverse(array, 0, 4);
            Array.Reverse(array, 4, 2);
            Array.Reverse(array, 6, 2);
            return new Guid(array);
        }

        /// <summary>
        /// Returns the approximate DateTime used to generate the Ulid.
        /// </summary>
        /// <returns>DateTime of the Ulid in UTC.</returns>
        public DateTime ToDateTime()
        {
            Byte[] time = new Byte[8];

            byte[] time_high = BitConverter.GetBytes(_time_high);
            byte[] time_low = BitConverter.GetBytes(_time_low);

            time_high.CopyTo(time, 2);
            time_low.CopyTo(time, 6);

            if (BitConverter.IsLittleEndian)
            {
                time_high.CopyTo(time, 2);
                time_low.CopyTo(time, 0);
                time[6] = 0;
                time[7] = 0;
            }

            Int64 timeInterval = BitConverter.ToInt64(time, 0);
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(timeInterval);

            return s_epoch.ToUniversalTime() + timeSpan;
        }

        /// <summary>
        /// Indicates whether the values of two specified Ulid objects are equal.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>true if a and b are equal; otherwise, false.</returns>
        public static bool operator ==(Ulid a, Ulid b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Indicates whether the values of two specified Ulid objects are not equal.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>true if a and b are not equal; otherwise, false.</returns>
        public static bool operator !=(Ulid a, Ulid b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Indicates whether the values of the first Ulid object is strictly less than the other.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>true if a is strictly less than b; otherwise, false.</returns>
        public static bool operator <(Ulid a, Ulid b)
        {
            return a.CompareTo(b) < 0;
        }

        /// <summary>
        /// Indicates whether the values of the first Ulid object is strictly greater than the other.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>true if a is strictly greater than b; otherwise, false.</returns>
        public static bool operator >(Ulid a, Ulid b)
        {
            return a.CompareTo(b) > 0;
        }

        /// <summary>
        /// Indicates whether the values of the first Ulid object is less than or equal to the other.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>true if a is less than or equal to b; otherwise, false.</returns>
        public static bool operator <=(Ulid a, Ulid b)
        {
            return a.CompareTo(b) <= 0;
        }

        /// <summary>
        /// Indicates whether the values of the first Ulid object is greater than or equal to the other.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>true if a is greater than or equal to b; otherwise, false.</returns>
        public static bool operator >=(Ulid a, Ulid b)
        {
            return a.CompareTo(b) >= 0;
        }

        /// <summary>
        /// Converts given numerical value to the corresponding Crockford Base32 Digit.
        /// </summary>
        /// <param name="value">Value range from 0 - 31.</param>
        /// <returns>The corresponding Crockford Base32 Digit.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Char ToCrockfordBase32(ulong value)
        {
            return ToCrockfordBase32((uint)(value));
        }

        /// <summary>
        /// Converts given numerical value to the corresponding Crockford Base32 Digit.
        /// </summary>
        /// <param name="value">Value range from 0 - 31.</param>
        /// <returns>The corresponding Crockford Base32 Digit.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Char ToCrockfordBase32(uint value)
        {
            return ToCrockfordBase32((ushort)(value));
        }

        /// <summary>
        /// Converts given numerical value to the corresponding Crockford Base32 Digit.
        /// </summary>
        /// <param name="value">Value range from 0 - 31.</param>
        /// <returns>The corresponding Crockford Base32 Digit.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Char ToCrockfordBase32(ushort value)
        {
            if (value > 31)
                throw new ArgumentException("Value provided is greater than 31!", nameof(value));

            Char c = (Char)48;
            if (value < 10)
                c += (Char)value;
            else if (value < 18)
                c += (Char)(value + 7);
            else if (value < 20)
                c += (Char)(value + 8);
            else if (value < 22)
                c += (Char)(value + 9);
            else if (value < 27)
                c += (Char)(value + 10);
            else
                c += (Char)(value + 11);

            return c;
        }

        /// <summary>
        /// Converts given Crockford Base32 Digit into its numerical value.
        /// </summary>
        /// <param name="c">Crockford Base32 Digit.</param>
        /// <returns>The corresponding numerical value.</returns>
        /// <exception cref="FormatException"></exception>
        public static ulong FromCrockfordBase32(Char c)
        {
            ulong value;

            if (c >= '0' && c <= '9')
                value = (UInt64)c - 48;
            else if (c >= 'A' && c <= 'H')
                value = (UInt64)c - 55;
            else if (c >= 'J' && c <= 'K')
                value = (UInt64)c - 56;
            else if (c >= 'M' && c <= 'N')
                value = (UInt64)c - 57;
            else if (c >= 'P' && c <= 'T')
                value = (UInt64)c - 58;
            else if (c >= 'V' && c <= 'Z')
                value = (UInt64)c - 59;
            else
                throw new FormatException(INVALID_FORMAT_STRING);

            return value;
        }
    }
}
