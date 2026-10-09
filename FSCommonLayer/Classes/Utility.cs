using System;
using System.Data;
using System.Xml;

namespace  FSCommonLayer.Classes
{
    public static class Utility
    {
        public static bool SendEmail(string p_Email_From, string p_Email_To, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "Fast Services");

                System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(p_Email_To);
                message.To.Add(to);
                
                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                message.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);
                
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static bool SendEmail(string p_Email_From, string p_Email_To, string[] p_Email_Cc, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body, string p_Attachment,int p_Port)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "Fast Services");

                if (p_Email_To != "")
                {
                    System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(p_Email_To);
                    message.To.Add(to);
                }

                foreach (string strEmaiCc in p_Email_Cc)
                {
                    if (strEmaiCc != "")
                    {
                        System.Net.Mail.MailAddress Cc = new System.Net.Mail.MailAddress(strEmaiCc);
                        message.CC.Add(Cc);
                    }
                }

                message.Attachments.Add(new System.Net.Mail.Attachment(p_Attachment));
                
                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);

                smtp.UseDefaultCredentials = false;
                smtp.Port = p_Port;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static bool SendEmail(string p_Email_From, DataTable p_Email_To, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "Fast Services");

                foreach (DataRow dr in p_Email_To.Rows)
                {
                    if (dr["EMAIL"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(dr["EMAIL"].ToString());
                        message.To.Add(to);
                    }
                }
                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                message.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static bool SendEmail(string p_Email_From, DataTable p_Email_To, string[] p_Email_Cc, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "Fast Services");

                foreach (DataRow dr in p_Email_To.Rows)
                {
                    if (dr["EMAIL"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(dr["EMAIL"].ToString());
                        message.To.Add(to);
                    }
                }

                foreach (string strEmaiCc in p_Email_Cc)
                {
                    if (strEmaiCc != "")
                    {
                        System.Net.Mail.MailAddress Cc = new System.Net.Mail.MailAddress(strEmaiCc);
                        message.CC.Add(Cc);
                    }
                }

                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;                
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static bool SendEmail(string p_Email_From, DataTable p_Email_To, DataTable p_Email_Cc, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body, string p_Attachment)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "Fast Services");

                foreach (DataRow dr in p_Email_To.Rows)
                {
                    if (dr["EMAIL"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(dr["EMAIL"].ToString());
                        message.To.Add(to);
                    }
                }


                foreach (DataRow dr in p_Email_Cc.Rows)
                {
                    if (dr["EMAIL"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress cc = new System.Net.Mail.MailAddress(dr["EMAIL"].ToString());
                        message.CC.Add(cc);
                    }
                }

                message.Attachments.Add(new System.Net.Mail.Attachment(p_Attachment));

                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static string ShowAlert(bool IsSuccess,string Message)
        {
            string AlertMessage = "";

            if (IsSuccess)
            {
                AlertMessage = "<div style='padding:10px; margin:10px; background-color:#ecfaf0;border: Solid 1px #04551d; color:#04551d;	font-size:12px; font-family:Verdana;'>" + Message + "</div>";
            }
            else
            {
                AlertMessage = "<div style='padding:10px; margin:10px; background-color:#fbefef;	border: Solid 1px #bb0000; color:#bb0000;	font-size:12px; font-family:Verdana;'>" + Message + "</div>";
            }

            return AlertMessage;
        }

        public static void CreateTransactionFile(string p_FileName, string p_StartElement)
        {
            XmlTextWriter writer = new XmlTextWriter(p_FileName, System.Text.Encoding.UTF8);
            writer.WriteStartDocument();
            writer.WriteStartElement(p_StartElement);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public static void InsertTransaction(string p_FileName, string p_TransactionType, string p_TransactionBy, string p_Entity, string p_TransactionDetail)
        {
            if (!System.IO.File.Exists(p_FileName))
            {
                CreateTransactionFile(p_FileName, "TransactionLog");
            }

            //create new instance of XmlDocument
            XmlDocument doc = new XmlDocument();
            XmlNode refElem = doc.FirstChild;
            //load from file
            doc.Load(p_FileName);

            //create node and add value
            XmlNode node = doc.CreateNode(XmlNodeType.Element, "Row", null);
            //node.InnerText = "this is new node";

            //create TransactionID node
            XmlNode nodeTransactionID = doc.CreateElement("TransactionID");
            //add value for it
            nodeTransactionID.InnerText = DateTime.Now.ToString("yyyyMMddHHmmss");

            //create TransactionType node
            XmlNode nodeTransactionType = doc.CreateElement("TransactionType");
            //add value for it
            nodeTransactionType.InnerText = p_TransactionType;

            //create TransactionBy node
            XmlNode nodeTransactionBy = doc.CreateElement("TransactionBy");
            nodeTransactionBy.InnerText = p_TransactionBy;

            //create Entity node
            XmlNode nodeEntity = doc.CreateElement("Entity");
            nodeEntity.InnerText = p_Entity;

            //create TransactionDetail node
            XmlNode nodeTransactionDetail = doc.CreateElement("TransactionDetail");
            nodeTransactionDetail.InnerText = p_TransactionDetail;

            //create TimeStamp node
            XmlNode nodeTimeStamp = doc.CreateElement("TimeStamp");            
            nodeTimeStamp.InnerText = DateTime.Now.ToString();

            //add to parent node
            node.AppendChild(nodeTransactionID);
            node.AppendChild(nodeTransactionType);
            node.AppendChild(nodeTransactionBy);
            node.AppendChild(nodeEntity);
            node.AppendChild(nodeTransactionDetail);
            node.AppendChild(nodeTimeStamp);
            //add to elements collection
            doc.DocumentElement.AppendChild(node);
            //save back
            doc.Save(p_FileName);
        }

        public static DataTable GetTransaction(string FilePath)
        {
            DataSet ds = new DataSet();

            if (System.IO.File.Exists(FilePath))
            {
                System.IO.FileStream fsReadXml = new System.IO.FileStream(FilePath, System.IO.FileMode.Open);

                try
                {
                    ds.ReadXml(fsReadXml);
                }
                catch (Exception ex)
                {
                    ExceptionPublisher.PublishException(ex);
                }
                finally
                {
                    fsReadXml.Close();
                }

                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public static string GetTransactionFilePath()
        {
            return "~/LogTransaction/" + System.DateTime.Now.ToString("yyyyMM") + "TransactionLog.xml";
        }

    }
}

