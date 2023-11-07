using Microsoft.AspNetCore.Mvc;

namespace Booking.Application.Commons.Models
{
    public class BasePagination
    {
        private const int _defaultSize = 10;

        public BasePagination()
        {
            Page = 1;
            Size = _defaultSize;
            Search = null;
        }
        public BasePagination(int page, int size, string search)
        {
            Page = page;
            Size = size;
            Search = search;
        }

        private int _page;
        private int _size;

        /// <summary>
        /// Page 1~n. Formula (1 <= x <= n, where n is unlimited)
        /// </summary>
        public int Page
        {
            set => _page = value;
            get
            {
                if (_page < 1)
                    _page = 1;

                return _page;
            }
        }

        /// <summary>
        /// Size 1~n. Formula (1 <= x <= n, where n is unlimited)
        /// </summary>
        public int Size
        {
            set => _size = value;
            get
            {
                if (_size < 1)
                    _size = _defaultSize;

                return _size;
            }
        }

        [FromQuery(Name = "Keyword")]
        public string Search { get; set; }

        public int ConvertPageToOffset()
        {
            if ((Page - 1) == 0 || (Page - 1) < 0)
                return 0;
            else
                return (Page - 1) * Size;
        }
    }
}
