using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace IF.Common.Metro.Framework.Storage
{
    public class FolderManager
    {
        public async static Task<StorageFile> GetFile(string filename, StoreType location = StoreType.Local,
                                                      params string[] folderNames)
        {
            var folder = await GetFolder(location, folderNames);

            var fileList = await folder.GetFilesAsync();

            if (fileList.Any(f => f.Name.Contains(filename)))
            {
                return await folder.GetFileAsync(filename);
            }
            else return null;
        }

        public async static Task<StorageFile[]> GetFileByNameFilter(string filter, StoreType location = StoreType.Local,
                                                                    params string[] folderNames)
        {
            var folder = await GetFolder(location, folderNames);

            var fileList = await folder.GetFilesAsync();

            if (fileList.Any(f => f.Name.Contains(filter)))
            {
                var filtered = fileList.Where(f => f.Name.Contains(filter));

                return filtered.ToArray();
            }
            else return null;
        }

        public async static Task<bool> SaveFileToStoreAs(StorageFile file, string name, StoreType location = StoreType.Local, params string[] folderNames)
        {
            var folder = await GetFolder(location, folderNames);

            try
            {
                await file.CopyAsync(folder, name, NameCollisionOption.ReplaceExisting);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<StorageFolder> GetFolder(StoreType location = StoreType.Local, params string[] folderNames)
        {
            StorageFolder folder;
            switch (location)
            {
                case StoreType.Local:
                    folder = ApplicationData.Current.LocalFolder;
                    break;
                case StoreType.Roaming:
                    folder = ApplicationData.Current.RoamingFolder;
                    break;
                case StoreType.Temporary:
                    folder = ApplicationData.Current.TemporaryFolder;
                    break;
                case StoreType.Install:
                    folder = Package.Current.InstalledLocation;
                    break;
                default:
                    throw new NotImplementedException("Folder type not recognised");
            }

            if (folderNames.Length == 0) return folder;

            for (var i = 0; i < folderNames.Length; i++)
            {
                folder = await folder.CreateFolderAsync(folderNames[i], CreationCollisionOption.OpenIfExists);
            }

            return folder;
        }
    }
}
