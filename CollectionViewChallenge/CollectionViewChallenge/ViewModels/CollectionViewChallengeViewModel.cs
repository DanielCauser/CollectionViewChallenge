using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using CollectionViewChallenge.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace CollectionViewChallenge.ViewModels
{
    public class CollectionViewChallengeViewModel : BaseViewModel
    {
        public CollectionViewChallengeViewModel()
        {
            _posts = new List<string> { "Causer" };
            //_posts = new List<Post> { new Post { Name = " Causer" } };
        }

        private List<string> _posts;
        public List<string> Posts
        {
            get => _posts;
            set => SetProperty(ref _posts, value);
        }

        public ICommand LoadCommand => new Command(() => OnLoadCommand());

        public async void OnLoadCommand()
        {
            await RunSafeAsync(async () =>
            {
                await LoadPosts();
                IsRefreshing = false;
            });
        }

        public override async void LoadData()
        {
            await RunSafeAsync(async () =>
            {
                await LoadPosts();
            });
        }

        public async Task LoadPosts()
        {
            //using (var httpClient = new HttpClient())
            //{
            //    var result = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
            //    Posts = JsonConvert.DeserializeObject<List<Post>>(result);
            //}
        }
    }
}
