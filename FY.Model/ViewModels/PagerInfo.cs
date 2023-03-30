using System;
using System.Collections.Generic;
using System.Text;

namespace FY.Model.ViewModels
{
    /// <summary>
    /// 分页查询传入参数
    /// </summary>
    public class PagerInfo
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 总页码
        /// </summary>
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                if (TotalNum > 0)
                {
                    return (TotalNum + PageSize - 1) / PageSize;
                }
                return 0;
            }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序类型,前端传入的是"ascending"，"descending"
        /// </summary>
        public string SortType { get; set; }
        public PagerInfo()
        {
            PageNum = 1;
            PageSize = 20;
        }

        public PagerInfo(int page = 1, int pageSize = 20)
        {
            PageNum = page;
            PageSize = pageSize;
        }
    }
}
