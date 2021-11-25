﻿using PeopleViewer.Common;
using System.Net;
using System.Text.Json;

namespace PersonDataReader.Service;

public class ServiceReader
{
    WebClient client = new();
    string baseUri = "http://localhost:9874";
    JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public IEnumerable<Person> GetPeople()
    {
        string address = $"{baseUri}/people";
        string reply = client.DownloadString(address);
        return JsonSerializer.Deserialize<IEnumerable<Person>>(reply, options) ?? new List<Person>();
    }

    public Person GetPerson(int id)
    {
        string address = $"{baseUri}/people/{id}";
        string reply = client.DownloadString(address);
        return JsonSerializer.Deserialize<Person>(reply, options) ?? new Person();
    }
}