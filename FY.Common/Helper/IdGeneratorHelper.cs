using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FY.Common.Extensions;

namespace FY.Common.Helper
{
    /// <summary>
    /// 生成数据库主键Id,雪花算法
    /// </summary>
    public class IdGeneratorHelper
    {
        private int SnowFlakeWorkerId = GlobalContext.SystemConfig.SnowFlakeWorkerId;

        private Snowflake snowflake;

        private static readonly IdGeneratorHelper instance = new IdGeneratorHelper();

        private IdGeneratorHelper()
        {
            snowflake = new Snowflake(SnowFlakeWorkerId, 0, 0);
        }
        public static IdGeneratorHelper Instance
        {
            get
            {
                return instance;
            }
        }
        public long GetId()
        {
            return snowflake.NextId();
        }
    }

    public class Snowflake
    {
        private const long TwEpoch = 1546272000000L;//2019-01-01 00:00:00

        private const int WorkerIdBits = 5;
        private const int DatacenterIdBits = 5;
        private const int SequenceBits = 12;
        private const long MaxWorkerId = -1L ^ -1L << WorkerIdBits;
        private const long MaxDatacenterId = -1L ^ -1L << DatacenterIdBits;

        private const int WorkerIdShift = SequenceBits;
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        private const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;
        private const long SequenceMask = -1L ^ -1L << SequenceBits;

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;
        /// <summary>
        ///10位的数据机器位中的高位
        /// </summary>
        public long WorkerId { get; protected set; }
        /// <summary>
        /// 10位的数据机器位中的低位
        /// </summary>
        public long DatacenterId { get; protected set; }

        private readonly object _lock = new object();
        /// <summary>
        /// 基于Twitter的snowflake算法
        /// </summary>
        /// <param name="workerId">10位的数据机器位中的高位，默认不应该超过5位(5byte)</param>
        /// <param name="datacenterId"> 10位的数据机器位中的低位，默认不应该超过5位(5byte)</param>
        /// <param name="sequence">初始序列</param>
        public Snowflake(long workerId, long datacenterId, long sequence = 0L)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;

            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException($"worker Id can't be greater than {MaxWorkerId} or less than 0");
            }

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException($"datacenter Id can't be greater than {MaxDatacenterId} or less than 0");
            }
        }

        public long CurrentId { get; private set; }

        /// <summary>
        /// 获取下一个Id，该方法线程安全
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {
            lock (_lock)
            {
                var timestamp = DateTime.Now.ToUnixTimestampByMilliseconds();
                if (timestamp < _lastTimestamp)
                {
                    //小于上一个时间表示存在时间回拨的现象
                    //TODO 是否可以考虑直接等待？
                    throw new Exception(
                        $"Clock moved backwards or wrapped around. Refusing to generate id for {_lastTimestamp - timestamp} ticks");
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = _sequence + 1 & SequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }
                _lastTimestamp = timestamp;
                CurrentId = timestamp - TwEpoch << TimestampLeftShift |
                         DatacenterId << DatacenterIdShift |
                         WorkerId << WorkerIdShift | _sequence;

                return CurrentId;
            }
        }

        private long TilNextMillis(long lastTimestamp)
        {
            var timestamp = DateTime.Now.ToUnixTimestampByMilliseconds();
            while (timestamp <= lastTimestamp)
            {
                timestamp = DateTime.Now.ToUnixTimestampByMilliseconds();
            }
            return timestamp;
        }
    }
}
