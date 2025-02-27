namespace MoviesRental.Query.Infrastructure.Settings;
public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string DirectorsCollection { get; set; }
    public string DvdsCollection { get; set; }
}
