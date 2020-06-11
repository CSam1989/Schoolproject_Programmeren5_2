using System;
using Microsoft.AspNetCore.Mvc;

namespace Application.Common.Models
{
    // Special thanks to Mike @ https://www.mikesdotnetting.com/article/328/simple-paging-in-asp-net-core-razor-pages

    public class PagSortFilterToReturnDto
    {
        public PagSortFilterToReturnDto()
        {
            CurrentPage ??= 1;
        }

        [BindProperty(SupportsGet = true)] public int? CurrentPage { get; set; }

        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int) Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public string SortBy { get; set; }
        public ProductSortingModel ProductSortingModel { get; set; }
        public OrderSortingModel OrderSortingModel { get; set; }

        public string CurrentFilter { get; set; }
    }
}