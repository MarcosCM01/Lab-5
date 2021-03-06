﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unidad_5_Lab.Models;
using System.Web;

namespace Unidad_5_Lab.Singleton
{
    public class Data
    {
            private static Data instancia = null;
            public static Data Instancia
            {
                get
                {
                    if (instancia == null)
                    {
                        instancia = new Data();
                    }
                    return instancia;
                }
            }

        public Dictionary<string, Informacion> Diccionario1 = new Dictionary<string, Informacion>();
        public Dictionary<string, bool> Diccionario2 = new Dictionary<string, bool>();
        public List<string> Equipos = new List<string>();

        public void LecturaCSV(string path)
        {
            string[] lineas = File.ReadAllLines(path);
            int contador = 0;

            foreach (var linea in lineas)
            {
                Informacion Info = new Informacion();

                if (contador > 0)
                {
                    string[] infolinea = linea.Split(';');
                    for (int i = 12; i < 23; i++)
                    {
                        Estampilla estapa = new Estampilla();
                        
                        estapa.cantidad = int.Parse(infolinea[i]);
                        estapa.numero = int.Parse(infolinea[i - 11]);

                        if(int.Parse(infolinea[i]) == 0)
                        {
                            Info.Faltantes.Add(estapa);
                            Diccionario2.Add(infolinea[0] + infolinea[i - 11], false);
                        }
                        else if(int.Parse(infolinea[i]) > 1)
                        {
                            Info.Coleccionadas.Add(estapa);
                            Info.Disponibles.Add(estapa);
                            Diccionario2.Add(infolinea[0] + infolinea[i - 11], true);
                        }
                        else
                        {
                            Info.Coleccionadas.Add(estapa);
                            Diccionario2.Add(infolinea[0] + infolinea[i - 11], true);
                        }
                    }
                    Equipos.Add(infolinea[0]);
                    Diccionario1.Add(infolinea[0], Info);
                    
                }
                else { contador++; }

                
            }
        }

        
    }
}