using LAB2_ED2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Text;
using System.Reflection.Emit;

namespace LAB2_ED2.Controllers
{
    public class PeopleMController : Controller
    {

        public IActionResult Index()
        {
            return View();
        } 

        public IActionResult PeopleList()
        {
            return View(LAB2_ED2.Models.Singleton.Instance.AVLTree);
        } 

        public IActionResult AddPeople() 
        {
            return View();
        }  



        public void AlgLZ78(PeopleModel ObjTodec)
        {
            String MessageToDec = "";
            String CodifAlgLZ78 = "";
            String DecodifAlgLZ78 = "";
            var NodePosition = LAB2_ED2.Models.Singleton.Instance.LZ78Array.Head;
            var ActualNode = LAB2_ED2.Models.Singleton.Instance.LZ78Array.Head;
            foreach (var Compani in ObjTodec.companies)
            {
                LAB2_ED2.Models.Singleton.Instance.LZ78Array.Clear();
                MessageToDec = Compani + Convert.ToString(ObjTodec.dpi);
                int IndexCount = 1;
                bool JumpSymbol = false;
                AlgoritmoLZ78 NodeLZ78 = new AlgoritmoLZ78();
                bool Jump = false;
                int CountMessage = 0;
                foreach (var symbol in MessageToDec)
                {
                    if (JumpSymbol == true)
                    {
                        NodeLZ78.Entry = NodeLZ78.Entry + Convert.ToString(symbol);
                        int CountTempSyT = 0;
                        ActualNode = LAB2_ED2.Models.Singleton.Instance.LZ78Array.Head;
                        while (CountTempSyT < LAB2_ED2.Models.Singleton.Instance.LZ78Array.Count() - 1 && ActualNode.Value.Entry != NodeLZ78.Entry)
                        {
                            ActualNode = ActualNode.Two;
                            CountTempSyT++;
                        }

                        if (ActualNode.Value.Entry == NodeLZ78.Entry)
                        {
                            NodeLZ78.Codigo = Convert.ToString(ActualNode.Value.Indice) + ">";
                            JumpSymbol = true;
                        }
                        else
                        {
                            NodeLZ78.Codigo = NodeLZ78.Codigo + Convert.ToString(symbol);
                            LAB2_ED2.Models.Singleton.Instance.LZ78Array.Insert(NodeLZ78);
                            JumpSymbol = false;
                        }
                    }
                    else
                    {
                        if (LAB2_ED2.Models.Singleton.Instance.LZ78Array.Count() == 0)
                        {
                            NodeLZ78 = new AlgoritmoLZ78();
                            NodeLZ78.Indice = IndexCount;
                            NodeLZ78.Codigo = "0," + symbol;
                            NodeLZ78.Entry = Convert.ToString(symbol);
                            LAB2_ED2.Models.Singleton.Instance.LZ78Array.Insert(NodeLZ78);
                            JumpSymbol = false;
                        }
                        else
                        {
                            CountMessage = 0;
                            ActualNode = LAB2_ED2.Models.Singleton.Instance.LZ78Array.Head;
                            NodePosition = LAB2_ED2.Models.Singleton.Instance.LZ78Array.Head;
                            bool NewSymbol = true;

                            do
                            {
                                if (ActualNode.Value.Entry == Convert.ToString(symbol))
                                {
                                    NewSymbol = false;
                                    NodePosition = ActualNode;//Guarda la primera coincidencia encontrada
                                }

                                if (ActualNode.Two != null)
                                {
                                    ActualNode = ActualNode.Two;
                                }
                                CountMessage++;
                            } while (CountMessage < LAB2_ED2.Models.Singleton.Instance.LZ78Array.Count() && NewSymbol == true);

                            if (NewSymbol == true)
                            {
                                NodeLZ78 = new AlgoritmoLZ78();
                                IndexCount++;
                                NodeLZ78.Indice = IndexCount;
                                NodeLZ78.Codigo = "0," + symbol;
                                NodeLZ78.Entry = Convert.ToString(symbol);
                                LAB2_ED2.Models.Singleton.Instance.LZ78Array.Insert(NodeLZ78);
                            }
                            else
                            {
                                NodeLZ78 = new AlgoritmoLZ78();
                                IndexCount++;
                                NodeLZ78.Indice = IndexCount;
                                NodeLZ78.Codigo = Convert.ToString(NodePosition.Value.Indice) + ",";
                                NodeLZ78.Entry = NodePosition.Value.Entry;
                                JumpSymbol = true;
                            }
                        }
                    }
                }

                foreach (var item in LAB2_ED2.Models.Singleton.Instance.LZ78Array)
                {
                    CodifAlgLZ78 = CodifAlgLZ78 + "<" + item.Codigo + ">";
                    DecodifAlgLZ78 = DecodifAlgLZ78 + item.Entry;
                }
                CodifAlgLZ78 = CodifAlgLZ78 + "{";
                DecodifAlgLZ78 = DecodifAlgLZ78 + "{";
            }
            String[] DataSplitCodifAlgLZ78 = CodifAlgLZ78.Split('{');
            String[] DataSplitDecodifAlgLZ78 = DecodifAlgLZ78.Split('{');

            ObjTodec.EncodeLZ78 = DataSplitCodifAlgLZ78;
            ObjTodec.DecodeLZ78 = DataSplitDecodifAlgLZ78;
        }








