﻿using System;
using System.Net;

namespace prueba.Aplicacion.ManejadorError
{
    public class ManejadorExcepcion : Exception
    {
        public HttpStatusCode Codigo { get; }
        public object Errores { get; }

        public ManejadorExcepcion(HttpStatusCode codigo, object errores = null)
        {
            Codigo = codigo;
            Errores = errores;
        }
    }
}
