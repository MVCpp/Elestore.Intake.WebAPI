    using System;
    namespace Elestor.Intake.API.Models
    {
        public class Negocio
        {
            public Negocio()
            {
            }


        public string clientid{get; set;} = string.Empty;
        public string nombre {get; set;} = string.Empty;
        public string callenumero {get; set;} = string.Empty;
        public string colonia {get; set;} = string.Empty;
        public string ciudad {get; set;} = string.Empty;
        public string estado {get; set;} = string.Empty;
        public string codigopostal {get; set;} = string.Empty;
        public string horaapertura {get; set;} = string.Empty;
        public string horacierre {get; set;} = string.Empty;
        public string categoria {get; set;} = string.Empty;
        public string subcategoria{get; set;} = string.Empty;
        public string descripcion {get; set;} = string.Empty;
        }
    }
