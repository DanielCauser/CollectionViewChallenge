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
            _posts = new List<Post>();
        }

        private List<Post> _posts;
        public List<Post> Posts
        {
            get => _posts;
            set => SetProperty(ref _posts, value);
        }

        public ICommand LoadCommand => new Command(async () => await OnLoadCommand());

        public async Task OnLoadCommand()
        {
            await RunSafeAsync(async () =>
            {
                await LoadPosts();
                IsRefreshing = false;
            });
        }

        public async Task LoadPosts()
        {

            Posts = new List<Post> { new Post { Name = "Causer" }, new Post { Name = "Daniel" } };
            //Posts = new List<Post> { "Causer" };
            //using (var httpClient = new HttpClient())
            //{
            //    var result = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
            //    Posts = JsonConvert.DeserializeObject<List<Post>>(result);
            //}
        }
    }
}
