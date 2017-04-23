using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services.Description;
using Microsoft.CSharp;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Common
{
    public static class WebServiceHelper
    {
        /// <summary>
        /// 动态调用WebService
        /// </summary>
        /// <param name="url">WebService地址</param>
        /// <param name="methodname">方法名(模块名)</param>
        /// <param name="args">参数列表,无参数为null</param>
        /// <returns>object</returns>
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return InvokeWebService(url, null, methodname, args);
        }
        /// <summary>
        /// 动态调用WebService
        /// </summary>
        /// <param name="url">WebService地址</param>
        /// <param name="classname">类名</param>
        /// <param name="methodname">方法名(模块名)</param>
        /// <param name="args">参数列表</param>
        /// <returns>object</returns>
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "http://webservice.datecenter.jscedc";
            if (classname == null || classname == "")
            {
                classname = WebServiceHelper.GetClassName(url);
            }
            //获取服务描述语言(WSDL)
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url + "?wsdl");//【1】
            ServiceDescription sd = ServiceDescription.Read(stream);//【2】
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();//【3】
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);//【4】
            //生成客户端代理类代码
            CodeCompileUnit ccu = new CodeCompileUnit();//【5】
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider csc = new CSharpCodeProvider();//【6】
            ICodeCompiler icc = csc.CreateCompiler();//【7】
            //设定编译器的参数
            CompilerParameters cplist = new CompilerParameters();//【8】
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");
            //编译代理类
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);//【9】
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new System.Exception(sb.ToString());
            }

            //生成代理实例,并调用方法
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            System.Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = System.Activator.CreateInstance(t);//【10】
            System.Reflection.MethodInfo mi = t.GetMethod(methodname);//【11】
            return mi.Invoke(obj, args);

        }
        private static string GetClassName(string url)
        {
            //假如URL为"http://localhost/InvokeService/Service1.asmx"
            //最终的返回值为 Service1
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }
    }
}
