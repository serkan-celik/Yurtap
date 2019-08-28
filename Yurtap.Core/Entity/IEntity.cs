using System;
using Yurtap.Core.Entity.Enums;

namespace Yurtap.Core.Entity
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        int EkleyenId { set; }
        DateTime EklemeTarihi { get;  }
        int? SonGuncelleyenId { get;  }
        DateTime? SonGuncellemeTarihi { get;  }
        DurumEnum Durum { get; set; }
    }
}
