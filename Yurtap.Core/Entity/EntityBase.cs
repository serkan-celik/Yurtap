using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Yurtap.Core.Entity.Enums;

namespace Yurtap.Core.Entity
{
    public abstract class EntityBase<T> : IEntity<T>
    {
        private int _currentUserId=0;
        public EntityBase()
        {
            var principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            if (principal != null)
            {
                _currentUserId = int.Parse(principal.Claims.SingleOrDefault(c => c.Type == "id").Value);
            }
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }
        public int EkleyenId
        {
            get { return _currentUserId; }
            set
            {
                value = _currentUserId;
            }
        }
        public DateTime EklemeTarihi { get; set; } =  DateTime.Now;
        public int? SonGuncelleyenId
        {
            get;
            set;
        }
        public DateTime? SonGuncellemeTarihi
        {
            get;
            set;
        }

        public DurumEnum Durum { get; set; } = DurumEnum.Aktif;
    }
}
