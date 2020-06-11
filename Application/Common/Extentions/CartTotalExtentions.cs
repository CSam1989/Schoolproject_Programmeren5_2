using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Application.Common.Extentions
{
    public static class CartTotalExtentions
    {
        public static int GetTotalItems(this IEnumerable<OrderLine> orderlines)
        {
            return orderlines.Sum(ol => ol.Quantity);
        }

        public static decimal GetTotalPrice(this IEnumerable<OrderLine> orderlines)
        {
            return orderlines.Sum(ol => ol.Product.Price * ol.Quantity);
        }

        public static decimal GetVat(this IEnumerable<OrderLine> orderlines)
        {
            var vatPercentage = 0.06M;
            return orderlines.GetTotalPrice() * vatPercentage;
        }

        public static decimal GetTotalPriceExclVat(this IEnumerable<OrderLine> orderlines)
        {
            var orderLines = orderlines as OrderLine[] ?? orderlines.ToArray();
            return orderLines.GetTotalPrice() - orderLines.GetVat();
        }
    }
}