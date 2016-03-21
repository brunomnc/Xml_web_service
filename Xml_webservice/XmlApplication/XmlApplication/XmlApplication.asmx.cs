using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Linq;
using System.Web.UI;
using HtmlAgilityPack;
using System.Xml;
using System.Text;
using System.Data;
using System.Web.Script.Services;
using Spire.Doc;

namespace XmlApplication
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private readonly string mpLink = "http://www.mpsc.mp.br";
        class Tags
        {
            private string date, link, desc, image;

            public Tags(string _date, string _link, string _desc, string _image)
            {
                this.date = _date;
                this.link = _link;
                this.desc = _desc;
                this.image = _image;
            }
            public string getDate
            {
                get { return date; }
            }
            public string getLink
            {
                get { return link; }
            }
            public string getDesc
            {
                get { return desc; }
            }
            public string getImage
            {
                get { return image; }
            }
            public void modifyDate(string data)
            {
                this.date = data;
            }
            public void modifyLink(string data)
            {
                this.link = data;
            }
            public void modifyDesc(string data)
            {
                this.desc = data;
            }
            public void modifyImage(string data)
            {
                this.image = data;
            }
        }
        [WebMethod]
        public void htmlParser()
        {
            var webGet = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = webGet.Load("http://portal.mpsc.mp.br/portal/home/mobile");
            List<Tags> tags = new List<Tags>();
            int i = 0;
            int j = 0;
            int k = 0;
            int tagCounter = 0;
            string date, linked, desc, image = null;
            using (XmlWriter writer = XmlWriter.Create(@"C:\Users\BManica\Desktop\sourcetest.xml"))
            {
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes(".//a[@href]"))
                {
                    string hrefValue = link.GetAttributeValue("href", string.Empty);
                    linked = string.Concat(mpLink, hrefValue);
                    Tags tagging = new Tags(null, linked, null, null);
                    tags.Add(tagging);
                    tagCounter++;
                }
                foreach (HtmlNode span in doc.DocumentNode.SelectNodes("//span[@class='date']"))
                {
                    string spanValue = span.GetAttributeValue("span", string.Empty);
                    date = span.InnerText.ToString();
                    tags[i].modifyDate(date);
                    i++;
                }
                foreach (HtmlNode a in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    string descValue = a.InnerText;
                    desc = descValue;
                    tags[j].modifyDesc(desc);
                    j++;
                }
                foreach (HtmlNode img in doc.DocumentNode.SelectNodes(".//img[@src]"))
                {
                    string imgValue = img.GetAttributeValue("src", string.Empty);
                    image = imgValue;
                    tags[k].modifyImage(image);
                    k++;
                }
                writer.WriteStartDocument();
                writer.WriteStartElement("li");
                for (int q = 0; q < tagCounter; q++)
                {
                    writer.WriteStartElement("ul");
                    writer.WriteElementString("a", tags[q].getLink);
                    writer.WriteElementString("span", tags[q].getDate);
                    writer.WriteElementString("p", tags[q].getDesc);
                    writer.WriteElementString("img", tags[q].getImage);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\BManica\Desktop\sourcetest.xml");
        }
    }
}

