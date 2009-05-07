using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;
using Spring2.DataTierGenerator.PluginInterface;

namespace Spring2.DataTierGenerator.Plugins {
    public class Plugins {

        private List<IPreAndPostProcessFile> preAndPostList = null;

        public Boolean PluginsFound {
            get { if(preAndPostList.Count > 0) 
                        return true;
                  else return false;  }
        }
        
        protected List<T> GetPlugins<T>(string folder) {
            List<T> tList = new List<T>();
            string[] files = null;
            Console.WriteLine("Looking for plugins here: " + folder); 
            try {
                files = Directory.GetFiles(folder, "*.dll");
                foreach (string file in files) {
                    try {
                        Assembly assembly = Assembly.LoadFile(file);
                        foreach (Type type in assembly.GetTypes()) {
                            if (!type.IsClass || type.IsNotPublic) {
                                continue;
                            }
                            Type[] interfaces = type.GetInterfaces();
                            if (((IList)interfaces).Contains(typeof(T))) {
                                object obj = Activator.CreateInstance(type);
                                T t = (T)obj;
                                tList.Add(t);
                            }
                        }
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
            } catch (DirectoryNotFoundException ex) {
                Console.WriteLine(ex.Message);                
            }
            return tList;
        }

        public Plugins(String executablePath) {
            string exeName = executablePath;
            string folder = executablePath + "\\Plugins";
            preAndPostList = GetPlugins<IPreAndPostProcessFile>(folder);
        }

        public void DoPreWriteExisting(String file) {
            foreach (IPreAndPostProcessFile plugin in preAndPostList) {
                plugin.PreWriteExisting(file);
            }
        }

        public void DoPostWriteExisting(String file) {
            foreach (IPreAndPostProcessFile plugin in preAndPostList) {
                plugin.PostWriteExisting(file);
            }
        }

        public void DoPreWriteNew(String file) {
            foreach (IPreAndPostProcessFile plugin in preAndPostList) {
                plugin.PreWriteNew(file);
            }
        }

        public void DoPostWriteNew(String file) {
            foreach (IPreAndPostProcessFile plugin in preAndPostList) {
                plugin.PostWriteNew(file);
            }
        }
    }
}
