using System;
using Xunit;

namespace XUnitTestProjectULID
{
    public class UnitTestUlid
    {
        [Fact]
        public void TestUlidMax()
        {
            string expectedULID = "7ZZZZZZZZZZZZZZZZZZZZZZZZZ";

            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Max;
            Assert.Equal(expectedULID, ulid.ToString());
        }

        [Fact]
        public void TestConstructorDateTime()
        {
            string expectedULID = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            DateTime dateTime = new DateTime(2016, 07, 30, 23, 54, 10, 259, DateTimeKind.Utc);

            TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(dateTime);

            // First 10 characters are the timestamp, next 16 are random.
            Assert.Equal(expectedULID.Substring(0, 10), ulid.ToString().Substring(0, 10));
        }

        [Fact]
        public void TestConstructorNullByteArray()
        {
            byte[] vs = null;
            Assert.Throws<ArgumentNullException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestConstructorInvalidSizeByteArray()
        {
            byte[] vs = new byte[17];
            Assert.Throws<ArgumentException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestConstructorByteArray()
        {
            string expectedULID = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            byte[] vs = new byte[] { 0x01, 0x56, 0x3e, 0x3a, 0xb5, 0xd3, 0xd6, 0x76, 0x4c, 0x61, 0xef, 0xb9, 0x93, 0x02, 0xbd, 0x5b };

            TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs);
            Assert.Equal(expectedULID, ulid.ToString());
        }

        [Fact]
        public void TestConstructorNullString()
        {
            string vs = null;
            Assert.Throws<ArgumentNullException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestConstructorEmptyString()
        {
            string vs = string.Empty;
            Assert.Throws<FormatException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestConstructorInvalidSizeString()
        {
            string vs = "0123456789";
            Assert.Throws<FormatException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestConstructorInvalidCharacterString()
        {
            string vs = "01ARZ3NDEKTSU4RRFFQ69G5FAV";
            Assert.Throws<FormatException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestTryParseStringValid()
        {
            string expectedULID = "01ARZ3NDEKTSV4RRFFQ69G5FAV";

            bool result = TensionDev.ULID.Ulid.TryParse(expectedULID, out TensionDev.ULID.Ulid ulid);
            Assert.Equal(expectedULID, ulid.ToString());
            Assert.True(result);
        }

        [Fact]
        public void TestTryParseStringInvalid()
        {
            string expectedULID = "01ARZ3NDEKTSU4RRFFQ69G5FAV";

            bool result = TensionDev.ULID.Ulid.TryParse(expectedULID, out TensionDev.ULID.Ulid _);
            Assert.False(result);
        }

#if NETCOREAPP3_0_OR_GREATER
        [Fact]
        public void TestTryParseSpanValid()
        {
            string expectedULID = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            ReadOnlySpan<char> input = expectedULID.AsSpan();

            bool result = TensionDev.ULID.Ulid.TryParse(input, out TensionDev.ULID.Ulid ulid);
            Assert.Equal(expectedULID, ulid.ToString());
            Assert.True(result);
        }

        [Fact]
        public void TestTryParseSpanInvalid()
        {
            string expectedULID = "01ARZ3NDEKTSU4RRFFQ69G5FAV";
            ReadOnlySpan<char> input = expectedULID.AsSpan();

            bool result = TensionDev.ULID.Ulid.TryParse(input, out TensionDev.ULID.Ulid _);
            Assert.False(result);
        }
#endif

        [Fact]
        public void TestCompareToObject()
        {
            int expectedResult = 1;
            object other = new object();
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);

            int actualResult = ulid.CompareTo(other);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestCompareToObjectUlid()
        {
            int expectedResult = 0;
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);
            object other = TensionDev.ULID.Ulid.Parse(vs);

            int actualResult = ulid.CompareTo(other);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestCompareToTwoInstance()
        {
            int expectedResult = 0;
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs);

            int actualResult = ulid1.CompareTo(ulid2);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestCompareToNull()
        {
            int expectedResult = 1;
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = null;

            int actualResult = ulid1.CompareTo(ulid2);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestEqualsNull()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = null;

            bool actualResult = ulid.Equals(ulid2);
            Assert.False(actualResult);
        }

        [Fact]
        public void TestEqualsObjectUlid()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);
            object other = TensionDev.ULID.Ulid.Parse(vs);

            bool actualResult = ulid.Equals(other);
            Assert.True(actualResult);
        }

        [Fact]
        public void TestEqualsObject()
        {
            object other = new object();
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);

            bool actualResult = ulid.Equals(other);
            Assert.False(actualResult);
        }

        [Fact]
        public void TestEqualsTwoInstance()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs);

            bool actualResult = ulid1.Equals(ulid2);
            Assert.True(actualResult);
        }

