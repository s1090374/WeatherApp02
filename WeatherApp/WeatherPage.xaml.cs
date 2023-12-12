using Java.Lang;
using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
	public List<Models.List> WeatherList;
	private double latitue;
	private double longitude;

	public WeatherPage()
	{
		InitializeComponent();
		WeatherList = new List<Models.List>();
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await GetLocation();
		await GetWeatherDataByLocation(latitue, longitude);
	}

	public async Task GetWeatherDataByLocation(double latitue, double longitude)
	{

		var result = await ApiService.GetWeather(latitue, longitude);
		foreach (var item in result.List)
		{
			WeatherList.Add(item);
		}
		CvWeather.ItemsSource = WeatherList;


		LblCity.Text = result.City.Name;
		LblWeatherDescription.Text = result.List[0].Weather[0].Description;
		LblTemperature.Text = result.List[0].Main.Temperature + "¢XC";
		LblHumidity.Text = result.List[0].Main.Humidity + "%";
		LblWind.Text = result.List[0].Wind.Speed + "km/h";
		ImgWeatherIcon.Source = result.List[0].Weather[0].custIcon;
	}




	public async Task GetLocation()
	{
		var location = await Geolocation.GetLastKnownLocationAsync();
		if (location != null)
		{
			latitue = location.Latitude;
			longitude = location.Longitude;
		}
	}
	private async void TapLocation_Tapped(object sender, EventArgs e)
	{
		await GetLocation();
		await GetWeatherDataByLocation(latitue, longitude);
	}

	private async void ImageButton_Clicked(object sender, EventArgs e)
	{
		var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
		if (response != null)
		{
			await GetWeatherDataByCity(response);
		}
	}

}	
