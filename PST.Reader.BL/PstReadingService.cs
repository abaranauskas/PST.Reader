using PST.Reader.BL.DTO;
using PST.Reader.BL.Services;
using Redemption;
using System.Collections.Generic;
using System.IO;

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

        private List<FolderDTO> GetFolders()
        {
            var folderList = new List<FolderDTO>();
            var folderReadingService = new FolderReadingService();
            for (int i = 1; i <= _IPMRoot.Count; i++)
            {
                folderList = folderReadingService.GetFolder(_IPMRoot[i], folderList);
            }
            return folderList;
        }


        public void GetFolderItems(string outputFolderPath)
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
                    }
                    else
                    {
                        createText += "\n\n";
                    }
                    i++;
                }
                createText += "\n\n";
            }
            File.WriteAllText(outputFolderPath, createText);
        }


        public void GetFoldersStructure(string outputFolderPath)
        {
            string foldersStructure = $"Root folder: {_IPMRoot[1].Parent.Name}\n\n"; ;
            for (int i = 1; i <= _IPMRoot.Count; i++)
            {
                string folderPosition = $"{i}";
                foldersStructure += $"{folderPosition}. {_IPMRoot[i].Name} (Folder Type: {_IPMRoot[i].FolderKind}, FolderItemsCount: {_IPMRoot[i].Items.Count})\n";
                foldersStructure = GetFolder(_IPMRoot[i], foldersStructure, folderPosition);
                foldersStructure += "\n";
            }
            File.WriteAllText(outputFolderPath, foldersStructure);
        }

        private string GetFolder(RDOFolder rDOFolder, string foldersStructure, string folderPosition)
        {
            if (rDOFolder.Folders.Count > 0)
            {
                var localfolderPosition = folderPosition;
                for (int i = 1; i <= rDOFolder.Folders.Count; i++)
                {
                    folderPosition = $"{folderPosition}.{i}";
                    var folderDetails = $"{rDOFolder.Folders[i].Name} (Folder Type: {rDOFolder.Folders[i].FolderKind}, FolderItemsCount: {rDOFolder.Folders[i].Items.Count})";
                    foldersStructure += $"{folderPosition}. {folderDetails}\n";
                    foldersStructure = GetFolder(rDOFolder.Folders[i], foldersStructure, folderPosition);
                    folderPosition = localfolderPosition;
                }
            }
            return foldersStructure;
        }
    }
}
