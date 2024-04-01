using FunctionApp.Models;
using System;

namespace FunctionApp.Factories {
    public class PaginationFactory {
        public static Pagination CreatePagination(string parameterPage, string parameterPageSize, int itemCount) {
            int totalPageCount = CalculateTotalPageCount(itemCount, Pagination.DefaultPagination.pageSize);
            if (string.IsNullOrEmpty(parameterPage) || string.IsNullOrEmpty(parameterPageSize)) {
                Pagination pagination = Pagination.DefaultPagination;
                pagination.totalPageCount = totalPageCount;
                return pagination;
            }

            int page;
            try {
                page = int.Parse(parameterPage);
            } catch (Exception exception) {
                throw new Exception($"'{parameterPage}' is not a valid '{nameof(parameterPage)}' value. '{nameof(parameterPage)}' must be a positive integer");
            }
            if (page < 1) {
                throw new ArgumentException($"'{parameterPage}' is not a valid '{nameof(parameterPage)}' value. '{nameof(parameterPage)}' must be a positive integer");
            }

            int pageSize;
            try {
                pageSize = int.Parse(parameterPageSize);
            } catch (Exception exception) {
                throw new Exception($"'{parameterPageSize}' is not a valid '{nameof(parameterPageSize)}' value. '{nameof(parameterPageSize)}' must be a positive integer");
            }
            if (pageSize < 1) {
                throw new ArgumentException($"'{parameterPageSize}' is not a valid '{nameof(parameterPageSize)}' value. '{nameof(parameterPageSize)}' must be a positive integer");
            }

            return new Pagination() {
                page = page,
                pageSize = pageSize,
                totalPageCount = CalculateTotalPageCount(itemCount, pageSize)
            };
        }

        public static int CalculateTotalPageCount(int itemCount, int pageSize) {
            return (int)Math.Ceiling((double)itemCount / pageSize);
        }
    }
}
