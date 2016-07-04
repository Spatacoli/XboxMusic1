using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Newtonsoft.Json;
using Windows.Data.Json;
using Windows.Web.Http;
using XboxMusic1.Models;

namespace XboxMusic1.DataModel
{
    public class MusicRepository : IMusicRepository, IDisposable
    {
        private const string ServiceURL = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private const string ClientID = "";
        private const string ClientSecret = "";
        private const string Scope = "http://music.xboxlive.com";
        private const string GrantType = "client_credentials";

        private string mAllText;
        private ResourceLoader mResourceLoader;


        public DateTime TokenExpiration { get; private set; }
        private string mToken;
        public string Token
        {
            get
            {
                if (DateTime.Now >= TokenExpiration)
                    return null;
                else
                    return mToken;
            }
        }

        private HttpClient mClient;

        public MusicRepository()
        {
            mClient = new HttpClient();
            mResourceLoader = ResourceLoader.GetForCurrentView();
            mAllText = mResourceLoader.GetString("AllGenresText");
            Init();
        }

        private async void Init()
        {
            await InitAccessTokenAsync();
        }

        private async Task<T> GetDataAsync<T>(string serviceUrl)
        {
            var response = await mClient.GetAsync(new Uri(serviceUrl));
            var responseString = await response.Content.ReadAsStringAsync();

            T t = JsonConvert.DeserializeObject<T>(responseString);
            return t;
        }

        public async Task<ObservableCollection<string>> GetGenreListAsync()
        {
            if (Token == null)
            {
                await InitAccessTokenAsync();
            }
            var service = string.Format("https://music.xboxlive.com/1/content/music/catalog/genres?accessToken=Bearer+{0}", WebUtility.UrlEncode(Token));

            GenreResults gr = await GetDataAsync<GenreResults>(service);
            var genres = new ObservableCollection<string>(gr.Genres);
            genres.Insert(0, mAllText);

            return genres;
        }

        public async Task<ObservableCollection<Album>> GetNewReleaseDataAsync(string genre)
        {
            if (Token == null)
            {
                await InitAccessTokenAsync();
            }
            var service = string.Empty;

            if (string.IsNullOrEmpty(genre))
            {
                service = string.Format("https://music.xboxlive.com/1/content/music/newreleases?accessToken=Bearer+{0}", WebUtility.UrlEncode(Token));
            }
            else
            {
                genre = WebUtility.UrlEncode(genre);
                service = string.Format("https://music.xboxlive.com/1/content/music/newreleases?genre={0}&accessToken=Bearer+{1}", genre, WebUtility.UrlEncode(Token));
            }
            NewReleaseResults nrr = await GetDataAsync<NewReleaseResults>(service);
            var albums = new ObservableCollection<Album>();
            foreach (NewReleaseResultItem item in nrr.Results.Items)
            {
                albums.Add(item.Album);
            }

            return albums;
        }


        public async Task<Album> GetAlbumAsync(string albumId)
        {
            if (Token == null)
            {
                await InitAccessTokenAsync();
            }
            var service = string.Format("https://music.xboxlive.com/1/content/{0}/lookup?extras=Tracks+AlbumDetails&accessToken=Bearer+{1}", WebUtility.UrlEncode(albumId), WebUtility.UrlEncode(Token));
            AlbumQueryResult sqr = await GetDataAsync<AlbumQueryResult>(service);
            return sqr.Albums.Items.FirstOrDefault<Album>();
        }

        public async Task InitAccessTokenAsync()
        {
            var client = new HttpClient();

            // Define the data needed to request an authorization token.
            var service = ServiceURL;

            // Create the request data.
            var requestData = new Dictionary<string, string>();
            requestData["client_id"] = ClientID; ;
            requestData["client_secret"] = ClientSecret;
            requestData["scope"] = Scope;
            requestData["grant_type"] = GrantType;

            // Post the request and retrieve the response.
            var response = await client.PostAsync(new Uri(service), new HttpFormUrlEncodedContent(requestData));
            var responseString = await response.Content.ReadAsStringAsync();

            JsonValue root = JsonValue.Parse(responseString);
            mToken = root.GetObject().GetNamedString("access_token");


            var expiresIn = root.GetObject().GetNamedString("expires_in");
            double expirationTime;
            if (!double.TryParse(expiresIn, out expirationTime))
                expirationTime = 0;

            TokenExpiration = DateTime.Now.AddMinutes(expirationTime);
        }

        public void Dispose()
        {
            mClient = null;
        }
    }
}
