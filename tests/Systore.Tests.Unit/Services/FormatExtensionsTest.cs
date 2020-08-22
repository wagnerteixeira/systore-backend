using System;
using System.Collections.Generic;
using Systore.Services;
using Xunit;

namespace Systore.Tests.Unit.Services
{
    public class FormatExtensionsTest : IDisposable
    {
        public FormatExtensionsTest()
        {


        }

        public void Dispose()
        {

        }

        [Fact]
        public void Should_Format_String_With_N_Digits()
        {
            // Arragne
            string test = "1234567890";
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(15));

            // Assert
            Assert.Null(exception);
            Assert.Equal("1234567890     ", ret);
        }

        [Fact]
        public void Should_Format_Int_With_N_Digits()
        {
            // Arragne
            int test = 1234567890;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(15));

            // Assert
            Assert.Null(exception);
            Assert.Equal("000001234567890", ret);
        }

        [Theory]
        [InlineData(1234567890, "000001234567890", 15)]
        [InlineData(null, "          ", 10)]
        public void Should_Format_Nullable_Int_With_N_Digits(int? test, string expected, int digits)
        {
            // Arragne            
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(digits));

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        [Fact]
        public void Should_Format_Long_With_N_Digits()
        {
            // Arragne
            long test = 1234567890;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(15));

            // Assert
            Assert.Null(exception);
            Assert.Equal("000001234567890", ret);
        }

        [Theory]
        [InlineData(1234567890, "000001234567890", 15)]
        [InlineData(null, "          ", 10)]
        public void Should_Format_Nullable_Long_With_N_Digits(long? test, string expected, int digits)
        {
            // Arragne            
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(digits));

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        //INICIO

        [Fact]
        public void Should_Except_Format_String_With_N_Digits()
        {
            // Arragne
            string test = "1234567890";
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(2));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Valor maior que o permitido", exception.Message);
            Assert.IsType<NotSupportedException>(exception);
        }

        [Fact]
        public void Should_Except_Format_Int_With_N_Digits()
        {
            // Arragne
            int test = 1234567890;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(2));

            // Assert
            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Valor maior que o permitido", exception.Message);
            Assert.IsType<NotSupportedException>(exception);
        }

        [Fact]
        public void Should_Except_Format_Nullable_Int_With_N_Digits()
        {
            // Arragne
            int? test = 1234567890;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(2));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Valor maior que o permitido", exception.Message);
            Assert.IsType<NotSupportedException>(exception);
        }

        [Fact]
        public void Should_Except_Format_Long_With_N_Digits()
        {
            // Arragne
            long test = 1234567890;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(2));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Valor maior que o permitido", exception.Message);
            Assert.IsType<NotSupportedException>(exception);
        }

        [Fact]
        public void Should_Except_Format_Nullable_Long_With_N_Digits()
        {
            // Arragne
            long? test = 1234567890;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(2));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Valor maior que o permitido", exception.Message);
            Assert.IsType<NotSupportedException>(exception);
        }

        public static IEnumerable<object[]> FormatDecimalWithNDigits =
            new TheoryData<decimal, int, string>
            {
                {1234567890.12M, 15, "000123456789012" },
                {999.99M, 8, "00099999" },
                {0000.0M, 5, "00000" },
                {0.1M, 5, "00010" },
                {0.01M, 5, "00001" },
            };

        [Theory]
        [MemberData(nameof(FormatDecimalWithNDigits))]
        public void Should_Format_Decimal_With_N_Digits(decimal test, int digits, string expected)
        {
            // Arragne            
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(digits));

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        public static IEnumerable<object[]> FormatNullableDecimalWithNDigits =
            new TheoryData<decimal?, int, string>
            {
                { 1234567890.12M, 15, "000123456789012" },
                { 999.99M, 8, "00099999" },
                { 0000.0M, 5, "00000" },
                { 0.1M, 5, "00010" },
                { 0.01M, 5, "00001" },
                { null, 5, "     " },
                { null, 15, "               " }
            };

        [Theory]
        [MemberData(nameof(FormatNullableDecimalWithNDigits))]
        public void Should_Format_Nullable_Decimal_With_N_Digits(decimal? test, int digits, string expected)
        {
            // Arragne            
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(digits));

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        [Fact]
        public void Should_Except_Format_Decimal_With_N_Digits()
        {
            // Arragne            
            string ret = "";
            decimal test = 1234578.0M;

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(5));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Valor incorreto", exception.Message);
            Assert.IsType<NotSupportedException>(exception);
        }

        [Fact]
        public void Should_Except_Format_Nullable_Decimal_With_N_Digits()
        {
            // Arragne            
            string ret = "";
            decimal? test = 12345678.0M;

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(5));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Valor incorreto", exception.Message);
            Assert.IsType<NotSupportedException>(exception);
        }

        public static IEnumerable<object[]> FormatDateTimeData =
            new TheoryData<DateTime, string, bool>
            {
                { new DateTime(2020, 4, 16), "20200416", false },
                { new DateTime(2020, 4, 16, 0, 24, 0), "202004160024", true },
                { new DateTime(2019, 1, 1), "20190101", false },
                { new DateTime(2017, 4, 16, 0, 59, 0), "201704160059", true },
                { new DateTime(2020, 12, 31, 15, 30, 0), "202012311530", true },
                { new DateTime(2017, 4, 16, 0, 59, 0), "20170416", false },
                { new DateTime(2020, 12, 31, 15, 30, 0), "20201231", false },
            };

        [Theory]
        [MemberData(nameof(FormatDateTimeData))]
        public void Should_Format_Datetime(DateTime test, string expected, bool dateTime)
        {
            // Arragne            
            string ret = "";
            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(dateTime));

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        public static IEnumerable<object[]> FormatTimeSpanData =
           new TheoryData<TimeSpan, string>
           {
                { new TimeSpan(13, 36, 0), "1336"},
                { new TimeSpan(23, 59, 0), "2359"},
                { new TimeSpan(0, 0, 0), "0000"},
                { new TimeSpan(1, 3, 0), "0103"},
           };

        [Theory]
        [MemberData(nameof(FormatTimeSpanData))]
        public void Should_Format_TimeSpan(TimeSpan test, string expected)
        {
            // Arragne            
            string ret = "";
            // Act
            var exception = Record.Exception(() => ret = test.StringFormat());

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        public static IEnumerable<object[]> FormatNullableDateTimeData =
           new TheoryData<DateTime?, string, bool>
           {
                { new DateTime(2020, 4, 16), "20200416", false },
                { new DateTime(2020, 4, 16, 0, 24, 0), "202004160024", true },
                { new DateTime(2019, 1, 1), "20190101", false },
                { new DateTime(2017, 4, 16, 0, 59, 0), "201704160059", true },
                { new DateTime(2020, 12, 31, 15, 30, 0), "202012311530", true },
                { null, "            ", true },
                { null, "        ", false },

           };

        [Theory]
        [MemberData(nameof(FormatNullableDateTimeData))]
        public void Should_Nullable_Format_Datetime(DateTime? test, string expected, bool dateTime)
        {
            // Arragne            
            string ret = "";
            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(dateTime));

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        public static IEnumerable<object[]> FormatNullableTimeSpanData =
          new TheoryData<TimeSpan?, string>
          {
                { new TimeSpan(13, 36, 0), "1336"},
                { new TimeSpan(23, 59, 0), "2359"},
                { new TimeSpan(0, 0, 0), "0000"},
                { new TimeSpan(1, 3, 0), "0103"},
                { null, "    "},
          };

        [Theory]
        [MemberData(nameof(FormatNullableTimeSpanData))]
        public void Should_Format_NullableTimeSpan(TimeSpan? test, string expected)
        {
            // Arragne            
            string ret = "";
            // Act
            var exception = Record.Exception(() => ret = test.StringFormat());

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        public static IEnumerable<object[]> FormatDateTimeAndTimeSpanData =
            new TheoryData<DateTime, TimeSpan, string>
            {
                { new DateTime(2020, 4, 16), new TimeSpan(13, 36, 0), "202004161336"},
                { new DateTime(2020, 4, 16), new TimeSpan(23, 59, 0), "202004162359"},
                { new DateTime(2019, 1, 1), new TimeSpan(0, 0, 0), "201901010000"},
                { new DateTime(2017, 4, 16), new TimeSpan(0, 59, 0), "201704160059"},
                { new DateTime(2020, 12, 31), new TimeSpan(15, 30, 0), "202012311530"},
                { new DateTime(2017, 4, 16), new TimeSpan(1, 3, 0), "201704160103" },
                { new DateTime(2020, 12, 31), new TimeSpan(15, 30, 0), "202012311530"},
            };

        [Theory]
        [MemberData(nameof(FormatDateTimeAndTimeSpanData))]
        public void Should_Format_DatetimeAndTimeSpan(DateTime test, TimeSpan timeSpanTest, string expected)
        {
            // Arragne            
            string ret = "";
            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(timeSpanTest));

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        public static IEnumerable<object[]> FormatNullableDateTimeAndTimeSpanData =
            new TheoryData<DateTime?, TimeSpan?, string>
            {
                { new DateTime(2020, 4, 16), new TimeSpan(13, 36, 0), "202004161336"},
                { new DateTime(2020, 4, 16), new TimeSpan(23, 59, 0), "202004162359"},
                { new DateTime(2019, 1, 1), new TimeSpan(0, 0, 0), "201901010000"},
                { new DateTime(2017, 4, 16), new TimeSpan(0, 59, 0), "201704160059"},
                { new DateTime(2020, 12, 31), new TimeSpan(15, 30, 0), "202012311530"},
                { new DateTime(2017, 4, 16), new TimeSpan(1, 3, 0), "201704160103" },
                { new DateTime(2020, 12, 31), new TimeSpan(15, 30, 0), "202012311530"},
                { null, null, "            "},
                { new DateTime(2020, 4, 16), null, "20200416    "},
                { null, new TimeSpan(23, 59, 0), "        2359"},
                { new DateTime(2019, 1, 1), null, "20190101    "},
                { null, new TimeSpan(0, 59, 0), "        0059"},
                { null, new TimeSpan(0, 0, 0), "        0000"},
            };

        [Theory]
        [MemberData(nameof(FormatNullableDateTimeAndTimeSpanData))]
        public void Should_Format_Nullable_DatetimeAndTimeSpan(DateTime? test, TimeSpan? timeSpanTest, string expected)
        {
            // Arragne            
            string ret = "";
            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(timeSpanTest));

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }


        [Theory]
        [InlineData("1", 1)]
        [InlineData("32767", 32767)]
        [InlineData("-32767", -32767)]
        [InlineData("1000", 1000)]
        [InlineData("999", 999)]
        public void Should_Convert_String_ToShort(string test, short expected)
        {
            // Arragne            
            short ret = 0;
            // Act
            var exception = Record.Exception(() => ret = test.ToShort());

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("32767", 32767)]
        [InlineData("-32767", -32767)]
        [InlineData("1000", 1000)]
        [InlineData("999", 999)]
        [InlineData("2147483647", 2147483647)]
        [InlineData("-2147483647", -2147483647)]
        [InlineData("12345678", 12345678)]
        [InlineData("-12345678", -12345678)]
        public void Should_Convert_String_ToInt(string test, int expected)
        {
            // Arragne            
            int ret = 0;
            // Act
            var exception = Record.Exception(() => ret = test.ToInt());

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        [Theory]
        [InlineData("2147483647", 2147483647)]
        [InlineData("-2147483647", -2147483647)]
        [InlineData("12345678", 12345678)]
        [InlineData("-9223372036854775807", -9223372036854775807)]
        [InlineData("9223372036854775807", 9223372036854775807)]
        [InlineData("1234567890", 1234567890)]
        public void Should_Convert_String_ToLong(string test, long expected)
        {
            // Arragne            
            long ret = 0;
            // Act
            var exception = Record.Exception(() => ret = test.ToLong());

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        public static IEnumerable<object[]> FormatConvertStringToDecimalData =
             new TheoryData<string, decimal>
             {
                { "2147483647.69", 2147483647.69M },
                { "-2147483647.25", 0.0M },
                { "12345678.01", 12345678.01M },
                { "-9223372036854775807.54", 0.0M },
                { "9223372036854775807.15", 9223372036854775807.15M },
                { "1234567890.01", 1234567890.01M }
             };

        [Theory]
        [MemberData(nameof(FormatConvertStringToDecimalData))]
        public void Should_Convert_String_ToDecimal(string test, decimal expected)
        {
            // Arragne            
            decimal ret = 0.0M;
            // Act
            var exception = Record.Exception(() => ret = test.ToDecimal());

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void Should_Convert_String_ToBoolean(string test, bool expected)
        {
            // Arragne            
            bool ret = false;
            // Act
            var exception = Record.Exception(() => ret = test.ToBoolean());

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }


        public static IEnumerable<object[]> StringTryToDateTimeData =
             new TheoryData<string, DateTime?>
             {
                { "", null },
                { "     ", null },
                { "01/10/2019 13:10:20,123", new DateTime(2019, 10, 1, 13, 10, 20, 123) },
                { "15/09/2010 01:01:01,000", new DateTime(2010, 9, 15, 1, 1, 1, 0) },
                { "2019-05-15 13:10:20.123", new DateTime(2019, 5, 15, 13, 10, 20, 123) },
                { "2020-12-31 01:01:02.000", new DateTime(2020, 12, 31, 1, 1, 2, 0) },
                { "01/10/19 13:10:20,123", new DateTime(2019, 10, 1, 13, 10, 20, 123) },
                { "15/09/10 01:01:01,000", new DateTime(2010, 9, 15, 1, 1, 1, 0) },
                { "19-05-15 13:10:20.123", new DateTime(2019, 5, 15, 13, 10, 20, 123) },
                { "20-12-31 01:01:02.000", new DateTime(2020, 12, 31, 1, 1, 2, 0) },
                { "123456789", null },
                { "dfklglkdfjglkjfç", null },
                { "123/45/67 89", null },
                { "123-45-67 89", null },
             };

        [Theory]
        [MemberData(nameof(StringTryToDateTimeData))]
        public void Should_String_TryToDateTime(string test, DateTime? expected)
        {
            // Arragne            
            DateTime? ret = null;
            // Act
            var exception = Record.Exception(() => ret = test.TryToDateTime());

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        public static IEnumerable<object[]> StringToDateTimeData =
             new TheoryData<string, DateTime?>
             {
                { "", new DateTime(1, 1, 1) },
                { "     ", new DateTime(1, 1, 1) },
                { "01/10/2019 13:10:20,123", new DateTime(2019, 10, 1, 13, 10, 20, 123) },
                { "15/09/2010 01:01:01,000", new DateTime(2010, 9, 15, 1, 1, 1, 0) },
                { "2019-05-15 13:10:20.123", new DateTime(2019, 5, 15, 13, 10, 20, 123) },
                { "2020-12-31 01:01:02.000", new DateTime(2020, 12, 31, 1, 1, 2, 0) },
                { "01/10/19 13:10:20,123", new DateTime(2019, 10, 1, 13, 10, 20, 123) },
                { "15/09/10 01:01:01,000", new DateTime(2010, 9, 15, 1, 1, 1, 0) },
                { "19-05-15 13:10:20.123", new DateTime(2019, 5, 15, 13, 10, 20, 123) },
                { "20-12-31 01:01:02.000", new DateTime(2020, 12, 31, 1, 1, 2, 0) },
             };


        [Theory]
        [MemberData(nameof(StringToDateTimeData))]
        public void Should_String_ToDateTime(string test, DateTime? expected)
        {
            // Arragne            
            DateTime? ret = null;
            // Act
            var exception = Record.Exception(() => ret = test.ToDateTime());

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }


        public static IEnumerable<object[]> StringTruncateData =
            new TheoryData<string, string, int>
            {
                { null, null, 10 } ,
                { "1234567890", "12345", 5 },
                { "1234567890", "1234567890", 10 },
                { "1234567890", "123456", 6 },
                { "1234567890", "12", 2 },
            };


        [Theory]
        [MemberData(nameof(StringTruncateData))]
        public void Should_String_Truncate(string test, string expected, int maxLength)
        {
            // Arragne            
            string ret = "";
            // Act
            var exception = Record.Exception(() => ret = test.Truncate(maxLength));
            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }

        public static IEnumerable<object[]> EqualsIgnoreCaseData =
           new TheoryData<string, string, bool>
           {
                { null, null, true } ,
                { "TEST", "test", true },
                { "Testing", "Testing", true },
                { "vaLUE", "Value", true },
                { "TestING", "Testing", true },
                { "Testing", "Test", false },
                { "vaLUE", "Should", false },
                { "Evaluate", "Value", false },
           };


        [Theory]
        [MemberData(nameof(EqualsIgnoreCaseData))]
        public void Should_Equals_Ignore_Case(string testA, string textB, bool expected)
        {
            // Arragne            
            bool ret = false;
            // Act
            var exception = Record.Exception(() => ret = testA.EqualsIgnoreCase(textB));
            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, ret);
        }


    }
}
