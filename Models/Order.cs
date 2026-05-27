namespace demo27._05
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            order_details = new HashSet<OrderDetails>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int order_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_order { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_arrive { get; set; }

        public int pickup_point_id { get; set; }

        public int user_id { get; set; }

        public int code { get; set; }

        public int status_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> order_details { get; set; }

        public virtual PickupPoints pickup_points { get; set; }

        public virtual Status status { get; set; }

        public virtual User user { get; set; }
    }
}
