using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Services
{
    public abstract class ExtractSearchBase
    {
        private TimeSpan workTime = default;
        public abstract string Name { get; }
        public string NameStatus { get; }

        public TimeSpan WorkTime
        {
            get
            {
                return workTime;
            }
        }

        public async Task<Status> Request(int wait, int randomMin, int randomMax)
        {
            if (randomMin <= 0)
                throw new ArgumentOutOfRangeException("Число не может быть отрицательным или равняться нулю, повторите запрос!", nameof(randomMin));
            if (randomMax <= 0)
                throw new ArgumentOutOfRangeException("Число не может быть отрицательным или равняться нулю, повторите запрос!", nameof(randomMax));
            if (randomMin > randomMax)
                throw new ArgumentException("Минимальное значение не должно превышать максимальное");

            CancellationTokenSource CancellationTokenSource = new CancellationTokenSource(wait + 80);
            CancellationToken cancellationToken = CancellationTokenSource.Token;

            await Task.Run(async () =>
            {
                workTime = await Search(randomMin, randomMax);
            }, cancellationToken);

            if (CancellationTokenSource.IsCancellationRequested)
                return Status.TimeOut;

            return GetStatus();
        }

        private Status GetStatus()
        {
            Random random = new Random();
            Array values = Enum.GetValues(typeof(Status)).OfType<Status>().Where(t => t != Status.TimeOut).ToArray();
            return (Status)values.GetValue(random.Next(values.Length));
        }

        private async Task<TimeSpan> Search(int randomMin, int randomMax)
        {
            Random random = new Random();
            var result = TimeSpan.FromMilliseconds(random.Next(randomMin, randomMax));
            await Task.Delay(result);
            return result;
        }
    }

    public enum Status : byte
    {
        [Description("ОК")]
        OK = 1,
        [Description("TIMEOUT")]
        TimeOut = 2,
        [Description("Ошибка")]
        Error = 50,
    }

    public static class Extension
    {
        public static string GetStatusName(this Status enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (field != null)
            {
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                    return attributes[0].Description;
            }

            throw new ArgumentException("Item was not found!", nameof(enumValue));
        }
    }
}
