using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
namespace SYE.Covol
{
    public class ControlesVolumeticos : IDisposable
    {
        private bool disposed = false;
        public int Errores { get; set; } = 0;
        public string XML { get; set; }
        public string FileName { get; set; }
        public ILogger Logger { get; }

        public ControlesVolumeticos(ILogger logger = null)
        {
            Logger = logger;
        }

        public async Task<int> ProcesaXML(Stream inStream, string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(inStream);
            inStream.Position = 0;
            int mensaje;
            string ResultadoXML = string.Empty;

            try
            {
                Errores = 0;
                Logger?.LogInformation($"Inicia Validación del archivo {FileName}");
                bool esValido = ValidaXML(inStream, fileName, xmlDoc.InnerXml);
                //System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
                //bool esValido = true;
                if (esValido)
                {

                    ResultadoXML = xmlDoc.InnerXml.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "")
                        .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>","")
                        .Replace("xmlns=\"http://www.sat.gob.mx/esquemas/controlesvolumetricos\"","");

                    Logger?.LogInformation("Validación Exitosa.");
                    Logger?.LogInformation($"Inicia proceso de inserción en B.D. archivo: {fileName}");
                    mensaje = await new CtrolVolumetrico(Logger).InsertaXML_CtrolVol(ResultadoXML, fileName);
                    Logger?.LogInformation("Termina proceso de inserción en B.D.");

                }
                else
                {
                    Logger?.LogWarning($"Validación con Errores en el archivo: {fileName}");

                    mensaje = -1;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError($"Error de excepción xml: {ex.Message.ToString()}");
                object[] objeto = new object[3];
                objeto[0] = "ERROR";
                objeto[1] = "Archivo con error en la linea: " + ex.Message;
                objeto[2] = fileName;
                Logger?.LogError("Archivo con error en la linea: " + ex.Message);

                new CtrolVolumetrico(Logger).InsertaResultadoCarga(xmlDoc.InnerXml, objeto);
                mensaje = -1;
                //Logger?.LogWarning($"Error de procesamiento: {ex.Message}");
                //throw new ArgumentException(ex.Message);
            }
            finally
            {
                if (inStream != null)
                {
                    inStream.Close();
                }
                //mensaje = -1;
            }

            return mensaje;
        }

        private bool ValidaXML(Stream inStream, string _fileName, string _xml)
        {
            bool resultado = false;
            

            string path = Path.Combine("XSD", "version 1_2.xsd");
            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.Schemas.Add("http://www.sat.gob.mx/esquemas/controlesvolumetricos", path);

            //            XmlSchemaSet schemas = new XmlSchemaSet();
            xmlSettings.ValidationType = ValidationType.Schema;
            using (XmlReader xmlReader = XmlReader.Create(inStream, xmlSettings))
            {
                try
                {



                    //XmlReader reader = XmlReader.Create(inStream, xmlSettings);
                    XmlDocument document = new XmlDocument();
                    document.Load(xmlReader);


                    ValidationEventHandler eventHandler = new ValidationEventHandler(XmlSettingsValidationEventHandler);


                    // the following call to Validate succeeds.
                    document.Validate(eventHandler);

                    resultado = true;
                }
                catch (Exception ex)
                {
                    Logger?.LogError($"Error de excepción xml: {ex.Message.ToString()}");
                    object[] objeto = new object[3];
                    objeto[0] = "ERROR";
                    objeto[1] = "Archivo con error en la linea: " + ex.Message;
                    objeto[2] = _fileName;
                    Logger?.LogError("Archivo con error en la linea: " + ex.Message);
                    resultado = false;
                    new CtrolVolumetrico(Logger).InsertaResultadoCarga(_xml, objeto);

                }
                finally
                {
                    xmlReader.Close();
                }
            }



            //XML = document.InnerXml;
            //FileName = _fileName;
            //bool resultado = true;
            //Logger?.LogInformation($"Inicia proceso de validación con archivo xml: {_fileName}");
            //xmlSettings.XmlResolver = new XmlUrlResolver();
            //xmlSettings.Schemas = schemas;
            //xmlSettings.ValidationType = ValidationType.Schema;
            //xmlSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            //xmlSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
            //xmlSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            //xmlSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            //xmlSettings.ValidationEventHandler += new ValidationEventHandler(XmlSettingsValidationEventHandler);
            ////ValidationEventHandler eventHandler = new ValidationEventHandler(XmlSettingsValidationEventHandler);
            //Logger?.LogInformation("Validando archivo xml");

            //using (XmlReader xml = XmlReader.Create(inStream, xmlSettings))
            //{
            //    try
            //    {
            //        //XmlDocument document = new XmlDocument();
            //        //document.Load(xml);
            //        //document.Validate(eventHandler);
            //        //if (Errores > 0)
            //        //{
            //        //    Logger?.LogError($"Error en la validacion del archivo XML {FileName}");
            //        //    resultado = false;
            //        //}
            //        while (xml.Read())
            //        {
            //            if (Errores > 0)
            //            {
            //                Logger?.LogError($"Error en la validacion del archivo XML {FileName}");
            //                resultado = false;
            //                break;
            //            }
            //        }
            //    }
            //    catch (XmlException ex)
            //    {
            //        Logger?.LogError($"Error de excepción xml: {ex.Message.ToString()}");
            //        throw new ArgumentException(ex.Message);
            //    }
            //    catch (Exception ex)
            //    {
            //        Logger?.LogError($"Error de excepción: {ex.Message.ToString()}");
            //        throw new ArgumentException(ex.Message);
            //    }
            //    finally
            //    {
            //        xml.Close();
            //        inStream.Close();
            //    }
            //}

            return resultado;
        }

        private void XmlSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (Errores < 1 && e.Severity == XmlSeverityType.Error)
            {
                Errores++;
                string campo = string.Empty;
                string valor = string.Empty;
                try
                {
                    PropertyInfo property = sender.GetType().GetProperty("Name", BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    campo = property != null ? property.GetValue(sender).ToString() : string.Empty;
                    property = sender.GetType().GetProperty("Value", BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    valor = property != null ? property.GetValue(sender).ToString() : string.Empty;
                    object[] objeto = new object[3];
                    objeto[0] = "ERROR";
                    objeto[1] = "Archivo con error en la linea: " + e.Exception.LineNumber + " atributo: " + campo + " valor no válido según su tipo de datos: " + valor;
                    objeto[2] = FileName;
                    Logger?.LogError("Archivo con error en la linea: " + e.Exception.LineNumber + " posición: " + e.Exception.LinePosition + " atributo: " + campo + " valor no válido según su tipo de datos: " + valor, e.Exception);
                    new CtrolVolumetrico(Logger).InsertaResultadoCarga(XML, objeto);
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex.Message, ex);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            disposed = true;
        }

        ~ControlesVolumeticos()
        {
            Dispose(false);
        }
    }
}
