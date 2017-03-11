using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace StudentManageSystem
{
    /// <summary>
    /// Xml账户密码管理
    /// </summary>
    public class ConfigForXml
    {
        //Xml文件路径
        public string XmlPath = Application.StartupPath + @"\config.xml";
        //节点树的基础路径
        public string NodeTree = "Config/IDPassword";
        
        /// <summary>
        /// 创建Xml文件，并定义根节点及其子节点IDPassword，创建管理员账号
        /// </summary>
        public void CreateXml()
        {
            if(!File.Exists(XmlPath))
            {
                XmlDocument clsxmldoc = new XmlDocument();
                XmlNode clsxmlnode = clsxmldoc.CreateNode(XmlNodeType.Element, "Config", string.Empty);
                XmlElement clsxmlelement = clsxmldoc.CreateElement("IDPassword");
                //管理员账号及密码
                XmlElement administrator = clsxmldoc.CreateElement("Administrator");
                administrator.InnerText = "Romantic";

                clsxmldoc.AppendChild(clsxmldoc.CreateXmlDeclaration("1.0", "utf-8", null));
                clsxmldoc.AppendChild(clsxmlnode);
                clsxmlnode.AppendChild(clsxmlelement);
                clsxmlelement.AppendChild(administrator);
                /*
                //创建算法并加密
                SymmetricAlgorithm symalg = SymmetricAlgorithm.Create("DES");  //Triple DES or "Rijndael" keysize = 128/192/256
                Encrypt(administrator, symalg);
                 */

                clsxmldoc.Save(XmlPath);
                //读取文件并更改其隐藏与只读属性
                FileInfo xmlfile = new FileInfo(XmlPath);
                xmlfile.Attributes |= FileAttributes.Hidden;
            }
        }

        /// <summary>
        /// 读取Xml指定Key数据
        /// </summary>
        public string ReadXmlData(string keyname)
        {
            XmlDocument clsxmldoc = new XmlDocument();
            if (!File.Exists(XmlPath))
                return string.Empty;
            clsxmldoc.Load(XmlPath);
            XmlElement clsxmlelement = clsxmldoc.SelectSingleNode(NodeTree + "/" + keyname) as XmlElement;
            if (clsxmlelement == null)
                return string.Empty;
            return clsxmlelement.InnerText;
        }

        /// <summary>
        /// 向Xml中设置数据
        /// </summary>
        public void SetXmlData(string id, string password)
        {
            XmlDocument clsxmldoc = new XmlDocument();
            clsxmldoc.Load(XmlPath);
            XmlElement clsxmlelement = clsxmldoc.SelectSingleNode(NodeTree + "/" + id) as XmlElement;
            if (clsxmlelement == null)
            {
                XmlNode clsxmlnode = clsxmldoc.SelectSingleNode(NodeTree);
                clsxmlelement = clsxmldoc.CreateElement(id);
                clsxmlnode.AppendChild(clsxmlelement);
            }
            clsxmlelement.InnerText = password;
            clsxmldoc.Save(XmlPath);
        }

        /// <summary>
        /// 删除某一账号内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeid"></param>
        /// <param name="password"></param>
        public void RemoveXmlData(string id)
        {
            XmlDocument clsxmldoc = new XmlDocument();
            clsxmldoc.Load(XmlPath);
            XmlNode clsxmlnode1 = clsxmldoc.SelectSingleNode(NodeTree);
            XmlNode clsxmlnode2 = clsxmlnode1.SelectSingleNode(id);
            clsxmlnode1.RemoveChild(clsxmlnode2);
            clsxmldoc.Save(XmlPath);
        }

        /// <summary>
        /// 初始化Xml
        /// </summary>
        public bool ReBuildXml()
        {
            try
            {
                if (File.Exists(XmlPath))
                    File.Delete(XmlPath);
            }
            catch
            {
                return false;
            }
            try
            {
                CreateXml();
            }
            catch
            {
                return false;
            }
            return true;
        }

        #region 加密项(未启用)
        /// <summary>
        /// Xml文档加密
        /// </summary>
        private void Encrypt(XmlElement elementtoencrypt, SymmetricAlgorithm key)
        {
            EncryptedXml exml = new EncryptedXml();
            byte[] encryptedelement = exml.EncryptData(elementtoencrypt, key, false);
            EncryptedData edelement = new EncryptedData();
            edelement.Type = EncryptedXml.XmlEncElementUrl;
            string encryptionMethod = null;

            if (key is TripleDES)
            {
                encryptionMethod = EncryptedXml.XmlEncTripleDESUrl;
            }
            else if (key is DES)
            {
                encryptionMethod = EncryptedXml.XmlEncDESUrl;
            }
            if (key is Rijndael)
            {
                switch (key.KeySize)
                {
                    case 128:
                        encryptionMethod = EncryptedXml.XmlEncAES128Url;
                        break;
                    case 192:
                        encryptionMethod = EncryptedXml.XmlEncAES192Url;
                        break;
                    case 256:
                        encryptionMethod = EncryptedXml.XmlEncAES256Url;
                        break;
                }
            }
            edelement.EncryptionMethod = new EncryptionMethod(encryptionMethod);
            edelement.CipherData.CipherValue = encryptedelement;
            EncryptedXml.ReplaceElement(elementtoencrypt, edelement, false);
        }
        #endregion
    }
}
