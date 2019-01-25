using PST.Reader.BL.DTO;
using Redemption;
using System.Collections.Generic;

namespace PST.Reader.BL.Services
{
    public class FolderReadingService
    {      
        public List<FolderDTO> GetFolder(RDOFolder folder, List<FolderDTO> folderList)
        {
            if (folder.Folders.Count != 0)
            {
                for (int i = 1; i <= folder.Folders.Count; i++)
                {
                    GetFolder(folder.Folders[i], folderList);
                }                             
            }

            var newFolderDto = new FolderDTO()
            {
                Name = folder.Name,
                Route = GetFullFolderRoute(folder),
                FolderItems = GetFolderItems(folder)
            };
            folderList.Add(newFolderDto);
            return folderList;
        }

        private List<RDOMail> GetFolderItems(RDOFolder folder)
        {
            var folderItemList = new List<RDOMail>();
            for (int i = 1; i <= folder.Items.Count; i++)
            {
                folderItemList.Add(folder.Items[i]);
            }
            return folderItemList;
        }

        private string GetFullFolderRoute(RDOFolder folder)
        {            
            string route = folder.Name;
            while (!string.IsNullOrWhiteSpace(folder.Parent.Name))
            {
                folder = folder.Parent;
                route = $"{folder.Name} \\ {route} ";
            }
            return route;
        }
    }
}
