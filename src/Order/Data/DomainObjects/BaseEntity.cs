using Order.Data.Enum;
using System;

namespace Order.Data.DomainObjects
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public EntityStatusEnum Status { get; set; }
        public DateTime DateCreateAt { get; set; }
        public DateTime? DateDeleteAt { get; set; }

        protected BaseEntity()
        {

            DateCreateAt = DateTime.Now;
            Status = EntityStatusEnum.Active;
        }

        public void Delete()
        {
            if (Status == EntityStatusEnum.Active)
            {
                Status = EntityStatusEnum.Inactive;
                DateDeleteAt = DateTime.Now;
            }
        }
    }
}