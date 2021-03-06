﻿using System;
using System.Security.Cryptography.X509Certificates;
using CfdiSharp.Comprobante;
using CfdiSharp.src.Comprobante;
using CfdiSharp.Util;

namespace CfdiSharpTest
{
    class CfdiCreator
    {
        public static Comprobante CreateComprobante()
        {
            var comp = new Comprobante
            {
                Emisor = new Emisor(),
                Receptor = new Receptor()
            };
            var regimenFiscal = new RegimenFiscal();
            var concepto = new Concepto();
            var traslado = new Traslado();
            comp.Emisor.DomicilioFiscal = new DomicilioFiscal();
            comp.Receptor.Domicilio = new Domicilio();
            comp.Impuestos = new Impuestos();



            comp.Fecha = DateTime.Now;
            comp.FormaDePago = "PAGO EN UNA SOLA EXHIBICION";
            comp.MetodoDePago = "NO IDENTIFICADO";
            comp.LugarExpedicion = "San Luis Potosí";
            comp.TipoDeComprobante = TipoDeComprobante.ingreso;
            comp.SubTotal = 1.00m;
            comp.Total = 1.16m;

            comp.Emisor.Rfc = "AAA010101AAA";
            comp.Emisor.DomicilioFiscal.Calle = "Himalaya";
            comp.Emisor.DomicilioFiscal.Municipio = "San Luis Potosí";
            comp.Emisor.DomicilioFiscal.Estado = "San Luis Potosí";
            comp.Emisor.DomicilioFiscal.Pais = "México";
            comp.Emisor.DomicilioFiscal.CodigoPostal = "78000";

            regimenFiscal.Regimen = "Régimen de las Personas Físicas con Actividades Empresariales y Profesionales";
            comp.Emisor.RegimenFiscal = new[] { regimenFiscal };

            comp.Receptor.Rfc = "AAA010101AAA";
            comp.Receptor.Domicilio.Pais = "México";

            concepto.Cantidad = 1;
            concepto.Unidad = "NO APLICA";
            concepto.Descripcion = "SERVICIO X";
            concepto.Importe = 1.00m;
            concepto.ValorUnitario = 1.00m;
            comp.Conceptos = new[] { concepto };

            traslado.Impuesto = TrasladoImpuesto.IEPS;
            traslado.Importe = 0.16m;
            traslado.Tasa = 16m;
            comp.Impuestos.Traslados = new[] { traslado };


            //opcionales
            if (true)
            {
                comp.Emisor.Nombre = "PANCHO VILLA";
                comp.Emisor.DomicilioFiscal.NoExterior = "1005";
                comp.Emisor.DomicilioFiscal.NoInterior = "124";
                comp.Emisor.DomicilioFiscal.Colonia = "Jose del Cantaro";
                comp.Emisor.DomicilioFiscal.Localidad = "San Luis Potosí";
                comp.Receptor.Domicilio.Calle = "AV. V. CARRANZA";
                comp.Receptor.Domicilio.CodigoPostal = "78000";
                comp.Receptor.Domicilio.Colonia = "Centro";
                comp.Receptor.Domicilio.NoExterior = "715";
                comp.Receptor.Domicilio.NoInterior = "PH";
                comp.Receptor.Domicilio.Localidad = "San Luis Potosí";
                comp.Receptor.Domicilio.Municipio = "San Luis Potosí";
                comp.Receptor.Domicilio.Estado = "San Luis Potosí";
                comp.Impuestos.TotalImpuestosTrasladados = 0.16m;
                comp.Serie = "U";
                comp.Folio = "0001";
            }

            return comp;
        }

        public static void SetSello(Comprobante comp)
        {
            var cer = new X509Certificate2(@"C:\Users\user\Desktop\CERAAA010101A23KEY.pfx", "12345678a",
                X509KeyStorageFlags.MachineKeySet);
            var cadena = Util.Sello.GenerarCadenaOriginal(comp);

            comp.NoCertificado = Util.Sello.NoCertificado(cer);
            comp.Certificado = Convert.ToBase64String(cer.RawData);
            comp.Sello = Util.Sello.GenerarSello(cer, cadena);
        }
    }
}
