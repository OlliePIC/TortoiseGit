using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BinarySerializationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> s = new List<string> { "0", "1", "2" };

            Program oProgram = new Program();
            oProgram.SerializeBinary();
            SerializableObject  o = oProgram.DeSerialize();
            Console.WriteLine("============驗證還原序列化物件===============");
            Console.WriteLine("ClsSerializable.Number : " + o.Number);
            Console.WriteLine("ClsSerializable.Demo : " + o.Demo);
            Console.ReadKey();
        }

        //序列化函式
        private void SerializeBinary() 
        {
            //建立物件
            SerializableObject oSerializable = new SerializableObject();
            //建立資料流物件
            FileStream oFileStream = new FileStream(@"C:\Users\p10271333\Desktop\sbinary.txt", FileMode.Create);
            //建立二進位格式化物件
            BinaryFormatter myBinaryFormatter = new BinaryFormatter();
            Console.WriteLine("二進位格式序列化......");
            //將物件進行二進位格式序列化，並且將之儲存成檔案
            myBinaryFormatter.Serialize(oFileStream, oSerializable);
            oFileStream.Flush();
            oFileStream.Close();
            oFileStream.Dispose();
            Console.WriteLine("完成進位格式序列化......");
        }

        //反序列函式
        private SerializableObject DeSerialize() 
        {
            SerializableObject o = null;
            FileStream oFileStream = new FileStream(@"C:\Users\p10271333\Desktop\sbinary.txt", FileMode.Open);
            BinaryFormatter myBinaryFormatter = new BinaryFormatter();
            Console.WriteLine("開始還原序列化物件......");
            //將檔案還原成原來的物件
            o = (SerializableObject)myBinaryFormatter.Deserialize(oFileStream);
            return o;
        }
    }

    [Serializable]
    public class SerializableObject
    {
        private int _Number;
        private string _Demo;
        public SerializableObject()
        {
            this._Number = 999;
            this._Demo = "this is a bookabc";
        }

        public int Number
        {
            get { return this._Number; }
        }
        public string Demo
        {
            get { return this._Demo; }
        }
    }
}
