using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST.Reader.BL.DTO
{
    public class FolderItemDTO
    {
        //try
        //{
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    RDOFolder inbox = session.GetDefaultFolder(rdoDefaultFolders.olFolderInbox);

        //    List<RDOMail> mailItemList = inbox.Items.OfType<RDOMail>().ToList();
        //    foreach (RDOMail mail in mailItemList)
        //    {

        //        string fileName = Path.Combine(path, this.MakeValidFileName(mail.SenderEmailAddress, mail.Subject, mail.ReceivedTime + ".msg"));

        //        try
        //        {
        //            mail.SaveAs(fileName + ".msg");
        //        }
        //        catch (Exception)
        //        {
        //            throw new InvalidOperationException("Invalid path: " + fileName);
        //        }

        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show("Please put in a Sysaid! Error:   " + ex);

        //}




        //var store = session.LogonPstStore(path, 1, "combinedPST");
        //var folder = store.IPMRootFolder;

    }
}
