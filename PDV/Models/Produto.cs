using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PDV.Models
{
    public class Produto
    {
        [Display(Name = "Código")]
        [HiddenInput(DisplayValue = false)]
        public int ProdutoID {get; set; }

        [Display(Name = "Descrição")]
        public string Descricao {get; set; }

        [Display(Name = "Preço de Custo")]
        public double PrecoCusto {get; set; }

        [Display(Name = "Quantidade")]
        public int Qtde {get; set; }

        [Display(Name = "Preço de Venda")]
        public double PrecoVenda {get; set; }
    }
}