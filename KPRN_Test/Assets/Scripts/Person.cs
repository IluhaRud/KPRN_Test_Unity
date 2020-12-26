using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class Person : IEquatable<Person>
{
    public string lastName;
    public string name;
    public string login;
    public string password;

    public Person(string lastName, string name, string login, string password)
    {
        this.lastName = lastName;
        this.name = name;
        this.login = login;
        this.password = password;
    }

    public void SerializePerson()
    {
        XDocument doc = XDocument.Load("data.xml");

        XElement person = new XElement($"{this.login}");

        XAttribute lastName = new XAttribute("last_name", this.lastName);
        XAttribute name = new XAttribute("name", this.name);
        XAttribute login = new XAttribute("login", this.login);
        XAttribute password = new XAttribute("password", this.password);

        person.Add(lastName, name, login, password);

        doc.Root.Add(person);
        doc.Save("data.xml");
    }

    public bool Equals(Person person)
    {
        return (person.login == this.login);
    }
}
