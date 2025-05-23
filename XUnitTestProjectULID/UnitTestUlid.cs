using System;

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
            byte[]? vs = null;
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestConstructorInvalidSizeByteArray()
        {
            byte[] vs = new byte[17];
            ArgumentException ex = Assert.Throws<ArgumentException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
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
            string? vs = null;
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestConstructorEmptyString()
        {
            string vs = string.Empty;
            FormatException ex = Assert.Throws<FormatException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestConstructorInvalidSizeString()
        {
            string vs = "0123456789";
            FormatException ex = Assert.Throws<FormatException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestConstructorInvalidCharacterString()
        {
            string vs = "01ARZ3NDEKTSU4RRFFQ69G5FAV";
            FormatException ex = Assert.Throws<FormatException>(() => { TensionDev.ULID.Ulid ulid = new TensionDev.ULID.Ulid(vs); });
        }

        [Fact]
        public void TestTryParseValid()
        {
            string expectedULID = "01ARZ3NDEKTSV4RRFFQ69G5FAV";

            bool result = TensionDev.ULID.Ulid.TryParse(expectedULID, out TensionDev.ULID.Ulid ulid);
            Assert.Equal(expectedULID, ulid.ToString());
            Assert.True(result);
        }

        [Fact]
        public void TestTryParseInalid()
        {
            string expectedULID = "01ARZ3NDEKTSU4RRFFQ69G5FAV";

            bool result = TensionDev.ULID.Ulid.TryParse(expectedULID, out TensionDev.ULID.Ulid _);
            Assert.False(result);
        }

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
            TensionDev.ULID.Ulid? ulid2 = null;

            int actualResult = ulid1.CompareTo(ulid2);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestEqualsNull()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid? ulid2 = null;

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
            TensionDev.ULID.Ulid? ulid = null;
            TensionDev.ULID.Ulid? ulid2 = null;

            bool actualResult = ulid == ulid2;
            Assert.True(actualResult);
        }

        [Fact]
        public void TestOperatorEqualsNull1()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid? ulid2 = null;

            bool actualResult = ulid == ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorEqualsNull2()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid? ulid = null;
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
            TensionDev.ULID.Ulid? ulid = null;
            TensionDev.ULID.Ulid? ulid2 = null;

            bool actualResult = ulid != ulid2;
            Assert.False(actualResult);
        }

        [Fact]
        public void TestOperatorNotEqualsNull1()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid ulid = TensionDev.ULID.Ulid.Parse(vs);
            TensionDev.ULID.Ulid? ulid2 = null;

            bool actualResult = ulid != ulid2;
            Assert.True(actualResult);
        }

        [Fact]
        public void TestOperatorNotEqualsNull2()
        {
            string vs = "01ARZ3NDEKTSV4RRFFQ69G5FAV";
            TensionDev.ULID.Ulid? ulid = null;
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

            ArgumentException ex = Assert.Throws<ArgumentException>(() => { Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value); });
        }

        [Fact]
        public void TestToCrockfordBase32_0()
        {
            Char expected = '0';
            ulong value = 0;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_1()
        {
            Char expected = '1';
            ulong value = 1;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_2()
        {
            Char expected = '2';
            ulong value = 2;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_3()
        {
            Char expected = '3';
            ulong value = 3;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_4()
        {
            Char expected = '4';
            ulong value = 4;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_5()
        {
            Char expected = '5';
            ulong value = 5;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_6()
        {
            Char expected = '6';
            ulong value = 6;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_7()
        {
            Char expected = '7';
            ulong value = 7;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_8()
        {
            Char expected = '8';
            ulong value = 8;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_9()
        {
            Char expected = '9';
            ulong value = 9;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_A()
        {
            Char expected = 'A';
            ulong value = 10;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_B()
        {
            Char expected = 'B';
            ulong value = 11;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_C()
        {
            Char expected = 'C';
            ulong value = 12;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_D()
        {
            Char expected = 'D';
            ulong value = 13;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_E()
        {
            Char expected = 'E';
            ulong value = 14;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_F()
        {
            Char expected = 'F';
            ulong value = 15;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_G()
        {
            Char expected = 'G';
            ulong value = 16;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_H()
        {
            Char expected = 'H';
            ulong value = 17;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_J()
        {
            Char expected = 'J';
            ulong value = 18;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_K()
        {
            Char expected = 'K';
            ulong value = 19;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_M()
        {
            Char expected = 'M';
            ulong value = 20;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_N()
        {
            Char expected = 'N';
            ulong value = 21;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_P()
        {
            Char expected = 'P';
            ulong value = 22;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_Q()
        {
            Char expected = 'Q';
            ulong value = 23;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_R()
        {
            Char expected = 'R';
            ulong value = 24;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_S()
        {
            Char expected = 'S';
            ulong value = 25;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_T()
        {
            Char expected = 'T';
            ulong value = 26;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_V()
        {
            Char expected = 'V';
            ulong value = 27;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_W()
        {
            Char expected = 'W';
            ulong value = 28;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_X()
        {
            Char expected = 'X';
            ulong value = 29;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_Y()
        {
            Char expected = 'Y';
            ulong value = 30;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToCrockfordBase32_Z()
        {
            Char expected = 'Z';
            ulong value = 31;

            Char actual = TensionDev.ULID.Ulid.ToCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32ArgumentException()
        {
            Char value = 'U';

            FormatException ex = Assert.Throws<FormatException>(() => { ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value); });
        }

        [Fact]
        public void TestFromCrockfordBase32_0()
        {
            Char value = '0';
            ulong expected = 0;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_1()
        {
            Char value = '1';
            ulong expected = 1;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_2()
        {
            Char value = '2';
            ulong expected = 2;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_3()
        {
            Char value = '3';
            ulong expected = 3;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_4()
        {
            Char value = '4';
            ulong expected = 4;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_5()
        {
            Char value = '5';
            ulong expected = 5;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_6()
        {
            Char value = '6';
            ulong expected = 6;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_7()
        {
            Char value = '7';
            ulong expected = 7;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_8()
        {
            Char value = '8';
            ulong expected = 8;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_9()
        {
            Char value = '9';
            ulong expected = 9;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_A()
        {
            Char value = 'A';
            ulong expected = 10;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_B()
        {
            Char value = 'B';
            ulong expected = 11;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_C()
        {
            Char value = 'C';
            ulong expected = 12;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_D()
        {
            Char value = 'D';
            ulong expected = 13;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_E()
        {
            Char value = 'E';
            ulong expected = 14;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_F()
        {
            Char value = 'F';
            ulong expected = 15;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_G()
        {
            Char value = 'G';
            ulong expected = 16;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_H()
        {
            Char value = 'H';
            ulong expected = 17;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_J()
        {
            Char value = 'J';
            ulong expected = 18;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_K()
        {
            Char value = 'K';
            ulong expected = 19;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_M()
        {
            Char value = 'M';
            ulong expected = 20;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_N()
        {
            Char value = 'N';
            ulong expected = 21;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_P()
        {
            Char value = 'P';
            ulong expected = 22;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_Q()
        {
            Char value = 'Q';
            ulong expected = 23;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_R()
        {
            Char value = 'R';
            ulong expected = 24;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_S()
        {
            Char value = 'S';
            ulong expected = 25;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_T()
        {
            Char value = 'T';
            ulong expected = 26;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_V()
        {
            Char value = 'V';
            ulong expected = 27;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_W()
        {
            Char value = 'W';
            ulong expected = 28;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_X()
        {
            Char value = 'X';
            ulong expected = 29;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_Y()
        {
            Char value = 'Y';
            ulong expected = 30;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFromCrockfordBase32_Z()
        {
            Char value = 'Z';
            ulong expected = 31;

            ulong actual = TensionDev.ULID.Ulid.FromCrockfordBase32(value);
            Assert.Equal(expected, actual);
        }
    }
}