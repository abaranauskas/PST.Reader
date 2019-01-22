using PST.Reader.BL.DTO;
using PST.Reader.BL.Services;
using Redemption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace PST.Reader.BL
{
    public class PstReadingService
    {
        private RDOSession _session;
        private RDOFolders _IPMRoot;

        public PstReadingService(string filePath)
        {
            _session = new RDOSession();
            _session.LogonPstStore(filePath);
            _IPMRoot = _session.Stores.DefaultStore.IPMRootFolder.Folders;
        }


        public void GetAccountDetails()
        {
            #region         

            //accounts
            RDOAccounts accounts = _session.Accounts;
            RDOAccountOrderList rdoAccountList = accounts.GetOrder(rdoAccountCategory.acMail);
            List<RDOAccount> accountList = new List<RDOAccount>();

            for (int i = 1; i <= rdoAccountList.Count; i++)
            {
                accountList.Add(rdoAccountList[i]);
            }

            //stores
            var stores = _session.Stores;
            var storesCout = stores.DefaultStore;
            #endregion

            //folders/items/item           

        }

        public List<FolderDTO> GetFolders()
        {
            var folderList = new List<FolderDTO>();
            var folderReadingService = new FolderReadingService();
            for (int i = 1; i <= _IPMRoot.Count; i++)
            {
                folderList = folderReadingService.GetFolder(_IPMRoot[i], folderList);
            }
            return folderList;
        }


        public void GetFolderDetails()
        {
            var foldersList = GetFolders();
            string createText = string.Empty;
            foreach (var folder in foldersList)
            {
                createText += $"Folder Name: {folder.Name}\nFolder items count: {folder.FolderItems.Count}\n";
                createText += $"Folder route: {folder.Route}\n-----------------------------\n";
                var i = 1;
                foreach (var folderItem in folder.FolderItems)
                {
                    createText += $"Item No.: {i}\nItem type: {folderItem.MessageClass}\nCreation Date: {folderItem.CreationTime}\nSender Email: {folderItem.SenderEmailAddress}\n";
                                       
                    if (!string.IsNullOrWhiteSpace(folderItem.Body))
                    {                        
                        createText += $"Message: {folderItem.Body}\n";
                    } else
                    {
                        createText += "\n\n";
                    }
                   
                    i++;
                }
                createText += "\n\n";
            }
            File.WriteAllText(@"C:\Users\aidas\OneDrive\Desktop\aidas.txt", createText);
        }

        //public void GetFolderItems()
        //{
        //    var foldersList = GetFolders();
        //    string createText = string.Empty;
        //    foreach (var folder in foldersList)
        //    {                
        //        foreach (var folderItem in folder.FolderItems)
        //        {
        //            if (!string.IsNullOrWhiteSpace(folderItem.Body))                   
        //                createText += folderItem.Body;                     
        //        }
        //        createText += "\n\n";
        //    }
        //    File.WriteAllText(@"C:\Users\aidas\OneDrive\Desktop\aidas.txt", createText);
        //}
    }
}
