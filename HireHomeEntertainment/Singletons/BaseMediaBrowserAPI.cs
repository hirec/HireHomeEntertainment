using MediaBrowser.ApiInteraction;
using MediaBrowser.Model.ApiClient;
using MediaBrowser.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HireHomeEntertainment.Singletons
{
    /// <summary>
    /// Singletons in C#. A class that has only one instance, 
    /// and provides a global point of access to the instance. 
    /// </summary>

    public sealed class BaseMediaBrowserAPI
    {
        private static volatile BaseMediaBrowserAPI instance;
        private static object syncRoot = new Object();

        private ApiClient baseAPIClient { get; set; }
        public ApiClient publicAPIClient { get; private set; }

        private BaseMediaBrowserAPI() {
            try
            {
                baseAPIClient = new ApiClient("localhost", 0, "", "", "");
                LoadKernel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private async void LoadKernel()
        {
            try
            {              
                await loadInitialClient().ConfigureAwait(false);
                UserDto locUser = await loadUsers(baseAPIClient);
                baseAPIClient.CurrentUserId = locUser.Id;
                publicAPIClient = baseAPIClient;
             
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error launching Media Browser: " + ex.Message);
            }
        }

        private async Task loadInitialClient()
        {
            var foundServer = false;

            if (!foundServer)
            {
                try
                {
                    var address = await new ServerLocator().FindServer(500, CancellationToken.None).ConfigureAwait(false);

                    var parts = address.ToString().Split(':');

                    baseAPIClient.ServerHostName = parts[0];
                    baseAPIClient.ServerApiPort = address.Port;

                    foundServer = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error attemting to locate server. error: " + ex.Message);
                }
            }
        }

        private async Task<UserDto> loadUsers(ApiClient client)
        {            
            //Get Users
            var users = await client.GetUsersAsync();
            var currentUser = users.First();           
            return currentUser;
        }

        public static BaseMediaBrowserAPI Instance
        {
            get 
            {
                if (instance == null) 
                {
                lock (syncRoot) 
                {
                    if (instance == null)
                        instance = new BaseMediaBrowserAPI();
                }
                }

                return instance;
            }
        }
    }
}
