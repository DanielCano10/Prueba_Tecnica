//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prueba.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Campo
    {
        public int id_campo { get; set; }
        public int idEncuesta { get; set; }
        public string nombre { get; set; }
        public string titulo { get; set; }
        public string requerido { get; set; }
        public string tipo { get; set; }
    
        public virtual Encuesta Encuesta { get; set; }
    }
}