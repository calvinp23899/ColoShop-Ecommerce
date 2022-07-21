using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ColoShopEcommerce.WebApp.Models.EF
{
    [Table("FILE")]
    public class FILE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}