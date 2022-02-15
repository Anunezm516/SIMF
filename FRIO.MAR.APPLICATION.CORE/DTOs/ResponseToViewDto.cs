using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DTOs
{
    public class ResponseToViewDto
    {
        public bool Estado { get; set; }
        public dynamic Data { get; set; }
        public string Mensaje { get; set; }
        public string CodigoError { get; set; }
    }
}
