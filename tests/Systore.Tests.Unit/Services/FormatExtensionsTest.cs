using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        [Fact]
        public void Should_Format_Nullable_Int_With_N_Digits()
        {
            // Arragne
            int? test = 1234567890;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(15));

            // Assert
            Assert.Null(exception);
            Assert.Equal("000001234567890", ret);
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

        [Fact]
        public void Should_Format_Nullable_Long_With_N_Digits()
        {
            // Arragne
            long? test = 1234567890;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(15));

            // Assert
            Assert.Null(exception);
            Assert.Equal("000001234567890", ret);
        }

        [Fact]
        public void Should_Format_Decimal_With_N_Digits()
        {
            // Arragne
            decimal test = 1234567890M;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(15));

            // Assert
            Assert.Null(exception);
            Assert.Equal("1234567890     ", ret);
        }

        [Fact]
        public void Should_Format_Nullable_Decimal_With_N_Digits()
        {
            // Arragne
            decimal? test = 1234567890M;
            string ret = "";

            // Act
            var exception = Record.Exception(() => ret = test.StringFormat(15));

            // Assert
            Assert.Null(exception);
            Assert.Equal("1234567890     ", ret);
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
    }
}
