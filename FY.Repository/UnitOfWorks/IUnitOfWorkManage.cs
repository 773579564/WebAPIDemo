using System.Reflection;
using SqlSugar;

namespace FY.Repository.UnitOfWorks
{
    public interface IUnitOfWorkManage
    {
        /// <summary>
        /// 获取指定数据上下文类型的实例
        /// </summary>
        SqlSugarScope GetDbClient();
        int TranCount { get; }

        UnitOfWork CreateUnitOfWork();

        void BeginTran();
        void BeginTran(MethodInfo method);
        void CommitTran();
        void CommitTran(MethodInfo method);
        void RollbackTran();
        void RollbackTran(MethodInfo method);
    }
}