        [HttpPost]
        public IActionResult AddPeople(IFormCollection IncomeData)
        {
            var NewData = new PeopleModel();
            NewData.name = IncomeData["name"];
            NewData.dpi = Convert.ToInt64(IncomeData["dpi"]);
            NewData.datebirth = Convert.ToDateTime(IncomeData["datebirth"]);
            NewData.address = IncomeData["address"];
            NewData.companies = IncomeData["companies"];

            Singleton.Instance.AVLTree.Add(NewData, Models.Delegates.DPIComparison);
            return View("/Views/Home/Index.cshtml");
        }

        public IActionResult UploadPeopleFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadPeopleFile(Microsoft.AspNetCore.Http.IFormFile FILE)
        {
            if (FILE != null)
            {
                try
                {
                    string Route = System.IO.Path.Combine(System.IO.Path.GetTempPath(), FILE.Name);
                    using (var Stream = new System.IO.FileStream(Route, System.IO.FileMode.Create))
                    {
                        FILE.CopyTo(Stream);
                    }
                    string FileData = System.IO.File.ReadAllText(Route);
                    foreach (string linea in FileData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(linea))
                        {
                            String[] action = linea.Split(';');
                            String[] JsSplit1 = action[1].Split("\r");
                            String[] JsSplit2 = JsSplit1[0].Split(',');
                            String JsTemp = JsSplit1[0];
                            JsPeople jsonString = JsonSerializer.Deserialize<JsPeople>(JsTemp);


                            PeopleModel DataJs = new PeopleModel();
                            DataJs.name = jsonString.name;
                            DataJs.dpi = Convert.ToInt64(jsonString.dpi);
                            DataJs.datebirth = Convert.ToDateTime(jsonString.datebirth);
                            DataJs.address = jsonString.address;
                            DataJs.companies = jsonString.companies;



                            if (action[0] == "INSERT")
                            {                            
                                AlgLZ78(DataJs);// Para codificar LZ78
                                SearchFilesToCod(Convert.ToString(DataJs.dpi));// cifrar cartas
                                SearchFilesToDecs(Convert.ToString(DataJs.dpi));// decifrar cartas
                                LAB2_ED2.Models.Singleton.Instance.AVLTree.Add(DataJs, LAB2_ED2.Models.Delegates.DPIComparison);
                            }
                            else if (action[0] == "DELETE")
                            {
                                LAB2_ED2.Models.Singleton.Instance.AVLTree.Delete(DataJs, LAB2_ED2.Models.Delegates.DPIComparison);
                            }
                            else
                            {
                                EditFile(DataJs, jsonString.datebirth);
                            }
                        }
                    }
                }
                catch (System.Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }




        private void SearchAndSave(string? id)
        {
            var DataToSearch = new LAB2_ED2.Models.PeopleModel();
            DataToSearch.dpi = Convert.ToInt64(id);
            var NodeValues = LAB2_ED2.Models.Singleton.Instance.AVLTree.SearchElement(DataToSearch, Delegates.DPIComparison);
            if (NodeValues != null)
            {
                LAB2_ED2.Models.Singleton.Instance.SearchedItem = NodeValues.Value;
            }
        }

        public IActionResult ViewPeopleData(string? id)
        {
            if (id != null)
            {
                SearchAndSave(id);
            }
            return View(LAB2_ED2.Models.Singleton.Instance.SearchedItem);
        }

        public IActionResult ViewEncodeLZ78(string? id)
        {
            if (id != null)
            {
                SearchAndSave(id);
            }
            return View(LAB2_ED2.Models.Singleton.Instance.SearchedItem);
        }

        public IActionResult ViewDecodeLZ78(string? id)
        {
            if (id != null)
            {
                SearchAndSave(id);
            }
            return View(LAB2_ED2.Models.Singleton.Instance.SearchedItem);
        }


        public IActionResult ResultPeopleList()
        {
            return View(LAB2_ED2.Models.Singleton.Instance.SearchedItems);
        }

        public IActionResult DeletePeopleData(string? id)
        {
            var DataToSearch = new LAB2_ED2.Models.PeopleModel();
            DataToSearch.dpi = System.Convert.ToInt64(id);
            LAB2_ED2.Models.Singleton.Instance.AVLTree.Delete(DataToSearch, Delegates.DPIComparison);
            return RedirectToAction("Index");
        }

        public IActionResult SearchPeopleData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchPeopleData(Microsoft.AspNetCore.Http.IFormCollection IncomeData)
        {
            string rutaArchivo = "C:\\Users\\hanse\\Documents\\2023\\2023segundociclo\\Esctructura de datos II\\Laboratorio\\Laboratorio1\\LAB1ED2\\LAB2_ED2-main\\Files\\Bitacora.txt";
            bool sobrescribir = false;

            DateTime thisDay = DateTime.Today;

            StreamWriter sw = new StreamWriter(rutaArchivo, !sobrescribir);

            var DataToSearch = new LAB2_ED2.Models.PeopleModel();
            LAB2_ED2.Models.PeopleModel? UniqueResult = null;
            System.Collections.Generic.List<LAB2_ED2.Models.PeopleModel> Lista = LAB2_ED2.Models.Singleton.Instance.AVLTree.ToList();
            LAB2_ED2.Models.Singleton.Instance.SearchedItem = null;

            switch (System.Convert.ToInt64(IncomeData["Variable"]))
            {
                case 1:
                    DataToSearch.name = IncomeData["name"];
                    LAB2_ED2.Models.Singleton.Instance.SearchedItems = Lista.FindAll(x => x.name.ToUpper() == DataToSearch.name.ToUpper());

                    
                   
                    sw.Write("Se  realizado una busqueda por Nombre " + IncomeData["name"] + " el dia: " + thisDay.ToLongDateString() + " \n");
                    sw.Close();

                    break;
                case 2:
                    DataToSearch.dpi = Convert.ToInt64((IncomeData["dpi"]));
                    LAB2_ED2.Models.Singleton.Instance.SearchedItems = Lista.FindAll(x => x.dpi.ToString() == DataToSearch.dpi.ToString());

                 
                    sw.Write("Se  realizado una busqueda por DPI " + Convert.ToInt64((IncomeData["dpi"])) + " el dia: " + thisDay.ToLongDateString() + " \n");
                    sw.Close();

                    break;
            }

            
            if (UniqueResult == null && LAB2_ED2.Models.Singleton.Instance.SearchedItems.Count == 0)
            {
                ViewBag.Message = "No se encontraron coincidencias.";
                return View();
            }
            else
            {
                if (LAB2_ED2.Models.Singleton.Instance.SearchedItems.Count == 1)
                {
                    UniqueResult = LAB2_ED2.Models.Singleton.Instance.SearchedItems[0];
                    LAB2_ED2.Models.Singleton.Instance.SearchedItems.Clear();
                }
                if (UniqueResult == null && LAB2_ED2.Models.Singleton.Instance.SearchedItems.Count != 0)
                {
                    return RedirectToAction("ResultPeopleList");
                }
                else
                {
                    LAB2_ED2.Models.Singleton.Instance.SearchedItem = UniqueResult;
                    return RedirectToAction("ViewPeopleData");
                }
            }
        }

        public IActionResult EditPeopleData(string? id)
        {
            var DataToSearch = new LAB2_ED2.Models.PeopleModel();
            DataToSearch.dpi = Convert.ToInt64(id);
            var NodeValues = LAB2_ED2.Models.Singleton.Instance.AVLTree.SearchElement(DataToSearch, Delegates.DPIComparison);
            if (NodeValues != null)
            {
                LAB2_ED2.Models.Singleton.Instance.SearchedItem = NodeValues.Value;
            }
            return View(LAB2_ED2.Models.Singleton.Instance.SearchedItem);
        }

        [HttpPost]
        public IActionResult EditPeopleData(Microsoft.AspNetCore.Http.IFormCollection IncomeData)
        {
            var DataToSearch = new LAB2_ED2.Models.PeopleModel();
            DataToSearch.dpi = LAB2_ED2.Models.Singleton.Instance.SearchedItem.dpi;
            DataToSearch.name = IncomeData["name"];
            DataToSearch.datebirth = Convert.ToDateTime(IncomeData["datebirth"]);
            DataToSearch.address = IncomeData["address"];
            LAB2_ED2.Models.Singleton.Instance.SearchedItem = null;
            var NodeValues = LAB2_ED2.Models.Singleton.Instance.AVLTree.SearchElement(DataToSearch, Delegates.DPIComparison);
            if (NodeValues != null)
            {
                if ((int)LAB2_ED2.Models.Delegates.CompareString(NodeValues.Value.name, DataToSearch.name) != 0 || (int)LAB2_ED2.Models.Delegates.CompareDateTime(Convert.ToDateTime(NodeValues.Value.datebirth),Convert.ToDateTime(DataToSearch.datebirth)) != 0 || (int)LAB2_ED2.Models.Delegates.CompareString(NodeValues.Value.address,DataToSearch.address) != 0)
                {
                    NodeValues.Value.name = DataToSearch.name;
                    NodeValues.Value.address = DataToSearch.address;
                    NodeValues.Value.datebirth = DataToSearch.datebirth;
                }
            }
            return View(nameof(Index));
        }

        public void EditFile(PeopleModel EditData, string birth)
        {
            LAB2_ED2.Models.Singleton.Instance.SearchedItem = null;
            var NodeValues = LAB2_ED2.Models.Singleton.Instance.AVLTree.SearchElement(EditData, Delegates.DPIComparison);
            if (NodeValues != null)
            {
                if ((int)LAB2_ED2.Models.Delegates.CompareString(NodeValues.Value.name, EditData.name) != 0 || (int)LAB2_ED2.Models.Delegates.CompareDateTime(Convert.ToDateTime(NodeValues.Value.datebirth),Convert.ToDateTime(EditData.datebirth)) != 0 || (int)LAB2_ED2.Models.Delegates.CompareString(NodeValues.Value.address, EditData.address) != 0)
                {
                    NodeValues.Value.name = EditData.name;
                    NodeValues.Value.address = EditData.address;
                    NodeValues.Value.datebirth = Convert.ToDateTime(birth);
                }
            }
        }









        //--------------------------------------------------------------------------------------









        /*
        public string InvertirTexto(string texto)
        {
            char[] caracteres = texto.ToCharArray();
            Array.Reverse(caracteres);
            return new string(caracteres);
        }
        */

        static string LeerArchivoATexto(string ruta)
        {
            // Usar StreamReader para leer el contenido del archivo
            using (StreamReader reader = new StreamReader(ruta))
            {
                return reader.ReadToEnd();
            }
        }


        static void CrearYGuardarArchivo(string ruta, string nombreArchivo, string contenido)
        {
            // Combina la ruta y el nombre del archivo
            string rutaCompleta = Path.Combine(ruta, nombreArchivo);

            // Usar StreamWriter para escribir el contenido en el archivo
            using (StreamWriter writer = new StreamWriter(rutaCompleta))
            {
                writer.Write(contenido);
            }
        }


        public void SearchFilesToCod(String DpiToSearch)
        {
            bool Cycle = true;
            int NumCard = 1;
            int clave = 3; //Numero de posiciones para transponer
            string contenido;
            
            string Ruta = @"C:\Users\hanse\Documents\2023\2023segundociclo\Esctructura de datos II\Laboratorio\Laboratorio1\LAB1ED2\LAB2_ED2-main\Files\CartasCifradas\";
            while (Cycle == true)
            {
                try
                {
                    DirectoryInfo TempPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    TempPath = TempPath.Parent.Parent.Parent;
                    string PathString = Convert.ToString(TempPath);

                    string archivo = System.IO.Path.Combine(PathString, "Files", "inputs", "REC-" + Convert.ToString(DpiToSearch) + "-" + Convert.ToString(NumCard) + ".txt");

                    contenido = LeerArchivoATexto(archivo);

                    string nombreN = "compressed-REC-" + Convert.ToString(DpiToSearch) + "-" + Convert.ToString(NumCard) + ".txt";

                    CrearYGuardarArchivo(Ruta, nombreN, CodificarTransposicionSimple(contenido, clave));


                    /*
                    DirectoryInfo TempPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    TempPath = TempPath.Parent.Parent.Parent;
                    string PathString = Convert.ToString(TempPath);
                    string Route = System.IO.Path.Combine(PathString, "Files", "inputs", "REC-" + Convert.ToString(DpiToSearch) + "-" + Convert.ToString(NumCard) + ".txt");
                    string Route2 = System.IO.Path.Combine(PathString, "Files", "CartasCodificadas", "compressed-REC-" + Convert.ToString(DpiToSearch) + "-" + Convert.ToString(NumCard) + ".txt");
                    string FileData = System.IO.File.ReadAllText(Route);
                    using (StreamWriter OutputFile = new StreamWriter(Route2))
                    {
                        OutputFile.WriteLine(CodificarTransposicionSimple(FileData,clave));//Message Coded save
                    }
                    */



                }
                catch (FileNotFoundException)
                {
                    Cycle = false;
                }
                catch (Exception e)
                {
                    Cycle = false;
                }
                NumCard++;
            }
        }




        public void SearchFilesToDecs(String DpiToSearch)
        {
            bool Cycle = true;
            int NumCard = 1;
            int clave2 = 3;
            string contenido2;
            string Ruta2 = @"C:\Users\hanse\Documents\2023\2023segundociclo\Esctructura de datos II\Laboratorio\Laboratorio1\LAB1ED2\LAB2_ED2-main\Files\CartasDecifradas";
            while (Cycle == true)
            {
                try
                {
                    DirectoryInfo TempPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    TempPath = TempPath.Parent.Parent.Parent;
                    string PathString = Convert.ToString(TempPath);

                    string archivo = System.IO.Path.Combine(PathString, "Files", "CartasCifradas", "compressed-REC-" + Convert.ToString(DpiToSearch) + "-" + Convert.ToString(NumCard) + ".txt");

                    contenido2 = LeerArchivoATexto(archivo);

                    string nombreN2 = "Decode - REC - " + Convert.ToString(DpiToSearch) + " - " + Convert.ToString(NumCard) + ".txt";

                    CrearYGuardarArchivo(Ruta2, nombreN2, DecodificarTransposicionSimple(contenido2, clave2));


                    /*

                    DirectoryInfo TempPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    TempPath = TempPath.Parent.Parent.Parent;
                    string PathString = Convert.ToString(TempPath);
                    string Route = System.IO.Path.Combine(PathString, "Files", "CartasCodificadas", "compressed-REC-" + Convert.ToString(DpiToSearch) + "-" + Convert.ToString(NumCard) + ".txt");
                    string Route2 = System.IO.Path.Combine(PathString, "Files", "CartasDecodificadas", "Decode-REC-" + Convert.ToString(DpiToSearch) + "-" + Convert.ToString(NumCard) + ".txt");
                    string FileData = System.IO.File.ReadAllText(Route);
                    using (StreamWriter OutputFile = new StreamWriter(Route2))
                    {
                        OutputFile.WriteLine(DecodificarTransposicionSimple(FileData, clave2));
                    }

                    */

                }
                catch (FileNotFoundException)
                {
                    Cycle = false;
                }
                catch (Exception e)
                {
                    Cycle = false;
                }

                NumCard++;
            }

        }







        static string CodificarTransposicionSimple(string texto, int clave)
        {
            char[] caracteres = texto.ToCharArray();
            StringBuilder resultado = new StringBuilder();

            for (int i = 0; i < caracteres.Length; i++)
            {
                if (char.IsLetter(caracteres[i]))
                {
                    char letra = caracteres[i];
                    char nuevaLetra = (char)(letra + clave);

                    if (char.IsUpper(letra) && nuevaLetra > 'Z')
                    {
                        nuevaLetra = (char)(nuevaLetra - 26);
                    }
                    else if (char.IsLower(letra) && nuevaLetra > 'z')
                    {
                        nuevaLetra = (char)(nuevaLetra - 26);
                    }

                    resultado.Append(nuevaLetra);
                }
                else
                {
                    resultado.Append(caracteres[i]);
                }
            }

            return resultado.ToString();
        }



        static string DecodificarTransposicionSimple(string textoCodificado, int clave)
        {
            // La decodificación es el proceso inverso de la codificación
            return CodificarTransposicionSimple(textoCodificado, -clave);
        }




    }
}