        [Fact]
        public void TestGetHashCodeTwoInstance()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs);

            int hashcode1 = ulid1.GetHashCode();
            int hashcode2 = ulid2.GetHashCode();
            Assert.Equal(hashcode1, hashcode2);
        }

        [Fact]
        public void TestToByteArray1()
        {
            byte[] expected = new byte[] { 0x01, 0x56, 0x3e, 0x3a, 0xb5, 0xd3, 0xd6, 0x76, 0x4c, 0x61, 0xef, 0xb9, 0x93, 0x02, 0xbd, 0x5b };
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs);

            byte[] actual = ulid.ToByteArray();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToByteArray2()
        {
            byte[] expected = new byte[] { 0x01, 0x56, 0x3e, 0x3a, 0xb5, 0xd3, 0xd6, 0x76, 0x4c, 0x61, 0xef, 0xb9, 0x93, 0x02, 0xbd, 0x5b };
            TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(expected);

            byte[] actual = ulid.ToByteArray();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToString1()
        {
            string expected = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            byte[] vs = new byte[] { 0x01, 0x56, 0x3e, 0x3a, 0xb5, 0xd3, 0xd6, 0x76, 0x4c, 0x61, 0xef, 0xb9, 0x93, 0x02, 0xbd, 0x5b };
            TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs);

            string actual = ulid.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToString2()
        {
            string expected = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(expected);

            string actual = ulid.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToGuid1()
        {
            Guid expected = new Guid("01563E3A-B5D3-D676-4C61-EFB99302BD5B");
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs);

            Guid actual = ulid.ToGuid();
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public void TestToDateTimeString()
        {
            DateTime expected = new DateTime(2016, 07, 30, 23, 54, 10, 259, DateTimeKind.Utc);

            TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid("01ARZ3NDEKTSV4RRFFQ69G5FAV");
            DateTime actual = ulid.ToDateTime();

            Assert.Equal(expected, actual, TimeSpan.FromMilliseconds(1));
        }

        [Fact]
        public void TestToDateTimeByteArray()
        {
            DateTime expected = new DateTime(2016, 07, 30, 23, 54, 10, 259, DateTimeKind.Utc);

            byte[] vs = new byte[] { 0x01, 0x56, 0x3e, 0x3a, 0xb5, 0xd3, 0xd6, 0x76, 0x4c, 0x61, 0xef, 0xb9, 0x93, 0x02, 0xbd, 0x5b };
            TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs);
            DateTime actual = ulid.ToDateTime();

            Assert.Equal(expected, actual, TimeSpan.FromMilliseconds(1));
        }

        [Fact]
        public void TestOperatorEqualsBothNull()
        {
            TensionDev.ULID.Ulid ulid = null;
            TensionDev.ULID.Ulid ulid2 = null;

            bool actualResult = ulid == ulid2;
            Assert.True(actualResult);
        }

        [Fact]
        public void TestOperatorEqualsNull1()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = null;

            bool actualResult = ulid == ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorEqualsNull2()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = null;
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs);

            bool actualResult = ulid == ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorEqualsTwoInstance()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs);

            bool actualResult = ulid1 == ulid2;
            Assert.True(actualResult);
        }

        [Fact]
        public void TestOperatorNotEqualsBothNull()
        {
            TensionDev.ULID.Ulid ulid = null;
            TensionDev.ULID.Ulid ulid2 = null;

            bool actualResult = ulid != ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorNotEqualsNull1()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = null;

            bool actualResult = ulid != ulid2;
            Assert.True(actualResult);
        }

        [Fact]
        public void TestOperatorNotEqualsNull2()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = null;
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs);

            bool actualResult = ulid != ulid2;
            Assert.True(actualResult);
        }

        [Fact]
        public void TestOperatorNotEqualsTwoInstance()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs);

            bool actualResult = ulid1 != ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorLessThanTwoInstance()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs);

            bool actualResult = ulid1 < ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorLessThanSmallerInstance()
        {
            string vs1 = "01ARZ3NDEKSSV4RRFFQ69G5FAV";
            string vs2 = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs1);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs2);

            bool actualResult = ulid1 < ulid2;
            Assert.True(actualResult);
        }

        [Fact]
        public void TestOperatorLessThanLargerInstance()
        {
            string vs1 = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            string vs2 = "01ARZ3NDEKSSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs1);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs2);

            bool actualResult = ulid1 < ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorGreaterThanTwoInstance()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs);

            bool actualResult = ulid1 > ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorGreaterThanSmallerInstance()
        {
            string vs1 = "01ARZ3NDEKSSV4RRFFQ69G5FAV";
            string vs2 = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs1);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs2);

            bool actualResult = ulid1 > ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorGreaterThanLargerInstance()
        {
            string vs1 = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            string vs2 = "01ARZ3NDEKSSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid1 = TensionDev.ULID.Ulid.Parse(vs1);
            TensionDev.ULID.Ulid ulid2 = TensionDev.ULID.Ulid.Parse(vs2);

            bool actualResult = ulid1 > ulid2;
            Assert.True(actualResult);
        }

        [Fact]
        public void TestToCrockfordBase32ArgumentException()
        {
            ulong value = 32;

            Assert.Throws<ArgumentException>(() => { Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value); });
        }

        [Theory]
        [InlineData('0', 0)]
        [InlineData('1', 1)]
        [InlineData('2', 2)]
        [InlineData('3', 3)]
        [InlineData('4', 4)]
        [InlineData('5', 5)]
        [InlineData('6', 6)]
        [InlineData('7', 7)]
        [InlineData('8', 8)]
        [InlineData('9', 9)]
        [InlineData('A', 10)]
        [InlineData('B', 11)]
        [InlineData('C', 12)]
        [InlineData('D', 13)]
        [InlineData('E', 14)]
        [InlineData('F', 15)]
        [InlineData('G', 16)]
        [InlineData('H', 17)]
        [InlineData('J', 18)]
        [InlineData('K', 19)]
        [InlineData('M', 20)]
        [InlineData('N', 21)]
        [InlineData('P', 22)]
        [InlineData('Q', 23)]
        [InlineData('R', 24)]
        [InlineData('S', 25)]
        [InlineData('T', 26)]
        [InlineData('V', 27)]
        [InlineData('W', 28)]
        [InlineData('X', 29)]
        [InlineData('Y', 30)]
        [InlineData('Z', 31)]
        public void TestToCrockfordBase32UInt64(Char expected, ulong value)
        {
            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('0', 0)]
        [InlineData('1', 1)]
        [InlineData('2', 2)]
        [InlineData('3', 3)]
        [InlineData('4', 4)]
        [InlineData('5', 5)]
        [InlineData('6', 6)]
        [InlineData('7', 7)]
        [InlineData('8', 8)]
        [InlineData('9', 9)]
        [InlineData('A', 10)]
        [InlineData('B', 11)]
        [InlineData('C', 12)]
        [InlineData('D', 13)]
        [InlineData('E', 14)]
        [InlineData('F', 15)]
        [InlineData('G', 16)]
        [InlineData('H', 17)]
        [InlineData('J', 18)]
        [InlineData('K', 19)]
        [InlineData('M', 20)]
        [InlineData('N', 21)]
        [InlineData('P', 22)]
        [InlineData('Q', 23)]
        [InlineData('R', 24)]
        [InlineData('S', 25)]
        [InlineData('T', 26)]
        [InlineData('V', 27)]
        [InlineData('W', 28)]
        [InlineData('X', 29)]
        [InlineData('Y', 30)]
        [InlineData('Z', 31)]
        public void TestToCrockfordBase32UInt32(Char expected, uint value)
        {
            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('0', 0)]
        [InlineData('1', 1)]
        [InlineData('2', 2)]
        [InlineData('3', 3)]
        [InlineData('4', 4)]
        [InlineData('5', 5)]
        [InlineData('6', 6)]
        [InlineData('7', 7)]
        [InlineData('8', 8)]
        [InlineData('9', 9)]
        [InlineData('A', 10)]
        [InlineData('B', 11)]
        [InlineData('C', 12)]
        [InlineData('D', 13)]
        [InlineData('E', 14)]
        [InlineData('F', 15)]
        [InlineData('G', 16)]
        [InlineData('H', 17)]
        [InlineData('J', 18)]
        [InlineData('K', 19)]
        [InlineData('M', 20)]
        [InlineData('N', 21)]
        [InlineData('P', 22)]
        [InlineData('Q', 23)]
        [InlineData('R', 24)]
        [InlineData('S', 25)]
        [InlineData('T', 26)]
        [InlineData('V', 27)]
        [InlineData('W', 28)]
        [InlineData('X', 29)]
        [InlineData('Y', 30)]
        [InlineData('Z', 31)]
        public void TestToCrockfordBase32UInt16(Char expected, ushort value)
        {
            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('u')]
        [InlineData('U')]
        public void TestFromCrockfordBase32ArgumentException(Char value)
        {
            Assert.Throws<FormatException>(() => { ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value); });
        }

        [Theory]
        [ClassData(typeof(CrockfordBase32DecodeData))]
        public void TestFromCrockfordBase32(Char value, ulong expected)
        {
            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }
    }
}