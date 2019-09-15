using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace XMLSerializer6
{
    class Program
    {
        const string FILENAME = @"E:\Documents\Projects\Serialisation\SQLSchema.xml";
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load(FILENAME);

            Table.tables = doc.Descendants("Table").Select(x => new Table()
            {
                SchemaName = (string)x.Attribute("Schema"),
                TableName = (string)x.Attribute("Name"),
                Columns = x.Descendants("Column").Select(y => new Column()
                {
                    Name = (string)y.Attribute("Name"),
                    IsPrimaryKey = (Boolean)y.Attribute("IsPrimaryKey")
                }).ToList()
            }).ToList();

            Dictionary<string, XElement> tableData = doc.Descendants("TableData")
                .GroupBy(x => (string)x.Attribute("Name"), y => y)
                .ToDictionary(x => x.Key, y => y.FirstOrDefault());

            foreach (Table table in Table.tables)
            {
                XElement xTable = tableData[table.TableName];
                table.SchemaName = (string)xTable.Attribute("Schema");
                table.Rows = xTable.Elements("RowData").Select(x => new Row()
                {
                    RowData = x.Attributes()
                    .GroupBy(y => y.Name.LocalName, z => (string)z)
                    .ToDictionary(y => y.Key, z => z.FirstOrDefault())
                }).ToList();
                var message = JsonConvert.SerializeObject(table);
                Console.WriteLine(message);
                Console.ReadKey();
            }



        }
    }
    public class Table
    {
        public static List<Table> tables = new List<Table>();
        public string TableName { get; set; }

        public string SchemaName { get; set; }

        public List<Column> Columns { get; set; }

        public List<Row> Rows { get; set; }

        public Table()
        {
            Columns = new List<Column>();
            Rows = new List<Row>();
        }
    }
    public class Column
    {
        public string Name { get; set; }

        public bool IsPrimaryKey { get; set; }
    }

    public class Row
    {
        public Dictionary<string, string> RowData { get; set; }
    }
}
