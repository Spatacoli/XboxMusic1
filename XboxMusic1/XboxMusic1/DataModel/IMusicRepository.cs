using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XboxMusic1.Models;

namespace XboxMusic1.DataModel
{
    public interface IMusicRepository
    {
        Task<ObservableCollection<string>> GetGenreListAsync();
        Task<ObservableCollection<Album>> GetNewReleaseDataAsync(string genre);

        Task<Album> GetAlbumAsync(string albumId);
    }
}
