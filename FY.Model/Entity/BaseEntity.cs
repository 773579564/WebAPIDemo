using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using FY.Common.Helper;
using SqlSugar;

namespace FY.Model.Entity
{
    public class BaseEntity : IEntity<long>
    {
        public BaseEntity() 
        {
            Id = 0;
        }

        /// <summary>
        /// 所有表的主键
        /// long返回到前端js的时候，会丢失精度，所以转成字符串
        /// </summary>
        /// 
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 判断主键是否为空，常用做判定操作是【添加】还是【编辑】
        /// </summary>
        /// <returns></returns>
        public bool KeyIsNull()
        {
            return Id == 0;
        }

        /// <summary>
        /// 创建默认的主键值
        /// </summary>
        public void GenerateDefaultKeyVal()
        {
            Id = IdGeneratorHelper.Instance.GetId();
        }

    }

    public class BaseCreateEntity : BaseEntity
    {
        public BaseCreateEntity()
        {
            BaseCreatorId = 0;
            BaseCreateTime = DateTime.Now;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime BaseCreateTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long BaseCreatorId { get; set; }

        public void Create(long _baseCreatorId)
        {
            if (KeyIsNull())
            {
                GenerateDefaultKeyVal();
            }
            if (BaseCreatorId == 0)
            {
                BaseCreateTime = DateTime.Now;
                BaseCreatorId = _baseCreatorId;
            }
        }
    }

    public class BaseModifyEntity : BaseCreateEntity
    {
        public BaseModifyEntity()
        {
            BaseVersion = 0;
            BaseModifyTime = DateTime.Now;
            BaseModifierId = 0;
        }

        /// <summary>
        /// 数据更新版本
        /// </summary>
        public int BaseVersion { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime BaseModifyTime { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public long BaseModifierId { get; set; }


        public new void Create(long _baseCreatorId)
        {
            base.Create(_baseCreatorId);

            BaseVersion = 0;
            BaseModifyTime = DateTime.Now;
            BaseModifierId = _baseCreatorId;
        }
        public void Modify(long _baseModifierId)
        {
            BaseVersion = BaseVersion + 1;
            BaseModifyTime = DateTime.Now;
            BaseModifierId = _baseModifierId;
        }
    }

    public class BaseExtensionEntity : BaseModifyEntity
    {
        public BaseExtensionEntity()
        {
            BaseIsDelete = 0;
        }

        /// <summary>
        /// 是否删除 1是，0否
        /// </summary>
        public int BaseIsDelete { get; set; }

        public new void Create(long _baseCreatorId)
        {
            base.Create(_baseCreatorId);

            base.Modify(_baseCreatorId);
        }

        public new void Modify(long _baseModifierId)
        {
            base.Modify(_baseModifierId);
        }
    }

    public class BaseField
    {
        public static string[] BaseFieldList = new string[]
        {
            "Id",
            "BaseIsDelete",
            "BaseCreateTime",
            "BaseModifyTime",
            "BaseCreatorId",
            "BaseModifierId",
            "BaseVersion"
        };
    }

}
