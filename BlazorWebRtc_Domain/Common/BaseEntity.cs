using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebRtc_Domain.Common
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
            Id = Guid.NewGuid();
        }
        public DateTime CreateDate { get; set; }
        public Guid Id { get; set; }
    }
}
