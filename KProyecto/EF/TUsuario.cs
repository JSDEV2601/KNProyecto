//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KProyecto.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class TUsuario
    {
        public long IdUsuario { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public int IdRol { get; set; }
    
        public virtual TRol TRol { get; set; }
    }
}
