using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FY.Common.Helper
{
    public class AsyncTaskHelper
    {
        /// <summary>
        /// 开始异步任务
        /// </summary>
        /// <param name="action"></param>
        public static void StartTask(Action action)
        {
            try
            {
                Action newAction = () =>
                { };
                newAction += action;
                Task task = new Task(newAction);
                task.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }


        public static async void StartTask(Dictionary<string, string> keyValuePairs)
        {
            Task task1 = Task.Run(async () =>
            {
                ConcurrentDictionary<string, StringBuilder> keyValues = new ConcurrentDictionary<string, StringBuilder>();
                await Task.Delay(100);
                Parallel.ForEach(keyValuePairs, async v =>
                {
                    keyValues[v.Key] = new StringBuilder();
                    await HttpClientHelper.PostAsync(v.Key, v.Value, keyValues[v.Key]);
                    await Task.Delay(30);
                });

                foreach (var v in keyValues)
                {
                    LogHelper.Error(v.Value);
                }
            });

            task1.Wait();

        }

    }
}
