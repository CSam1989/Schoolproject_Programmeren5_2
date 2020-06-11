using Microsoft.AspNetCore.Mvc;

namespace Application.Common.Models
{
    public class PagFilterSortingModel
    {
        [BindProperty(SupportsGet = true)] public int? CurrentPage { get; set; } = 1;

        public int? PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }
    }
}