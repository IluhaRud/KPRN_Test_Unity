using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas enterCanvas;
    public Canvas registrationCanvas;
    public Canvas bacgroundVideoCanvas;

    public GameObject test;

    public List<Person> persons = new List<Person>();

    private void Awake()
    {
        if (File.Exists("data.xml"))
        {
            StreamReader stream = new StreamReader("data.xml");
            string doc = stream.ReadToEnd();

            if (doc != string.Empty)
            {
                List<XElement> persons = XDocument.Parse(doc).Root.Elements().ToList();

                for (int i = 0; i < persons.Count; i++)
                {
                    string lastName = persons[i].Attribute("last_name").Value;
                    string name = persons[i].Attribute("name").Value;
                    string login = persons[i].Attribute("login").Value;
                    string password = persons[i].Attribute("password").Value;

                    this.persons.Add(new Person(lastName, name, login, password));
                }

                return;
            }
        }

        XElement dataBase = new XElement("DATA_BASE");
        dataBase.Save("data.xml");

    }
    /// <summary>
    /// Происходит при нажатии на кнопку "Начать тестирование"
    /// </summary>
    public void ClickStartButton()
    {
        enterCanvas.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }
    /// <summary>
    /// Происходит при нажатии на кнопку "Выход"
    /// </summary>
    public void ClickExitButton()
    {
        Application.Quit();
    }

    //Меню авторизации

    /// <summary>
    /// Происходит при нажатии на кнопку "Вернуться в меню"
    /// </summary>
    public void ClickReturnButton()
    {
        enterCanvas.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
    /// <summary>
    /// Происходит при нажатии на кнопку "Войти"
    /// </summary>
    public void ClickEnterButton()
    {
        string login = enterCanvas.transform.Find("LoginInputField").Find("Text").GetComponent<Text>().text;
        string password = enterCanvas.transform.Find("PasswordInputField").GetComponent<InputField>().text;

        for (int i = 0; i < persons.Count; i++)
            if (login == persons[i].login)
                if (password == persons[i].password)
                {
                    SceneManager.LoadScene("TestScene");
                    return;
                }
        enterCanvas.gameObject.transform.Find("InvalidLoginOrPasswordExceptionText").gameObject.SetActive(true);
    }
    /// <summary>
    /// Происходит при нажатии на кнопку "Регистрация"
    /// </summary>
    public void ClickRegistrationButton()
    {
        enterCanvas.gameObject.SetActive(false);
        registrationCanvas.gameObject.SetActive(true);
    }

    //Меню регистрации

    /// <summary>
    /// Происходит при нажатии на кнопку "Зарегистрироваться" в окне Регистрации
    /// </summary>
    public void ClickAddNewPersonButton()
    {
        string lastName = registrationCanvas.transform.Find("LastNameInputField").Find("Text").GetComponent<Text>().text;
        string name = registrationCanvas.transform.Find("NameInputField").Find("Text").GetComponent<Text>().text;
        string login = registrationCanvas.transform.Find("LoginInputField").Find("Text").GetComponent<Text>().text;
        string password = registrationCanvas.transform.Find("PasswordInputField").Find("Text").GetComponent<Text>().text;
        string repeatPassword = registrationCanvas.transform.Find("RepeatPasswordInputField").Find("Text").GetComponent<Text>().text;

        GameObject fieldsExceptionText = registrationCanvas.transform.Find("FieldsExceptionText").gameObject;
        GameObject passwordsExceptionText = registrationCanvas.transform.Find("PasswordsExceptionText").gameObject;

        fieldsExceptionText.SetActive(false);
        passwordsExceptionText.SetActive(false);

        if (lastName == string.Empty || name == string.Empty || login == string.Empty || password == string.Empty || repeatPassword == string.Empty)
        {
            fieldsExceptionText.SetActive(true);
            return;
        }

        if (password != repeatPassword)
        {
            passwordsExceptionText.SetActive(true);
            return;
        }

        Person person = new Person(lastName, name, login, password);

        for (int i = 0; i < persons.Count; i++)
        {
            if (!person.Equals(persons[i]) && i + 1 != persons.Count)
                continue;

            if (person.Equals(persons[i]))
                break;

            persons.Add(person);
            person.SerializePerson();
        }

        enterCanvas.gameObject.SetActive(true);
        registrationCanvas.gameObject.SetActive(false);
    }
    /// <summary>
    /// Происходит при нажатии на кнопку "Назад" в окне Регистрации
    /// </summary>
    public void ClickRegistrationBackButton()
    {
        enterCanvas.gameObject.SetActive(true);
        registrationCanvas.gameObject.SetActive(false);
    }
}
