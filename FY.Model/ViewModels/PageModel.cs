using System;
using System.Collections.Generic;
using System.Text;

namespace FY.Model.ViewModels
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalNum { get; set; }
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
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 分页查询结果
        /// </summary>
        public List<T> Result { get; set; }
        public Dictionary<string, object> Extra { get; set; } = new Dictionary<string, object>();


        /// <summary>
        /// 初始化分页返回实体
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="result">分页查询结果</param>
        /// <param name="totalPage">总记录数</param>
        public PageModel(int pageIndex, int pageSize, List<T> result, int totalPage)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Result = result;
            TotalNum = totalPage;
        }

    }
}
