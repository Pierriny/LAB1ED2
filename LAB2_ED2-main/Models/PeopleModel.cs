using System;
using System.ComponentModel.DataAnnotations;



namespace LAB2_ED2.Models
{
    public class PeopleModel
    {
        [Required(ErrorMessage = "El campo de Nombre es requerido.")]
        [Display(Name = "name")]
        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1'´'-'\s]{1,20}$", ErrorMessage = "Cáracteres Inválidos.")]
        public string name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(13)]
        [System.ComponentModel.DataAnnotations.MinLength(13)]
        [System.ComponentModel.DataAnnotations.Range(0000000010101, 9999999992217)]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^[0-9]{1,13}$", ErrorMessage = "Caractéres Inválidos")]
        [System.ComponentModel.DataAnnotations.Display(Name = "dpi")]
        public System.Int64 dpi { get; set; }

        [Required(ErrorMessage = "Introduzca una fecha de nacimiento.")]
        [Display(Name = "datebirth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime? datebirth { get; set; }

        [Required(ErrorMessage = "Introduzca una dirección")]
        [Display(Name = "address")]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1'´'-'\s]{1,20}$", ErrorMessage = "Cáracteres Inválidos.")]
        public string address { get; set; }

        [Required(ErrorMessage = "El campo de las companias es requerido.")]
        [Display(Name = "companies")]
        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1'´'-'\s]{1,20}$", ErrorMessage = "Cáracteres Inválidos.")]
        public String[] companies { get; set; }

        public string[] Symbol { get; set; }

        public String[] FrequencyP { get; set; }

        public String[] Probability { get; set; }
        public String[] RangeP { get; set; }

        public String[] Inferior { get; set; }

        public String[] Superior { get; set; }


        public String[] Encode { get; set; }
        public String[] Decode { get; set; }

        public String[] EncodeLZ78 { get; set; }
        public String[] DecodeLZ78 { get; set; }

    }
}
