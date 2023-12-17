using System.ComponentModel.DataAnnotations.Schema;
using BSAuth.Models.DAO.Generics;
using NetTopologySuite.Geometries;

namespace BSAuth.Models.DAO;
[Table("client_location")]
public class ClientLocation : Base
{
    public Point Location { get; set; }
    public Client Client { get; set; }
    
    public ClientLocation() {}

    public ClientLocation(float lat, float lng)
    {
        Location = new Point(lng, lat);
    }
    
    public ClientLocation(float lat, float lng, Client client)
    {
        Location = new Point(lng, lat);
        Client = client;
    }
}