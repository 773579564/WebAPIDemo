﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FY.Model.Entity
{
    /// <summary>
    /// 数据模型接口
    /// </summary>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 实体唯一标识，主键
        /// </summary>
        TKey Id { get; set; }

    }
}
