using System;
using System.ComponentModel.DataAnnotations;

namespace LAB2_ED2.Models
{
    
    public class CodeModel
    {

        public string Simbolo { get; set; }
        public int Frecuenia { get; set; }
        public Double Probabilidad { get; set; }
        public Double LimiteInferior { get; set; }
        public Double LimiteSuperior { get; set; }
    }
   
}