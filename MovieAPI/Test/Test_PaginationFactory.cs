using FunctionApp.Factories;
using FunctionApp.Models;
using Xunit;

namespace Test {
    public class Test_PaginationFactory {
        [Theory]
        [InlineData(47, 10, 5)]
        [InlineData(20, 5, 4)]
        [InlineData(100, 20, 5)]
        public void CalculateTotalPageCount_ReturnsCorrectValue(int itemCount, int pageSize, int expectedResult) {
            int totalPages = PaginationFactory.CalculateTotalPageCount(itemCount, pageSize);

            Assert.Equal(expectedResult, totalPages);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("abc")]
        [InlineData("-1")]
        public void CreatePagination_BadPageThrowsException(string parameterPage) {
            string parameterPageSize = "10";
            int itemCount = 100;

            Assert.ThrowsAny<Exception>(() => PaginationFactory.CreatePagination(parameterPage, parameterPageSize, itemCount));
        }

        public static IEnumerable<object[]> TestData_CreatePagination_CreatesCorrectPagination =>
            new List<object[]> {
                new object[] { "1", "10", 100, new Pagination { page=1, pageSize=10, totalPageCount=10 } }
            };

        [Theory]
        [MemberData(nameof(TestData_CreatePagination_CreatesCorrectPagination))]
        public void CreatePagination_CreatesCorrectPagination(string parameterPage, string parameterPageSize, int itemCount, Pagination expectedResult) {
            Pagination pagination = PaginationFactory.CreatePagination(parameterPage, parameterPageSize, itemCount);

            // havent defined Equals for Pagination so checking each member
            Assert.Equal(pagination.page, expectedResult.page);
            Assert.Equal(pagination.pageSize, expectedResult.pageSize);
            Assert.Equal(pagination.totalPageCount, expectedResult.totalPageCount);
        }
    }
}
