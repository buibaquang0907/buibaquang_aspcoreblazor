using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buibaquang_aspcoreblazor.Models.SeedWork
{
    public class PageList<T>
    {
        public MetaData MetaData { get; set; }
        public List<T> Items { get; set; }
        public PageList() { }
        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Items = items;

        }
    }
}
