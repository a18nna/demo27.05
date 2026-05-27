namespace demo27._05
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_details = new HashSet<OrderDetails>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        [Required]
        [StringLength(6)]
        public string article { get; set; }

        public int label_id { get; set; }

        [Required]
        [StringLength(50)]
        public string unit { get; set; }

        public decimal price { get; set; }

        public int supplier_id { get; set; }

        public int fabric_id { get; set; }

        public int category_id { get; set; }

        public int discount { get; set; }

        public int amount { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string description { get; set; }

        [StringLength(50)]
        public string photo { get; set; }

        public virtual Category Category { get; set; }

        public virtual Fabric Fabric { get; set; }

        public virtual Label Label { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> Order_details { get; set; }

        public virtual Supplier Supplier { get; set; }

        [NotMapped]
        public decimal DiscountedPrice => price * (1 - discount / 100m);
    }
}
