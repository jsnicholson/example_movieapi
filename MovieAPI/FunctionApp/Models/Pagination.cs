namespace FunctionApp.Models {
    public class Pagination {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPageCount { get; set; }

        public static Pagination DefaultPagination = new() {
            page = 1,
            pageSize = 20
        };
    }
}
