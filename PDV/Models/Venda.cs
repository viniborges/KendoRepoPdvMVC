using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PDV.Models
{
    public class Venda
    {
        //public Venda()
        //{
        //    VendaItens = new List<VendaItem>();
        //}
        public int VendaId { get; set; }

        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
        
        [Display(Name = "Data da Venda")]
        [DataType(DataType.DateTime)]
        public DateTime DataMovimento { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public double ValorTotalVenda { get; set; }
        public virtual IList<VendaItem> VendaItem { get; set; }

    }
}