namespace ArtistSqlLite
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _db;
        private int _id;

        public MainPage(LocalDbService db)
        {
            InitializeComponent();
            _db = db;
            Task.Run(async ()=> listView.ItemsSource=await _db.GetArtists());
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (_id == 0) 
            {
                //Add Artist
                await _db.Create(new Artist 
                {
                    ArtistName = nameEntryField.Text,
                    BirthDate = dobEntryField.Date, // Use DatePicker's Date property
                    IsActive = isActiveSwitch.IsToggled, // Use Switch's IsToggled property
                    Salary = decimal.Parse(salaryEntryField.Text)
                });
            }
            else
            {
                //Edit Customer
                await _db.Update(new Artist
                {
                    Id = _id,
                    ArtistName = nameEntryField.Text,
                    BirthDate = dobEntryField.Date,
                    IsActive = isActiveSwitch.IsToggled,
                    Salary = decimal.Parse(salaryEntryField.Text)
                });

                _id = 0;
            }
            nameEntryField.Text = string.Empty;
            dobEntryField.Date = DateTime.Now; // Reset to current date
            isActiveSwitch.IsToggled = false;
            salaryEntryField.Text = string.Empty;

            listView.ItemsSource = await _db.GetArtists();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e) 
        {
            var artist=(Artist)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _id = artist.Id;
                    nameEntryField.Text = artist.ArtistName;
                    dobEntryField.Date = artist.BirthDate;
                    isActiveSwitch.IsToggled = artist.IsActive;
                    salaryEntryField.Text = artist.Salary.ToString();
                    break;
                case "Delete":
                    await _db.Delete(artist);
                    listView.ItemsSource = await _db.GetArtists();
                    break;
            }
        }
    }

}
