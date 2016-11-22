using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace boost
{
  class PluginPlatform
  {
    Assembly dll;
    
    Type type;
    
    object inst;
    
    private string[] pluginData;
    
    public void PluginPlatform(string DLLLocation, string type_)
    {
      dll = Assembly.LoadFrom(DLLLocation);
      type = dll.GetType(type_);
      
      inst = Activator.CreateInstance(type);
      
      inst.Name = pluginData["name"];
      
      inst.Version = pluginData["version"];
      
      inst.Type = pluginData["type"];
    }
    
    public void CallSetupMethod()
    {
      inst.Setup();
    }
    
    public void CallMainLoop(object[] parameters)
    {
      inst.Main(paramaters);
    }
    
    public void GetPluginInfo()
    {
    }
  }
}
