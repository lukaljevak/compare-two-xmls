using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace differencesXML
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            XDocument doc1 = XDocument.Load("prvi.xml");
            XDocument doc2 = XDocument.Load("drugi.xml");

            CompareXml(doc1.Root, doc2.Root);
        }

        static void CompareXml(XElement elem1, XElement elem2, string indent = "")
        {
            if (elem1.Name != elem2.Name)
            {
                Console.WriteLine($"{indent}Razlika u nazivima elemenata: <{elem1.Name}> vs <{elem2.Name}>");
                return;
            }

            
            if (!string.Equals(elem1.Value, elem2.Value))
            {
                Console.WriteLine($"{indent}Razlika u sadržaju: '{elem1.Value}' vs '{elem2.Value}'");
            }

            
            var attributes1 = elem1.Attributes().ToList();
            var attributes2 = elem2.Attributes().ToList();

            var commonAttributes = attributes1.Join(attributes2, a1 => a1.Name, a2 => a2.Name, (a1, a2) => a1).ToList();
            var differentAttributes = attributes1.Except(commonAttributes).Concat(attributes2.Except(commonAttributes)).ToList();

            foreach (var attr in differentAttributes)
            {
                Console.WriteLine($"1 {elem1.Name} is different Attribute {attr.Name} IssueInFirst {elem1.Name}={attr.Value} IssueInSecond {elem2.Name}={attr.Value}");
            }

            
            var ch1 = elem1.Elements().ToList();
            var ch2 = elem2.Elements().ToList();

            var commonChl = ch1.Join(ch2, c1 => c1.Name, c2 => c2.Name, (c1, c2) => c1).ToList();
            var differentChl = ch1.Except(commonChl).Concat(ch2.Except(commonChl)).ToList();

            foreach (var child in differentChl)
            {

                CompareXml(child, ch2.FirstOrDefault(c => c.Name == child.Name), indent + "  ");
            }
        Console.ReadKey();
        }
    
    }
   
}