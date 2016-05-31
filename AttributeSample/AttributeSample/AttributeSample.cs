using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeSample
{
    class AttributeSample
    {
        static void Main(string[] args)
        {
            //取出操作對象的Type
            System.Type oType = typeof(SomeClass);
            //利用反射取出成員資訊（記住Reflection蠻耗效能）
            System.Reflection.MemberInfo oItemInfo = oType;

            /*取出類別上方的Attribute*/
            //取出類別Class中隱含的屬性(0筆至多筆)

            var writers = System.Attribute.GetCustomAttributes(oItemInfo).Where(x => x.GetType().ToString() == "AttributeSample.Writer");
            
            //也可以用這個方法取出特定類型的Attribute
            var writers2 = oItemInfo.GetCustomAttributes(typeof(Writer),true);

            //from x in writers 

            foreach (Writer oWriter in writers)
            {
                //var test = oObject.GetType();
                Console.Write("Writer id={0}, ", oWriter.id.ToString());
                Console.WriteLine("name={0}", oWriter.name);
            }

            var customattribute = System.Attribute.GetCustomAttributes(oItemInfo).Where(x => x.GetType().ToString() == "AttributeSample.CustomAttribute");

            //也可以用這個方法取出特定類型的Attribute
            var customattribute2 = oItemInfo.GetCustomAttributes(typeof(CustomAttribute), true);

            foreach(CustomAttribute oCustomAttribute in customattribute)
            {
                Console.Write("CustomAttribute fortest={0}, ", oCustomAttribute.fortest.ToString());
                Console.WriteLine("customid={0}", oCustomAttribute.customid);
            }

            /*取出方法上方的Attribute*/
            //取出這個類別裡面的所有方法
            foreach (System.Reflection.MethodInfo oMethod in oType.GetMethods())
            {

                var writersMethod = System.Attribute.GetCustomAttributes(oMethod).Where(x => x.GetType().ToString() == "AttributeSample.Writer");

                //取出類別Method中隱含的屬性(0筆至多筆)
                foreach (var oTemp in writersMethod)
                {
                    //因為有可能取到.ToString()之類的繼承方法，那些並不是我們所需要的，所以必須過濾掉
                    if (oTemp.GetType() == typeof(Writer))
                    {
                        Writer oWriter = (Writer)oTemp;
                        System.Console.Write("Method Name: {0}; ", oMethod.Name);
                        System.Console.Write("Writer id={0}, ", oWriter.id.ToString());
                        System.Console.Write("name={0}, ", oWriter.name);
                        System.Console.WriteLine("note={0}", oWriter.note);
                    }
                }

                var customattributeMethod = System.Attribute.GetCustomAttributes(oMethod).Where(x => x.GetType().ToString() == "AttributeSample.CustomAttribute");

                //取出類別Method中隱含的屬性(0筆至多筆)
                foreach (var oTemp in customattributeMethod)
                {
                    //因為有可能取到.ToString()之類的繼承方法，那些並不是我們所需要的，所以必須過濾掉
                    if (oTemp.GetType() == typeof(CustomAttribute))
                    {
                        CustomAttribute oCustomAttribute = (CustomAttribute)oTemp;
                        System.Console.Write("Method Name: {0}; ", oMethod.Name);
                        System.Console.Write("CustomAttribute fortest={0}, ", oCustomAttribute.fortest.ToString());
                        System.Console.Write("customid={0}, ", oCustomAttribute.customid);
                    }
                }

            }

            System.Console.Read();
        }
    }

    //示範類別（單純的示範這個程式用來記載某個類別方法是「誰」寫的）
    [Writer(id = 777, name = "John")]
    [Writer(id = 888, name = "Mary")]
    [CustomAttribute(customid=123, fortest=true)]
    class SomeClass
    {
        [Writer(id = 901, name = "Programer-1", note = "程式設計師一")]
        [Writer(id = 902, name = "Programer-2", note = "程式設計師二")]
        public string ADD()
        { return "Method ADD has been run."; }

        [Writer(id = 903, name = "Programer-3", note = "程式設計師三")]
        [CustomAttribute(customid=168, fortest=true)]
        public string DEL()
        { return "Method DEL has been run."; }
    }

    //自訂Attribute類別，一定要繼承System.Attribute
    //且可藉由上方的Attribute來進行細部的設定（例如多Attribute的AllowMultiple設定）
    [System.AttributeUsage(
        System.AttributeTargets.All,
        Inherited = false,
        AllowMultiple = true)]
    class Writer : System.Attribute
    {
        public int id { get; set; }

        public string name { get; set; }

        public string note { get; set; }
    }

    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    class CustomAttribute : System.Attribute
    {
        public bool fortest { get; set; }

        public int customid { get; set; }
    }

}
