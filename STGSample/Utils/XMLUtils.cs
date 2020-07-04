using static STGSample.Utils.ConstantsTable;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using STGSample.Loading;


namespace STGSample.Utils
{
    public static class XMLUtils
    {
        public static List<T> XMLReader<T>(string path)
        {
            List<T> xmlcontent = new List<T>();
            using (var reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                xmlcontent = (List<T>)serializer.Deserialize(reader);
            }
            return xmlcontent;
        }

        public static void XMLWriter<T>(string path, List<T> list)
        {
            using (var writer = new StreamWriter(new FileStream(path, FileMode.Create)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(writer, list);
            }
        }

        public static void WriteSav(PlayerArchive archive)
        {
            using (var writer = new StreamWriter(new FileStream(ARCHIVEPATH, FileMode.OpenOrCreate)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PlayerArchive));
                serializer.Serialize(writer, archive);
            }
        }

        public static PlayerArchive ReadSav()
        {
            PlayerArchive archive = new PlayerArchive();
            if (!File.Exists(ARCHIVEPATH)) WriteSav(archive);
            using (var reader = new StreamReader(new FileStream(ARCHIVEPATH, FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(PlayerArchive));
                archive = (PlayerArchive)serializer.Deserialize(reader);
            }
            return archive;
        }
    }
}
