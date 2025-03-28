﻿namespace verticalSliceArchitecture.Shared
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public Dictionary<string, string> Links { get; set; } = new();
    }
}
