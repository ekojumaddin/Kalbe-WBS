using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.Model.Custom;

namespace WBSBE.Common.Library
{
    public class clsCommonFunction
    {
        public static bool checkAndDeleteAttacment(string path, string fileName)
        {
            var fullPath = Path.GetFullPath(path).Replace("~\\", "");
            fullPath += fileName;
            if (File.Exists(fullPath)) File.Delete(fullPath);
            return true;
        }

        public static string createURLAttachment(string path, string paramFile = null)
        {
            path = path.Replace("~", "").Replace("/", Path.DirectorySeparatorChar.ToString());
            var fullPath = Path.GetFullPath(ClsGlobalClass.GetRootPath + path);

            return fullPath + paramFile;
        }

        public static string saveAttachment(IFormFile paramFile, string path, string txtRenameFile = null)
        {
            path = path.Replace("~", "").Replace("/", Path.DirectorySeparatorChar.ToString());
            var fullPath = Path.GetFullPath(ClsGlobalClass.GetRootPath + path);
            var fileName = string.IsNullOrEmpty(txtRenameFile) ? paramFile.FileName : txtRenameFile;

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            checkAndDeleteAttacment(fullPath, fileName);

            using (var stream = File.Create(fullPath + fileName))
            {
                paramFile.CopyTo(stream);
            }

            return fileName;
        }

        public static bool sendEmail(cstmMailModel paramModel)
        {
            MailAddress to = new MailAddress(paramModel.txtTo);
            MailAddress from = new MailAddress(paramModel.txtFrom);

            MailMessage mail = new MailMessage(from, to);

            mail.Subject = paramModel.txtSubject;
            mail.Body = paramModel.txtBody;

            SmtpClient smtp = new SmtpClient(ClsGlobalConstant.defaultConfigurationValue.SMTP);

            //var smtp = new SmtpClient("127.0.0.1");

            #region Attachment

            if (paramModel.attachment != null)
            {
                mail.Attachments.Add(new Attachment(paramModel.attachment.OpenReadStream(), paramModel.attachment.FileName));
            }
            else if (paramModel.listAttachment != null)
            {
                foreach (var dtAttachment in paramModel.listAttachment)
                {
                    mail.Attachments.Add(new Attachment(dtAttachment.OpenReadStream(), dtAttachment.FileName));
                }
            }
            #endregion

            #region CC
            if (paramModel.listTxtCC != null)
            {
                foreach (var dtCC in paramModel.listTxtCC)
                {
                    mail.CC.Add(dtCC);
                }
            }
            #endregion 

            smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            smtp.Send(mail);

            return true;
        }
    }
}
