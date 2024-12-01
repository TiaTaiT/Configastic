using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configastic.SharedModels.Interfaces
{
    public enum States : byte
    {
        UnderGuard = 24, // Взят
        RemovedGuard = 109, // Снят
        Alarm = 3, // Тревога
        TakeFailed = 17, // Невзятие
        Unknow = 0 // неопознан
    }
}
